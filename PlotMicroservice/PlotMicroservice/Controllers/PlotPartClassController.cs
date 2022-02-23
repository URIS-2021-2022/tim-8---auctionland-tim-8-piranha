using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartClassModel;
using PlotMicroservice.Validators;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using PlotMicroservice.ServiceCalls;
using Microsoft.Extensions.Logging;

namespace PlotMicroservice.Controllers
{
    /// <summary>
    /// Plot part class controller. Gives access to fields and methods of plot part class.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-part-classes")]
    [Produces("application/json", "application/xml")]
    public class PlotPartClassController : ControllerBase
    {
        private readonly IPlotPartClassRepository PlotPartClassRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotPartClassValidator Validator;
        private readonly ILoggerService Logger;

        /// <summary>
        /// Plot part class constructor.
        /// Initializes properties.s
        /// </summary>
        /// <param name="plotPartClassRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        /// <param name="logger"></param>
        public PlotPartClassController(IPlotPartClassRepository plotPartClassRepository, IMapper mapper, LinkGenerator linkGenerator, PlotPartClassValidator validator, ILoggerService logger)
        {
            PlotPartClassRepository = plotPartClassRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
            Logger = logger;
        }

        /// <summary>
        /// Getting all instances of plot part classes for given filter.
        /// </summary>
        /// <param name="plotPartClass">Plot part class (ex. II)</param>
        /// <returns>List of plot part classes.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotPartClassDto>>> GetPlotPartClassesAsync(string plotPartClass)
        {
            List<PlotPartClass> plotPartClasses = await PlotPartClassRepository.GetPlotPartClassesAsync(plotPartClass);

            if (plotPartClasses == null || plotPartClasses.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part class list is empty!", "Plot microservice", "GetPlotPartClassesAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part class list successfully returned!", "Plot microservice", "GetPlotPartClassesAsync");
            return Ok(Mapper.Map<List<PlotPartClassDto>>(plotPartClasses));
        }

        /// <summary>
        /// Getting plot part class by given GUID of plot part class as parameter.
        /// </summary>
        /// <param name="plotPartClassId"></param>
        /// <returns>Single plot part class.</returns>
        [HttpGet("{plotPartClassId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotPartClassDto>> GetPlotPartClassByIdAsync(Guid plotPartClassId)
        {
            PlotPartClass plotPartClass = await PlotPartClassRepository.GetPlotPartClassByIdAsync(plotPartClassId);

            if (plotPartClass == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part class not found!", "Plot microservice", "GetPlotPartClassByIdAsync");
                return NotFound();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part class found and successfully returned!", "Plot microservice", "GetPlotPartClassByIdAsync");
            return Ok(Mapper.Map<PlotPartClassDto>(plotPartClass));
        }

        /// <summary>
        /// Creating new plot part class.
        /// </summary>
        /// <param name="plotPartClassCreation"></param>
        /// <returns>Confirmation about created plot part class.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-part-classes \
        /// {   \
        ///     "class": "New plot part class" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartClassConfirmationDto>> CreatePlotPartClassAsync([FromBody] PlotPartClassCreationDto plotPartClassCreation)
        {
            try
            {
                PlotPartClass plotPartClass = Mapper.Map<PlotPartClass>(plotPartClassCreation);

                Validator.ValidateAndThrow(plotPartClass);

                PlotPartClassConfirmation plotPartClassConfirmation = await PlotPartClassRepository.CreatePlotPartClassAsync(plotPartClass);
                await PlotPartClassRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlotPartClasses", "PlotPartClass", new { plotPartClassId = plotPartClassConfirmation.PlotPartClassId });

                await Logger.LogMessage(LogLevel.Information, "Plot part class successfully created!", "Plot microservice", "CreatePlotPartClassAsync");
                return Created(uri, Mapper.Map<PlotPartClassConfirmationDto>(plotPartClassConfirmation));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part class object failed!", "Plot microservice", "CreatePlotPartClassAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part class object creation failed!", "Plot microservice", "CreatePlotPartClassAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot part class by given GUID.
        /// </summary>
        /// <param name="plotPartClassUpdate"></param>
        /// <returns>Updated plot part class.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT /api/plot-part-classes \
        /// {   \
        ///     "plotPartClassId": "f319d30e-85b7-40d3-e79b-08d9e4b5d468", \
        ///     "class": "UpdatedPlotPartClass" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartClassDto>> UpdatePlotPartClassAsync(PlotPartClassUpdateDto plotPartClassUpdate)
        {
            try
            {
                PlotPartClass existingPlotPartClass = await PlotPartClassRepository.GetPlotPartClassByIdAsync(plotPartClassUpdate.PlotPartClassId);

                if (existingPlotPartClass == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part class object not found!", "Plot microservice", "UpdatePlotPartClassAsync");
                    return NotFound();
                }

                PlotPartClass plotPartClass = Mapper.Map<PlotPartClass>(plotPartClassUpdate);

                Validator.ValidateAndThrow(plotPartClass);
                
                Mapper.Map(plotPartClass, existingPlotPartClass);

                await PlotPartClassRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part class object updated successfully!", "Plot microservice", "UpdatePlotPartClassAsync");
                return Ok(Mapper.Map<PlotPartClassDto>(existingPlotPartClass));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part class object failed!", "Plot microservice", "UpdatePlotPartClassAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part class object updating failed!", "Plot microservice", "UpdatePlotPartClassAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleting plot part class by given GUID.
        /// </summary>
        /// <param name="plotPartClassId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotPartClassId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotPartClassAsync(Guid plotPartClassId)
        {
            try
            {
                PlotPartClass plotPartClass = await PlotPartClassRepository.GetPlotPartClassByIdAsync(plotPartClassId);

                if(plotPartClass == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part class object not found!", "Plot microservice", "DeletePlotPartClassAsync");
                    return NotFound();
                }

                await PlotPartClassRepository.DeletePlotPartClassAsync(plotPartClassId);
                await PlotPartClassRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part class object deleted successfully!", "Plot microservice", "DeletePlotPartClassAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part class object deletion failed!", "Plot microservice", "DeletePlotPartClassAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotPartClassOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotPartClassOptions");

            return Ok();
        }
    }
}
