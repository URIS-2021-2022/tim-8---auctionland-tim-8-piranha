using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotWorkabilityModel;
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
    /// Plot workability controller. Gives access to fields and methods of plot workability.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-workabilities")]
    [Produces("application/json", "application/xml")]
    public class PlotWorkabilityController : ControllerBase
    {
        private readonly IPlotWorkabilityRepository PlotWorkabilityRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotWorkabilityValidator Validator;
        private readonly ILoggerService Logger;

        /// <summary>
        /// Plot workability constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotWorkabilityRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        public PlotWorkabilityController(IPlotWorkabilityRepository plotWorkabilityRepository, IMapper mapper, LinkGenerator linkGenerator, PlotWorkabilityValidator validator, ILoggerService logger)
        {
            PlotWorkabilityRepository = plotWorkabilityRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
            Logger = logger;
        }

        /// <summary>
        /// Getting all instances of plot workabilities for given filter.
        /// </summary>
        /// <param name="workability">Workability (ex. Obradivo)</param>
        /// <returns>List of plot workabilities.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotWorkabilityDto>>> GetPlotWorkabilitiesAsync(string workability)
        {
            List<PlotWorkability> plotWorkabilities = await PlotWorkabilityRepository.GetPlotWorkabilitiesAsync(workability);

            if(plotWorkabilities == null || plotWorkabilities.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot workability list is empty!", "Plot microservice", "GetPlotWorkabilitiesAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot workability list successfully returned!", "Plot microservice", "GetPlotWorkabilitiesAsync");
            return Ok(Mapper.Map<List<PlotWorkabilityDto>>(plotWorkabilities));
        }

        /// <summary>
        /// Getting plot workability by given GUID of plot workability as parameter.
        /// </summary>
        /// <param name="plotWorkabilityId"></param>
        /// <returns>Single plot workability.</returns>
        [HttpGet("{plotWorkabilityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotWorkabilityDto>> GetPlotWorkabilityIdAsync(Guid plotWorkabilityId)
        {
            PlotWorkability plotWorkability = await PlotWorkabilityRepository.GetPlotWorkabilityByIdAsync(plotWorkabilityId);

            if(plotWorkability == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot workability not found!", "Plot microservice", "GetPlotWorkabilityIdAsync");
                return NotFound();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot workability found and successfully returned!", "Plot microservice", "GetPlotWorkabilityIdAsync");
            return Ok(Mapper.Map<PlotWorkabilityDto>(plotWorkability));
        }

        /// <summary>
        /// Creating new plot workability.
        /// </summary>
        /// <param name="plotWorkabilityCreation"></param>
        /// <returns>Confirmation about created plot workability.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-workabilities \
        /// {   \
        ///     "workability": "TestWorkability" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotWorkabilityConfirmationDto>> CreatePlotWorkabilityAsync([FromBody] PlotWorkabilityCreationDto plotWorkabilityCreation)
        {
            try
            {
                PlotWorkability plotWorkability = Mapper.Map<PlotWorkability>(plotWorkabilityCreation);

                Validator.ValidateAndThrow(plotWorkability);
                
                PlotWorkabilityConfirmation plotWorkabilityConfirmation = await PlotWorkabilityRepository.CreatePlotWorkabilityAsync(plotWorkability);
                await PlotWorkabilityRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlotWorkabilities", "PlotWorkability", new { plotWorkabilityId = plotWorkabilityConfirmation.PlotWorkabilityId });

                await Logger.LogMessage(LogLevel.Information, "Plot workability successfully created!", "Plot microservice", "CreatePlotWorkabilityAsync");
                return Created(uri, Mapper.Map<PlotWorkabilityConfirmationDto>(plotWorkabilityConfirmation));

            } catch(ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot workability object failed!", "Plot microservice", "CreatePlotWorkabilityAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot workability object creation failed!", "Plot microservice", "CreatePlotWorkabilityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot workability by given GUID.
        /// </summary>
        /// <param name="plotWorkabilityUpdate"></param>
        /// <returns>Updated plot workability.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT /api/plot-workablities \
        /// {   \
        ///      "plotWorkabilityId": "97a257a0-127e-4167-9faf-08d9e421e1e4", \
        ///      "workability": "UpdatedWorkability" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotWorkabilityDto>> UpdatePlotWorkabilityAsync(PlotWorkabilityUpdateDto plotWorkabilityUpdate)
        {
            try
            {
                PlotWorkability existingPlotWorkability = await PlotWorkabilityRepository.GetPlotWorkabilityByIdAsync(plotWorkabilityUpdate.PlotWorkabilityId);

                if (existingPlotWorkability == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot workability object not found!", "Plot microservice", "UpdatePlotWorkabilityAsync");
                    return NotFound();
                }

                PlotWorkability plotWorkability = Mapper.Map<PlotWorkability>(plotWorkabilityUpdate);

                Validator.ValidateAndThrow(plotWorkability);
                
                Mapper.Map(plotWorkability, existingPlotWorkability);

                await PlotWorkabilityRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot workability object updated successfully!", "Plot microservice", "UpdatePlotWorkabilityAsync");
                return Ok(Mapper.Map<PlotWorkabilityDto>(existingPlotWorkability));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot workability object failed!", "Plot microservice", "UpdatePlotWorkabilityAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot workability object updating failed!", "Plot microservice", "UpdatePlotWorkabilityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleting plot workability by given GUID.
        /// </summary>
        /// <param name="plotWorkabilityId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotWorkabilityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotWorkabilityAsync(Guid plotWorkabilityId)
        {
            try
            {
                PlotWorkability plotWorkability = await PlotWorkabilityRepository.GetPlotWorkabilityByIdAsync(plotWorkabilityId);

                if (plotWorkability == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot workability object not found!", "Plot microservice", "DeletePlotWorkabilityAsync");
                    return NotFound();
                }

                await PlotWorkabilityRepository.DeletePlotWorkabilityAsync(plotWorkabilityId);
                await PlotWorkabilityRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot workability object deleted successfully!", "Plot microservice", "DeletePlotWorkabilityAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot workability object deletion failed!", "Plot microservice", "DeletePlotWorkabilityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotWorkabilityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotWorkabilityOptions");

            return Ok();
        }
    }
}
