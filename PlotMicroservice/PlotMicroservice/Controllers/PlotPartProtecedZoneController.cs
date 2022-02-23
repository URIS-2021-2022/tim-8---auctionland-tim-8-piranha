using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartProtectedZoneModel;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using PlotMicroservice.Validators;
using PlotMicroservice.ServiceCalls;
using Microsoft.Extensions.Logging;

namespace PlotMicroservice.Controllers
{
    /// <summary>
    /// Plot part protected zone controller. Gives access to fields and methods of plot part protected zone.
    /// Produces JSON and XML objects as response to a request.    
    /// </summary>
    [ApiController]
    [Route("api/plot-part-protected-zones")]
    [Produces("application/json", "application/xml")]
    public class PlotPartProtecedZoneController : ControllerBase
    {
        private readonly IPlotPartProtectedZoneRepository PlotPartProtectedZoneRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotPartProtectedZoneValidator Validator;
        private readonly ILoggerService Logger;

        /// <summary>
        /// Plot part protected zone constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotPartProtectedZoneRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        /// <param name="logger"></param>
        public PlotPartProtecedZoneController(IPlotPartProtectedZoneRepository plotPartProtectedZoneRepository, IMapper mapper, LinkGenerator linkGenerator, PlotPartProtectedZoneValidator validator, ILoggerService logger)
        {
            PlotPartProtectedZoneRepository = plotPartProtectedZoneRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
            Logger = logger;
        }

        /// <summary>
        /// Getting all instances of plot part protected zones for given filter.
        /// </summary>
        /// <param name="protectedZone">Protected zone (ex. 3)</param>
        /// <returns>List of plot part protected zones.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotPartProtectedZoneDto>>> GetPlotPartProtectedZonesAsync(string protectedZone)
        {
            List<PlotPartProtectedZone> plotPartProtectedZones = await PlotPartProtectedZoneRepository.GetPlotPartProtectedZonesAsync(protectedZone);

            if(plotPartProtectedZones == null || plotPartProtectedZones.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part protected zone list is empty!", "Plot microservice", "GetPlotPartProtectedZonesAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part protected zone list successfully returned!", "Plot microservice", "GetPlotPartProtectedZonesAsync");
            return Ok(Mapper.Map<List<PlotPartProtectedZoneDto>>(plotPartProtectedZones));
        }

        /// <summary>
        /// Getting plot part protected zone by given GUID of plot part protected zone as parameter.
        /// </summary>
        /// <param name="plotPartProtectedZoneId"></param>
        /// <returns>Single plot part protected zone.</returns>
        [HttpGet("{plotPartProtectedZoneId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotPartProtectedZoneDto>> GetPlotPartProtectedZoneByIdAsync(Guid plotPartProtectedZoneId)
        {
            PlotPartProtectedZone plotPartProtectedZone = await PlotPartProtectedZoneRepository.GetPlotPartProtectedZoneByIdAsync(plotPartProtectedZoneId);

            if(plotPartProtectedZone == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part protected zone not found!", "Plot microservice", "GetPlotPartProtectedZoneByIdAsync");
                return NotFound();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part protected zone found and successfully returned!", "Plot microservice", "GetPlotPartProtectedZoneByIdAsync");
            return Ok(Mapper.Map<PlotPartProtectedZoneDto>(plotPartProtectedZone));
        }

        /// <summary>
        /// Creating new plot part protected zone.
        /// </summary>
        /// <param name="plotPartProtectedZoneCreation"></param>
        /// <returns>Confirmation about created plot part protected zone.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-part-protected-zones \
        /// {   \
        ///     "protectedZone": "TestProtectedZone" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartProtectedZoneConfirmationDto>> CreatePlotPartProtectedZoneAsync([FromBody] PlotPartProtectedZoneCreationDto plotPartProtectedZoneCreation)
        {
            try
            {
                PlotPartProtectedZone plotPartProtectedZone = Mapper.Map<PlotPartProtectedZone>(plotPartProtectedZoneCreation);

                Validator.ValidateAndThrow(plotPartProtectedZone);
                
                PlotPartProtectedZoneConfirmation plotPartProtectedZoneConfirmation = await PlotPartProtectedZoneRepository.CreatePlotPartProtectedZoneAsync(plotPartProtectedZone);
                await PlotPartProtectedZoneRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlotPartProtectedZones", "PlotPartProtecedZone", new { plotPartProtectedZoneId = plotPartProtectedZoneConfirmation.PlotPartProtectedZoneId});

                await Logger.LogMessage(LogLevel.Information, "Plot part protected zone successfully created!", "Plot microservice", "CreatePlotPartProtectedZoneAsync");
                return Created(uri, Mapper.Map<PlotPartProtectedZoneConfirmationDto>(plotPartProtectedZoneConfirmation));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part protected zone object failed!", "Plot microservice", "CreatePlotPartProtectedZoneAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part protected zone object creation failed!", "Plot microservice", "CreatePlotPartProtectedZoneAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot part protected zone by given GUID.
        /// </summary>
        /// <param name="plotPartProtectedZoneUpdate"></param>
        /// <returns>Updated plot part protected zone.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT /api/plot-part-protected-zones \
        /// {   \
        ///     "plotPartProtectedZoneId": "5d41023b-d930-4f2c-f578-08d9e4d11ca1", \
        ///     "protectedZone": "UpdatedProtectedZone" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartProtectedZoneDto>> UpdatePlotPartProtectedZoneAsync(PlotPartProtectedZoneUpdateDto plotPartProtectedZoneUpdate)
        {
            try
            {
                PlotPartProtectedZone existingPlotPartProtectedZone = await PlotPartProtectedZoneRepository.GetPlotPartProtectedZoneByIdAsync(plotPartProtectedZoneUpdate.PlotPartProtectedZoneId);

                if(existingPlotPartProtectedZone == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part protected zone object not found!", "Plot microservice", "UpdatePlotPartProtectedZoneAsync");
                    return NotFound();
                }

                PlotPartProtectedZone plotPartProtectedZone = Mapper.Map<PlotPartProtectedZone>(plotPartProtectedZoneUpdate);

                Validator.ValidateAndThrow(plotPartProtectedZone);
                
                Mapper.Map(plotPartProtectedZone, existingPlotPartProtectedZone);

                await PlotPartProtectedZoneRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part protected zone object updated successfully!", "Plot microservice", "UpdatePlotPartProtectedZoneAsync");
                return Ok(Mapper.Map<PlotPartProtectedZoneDto>(existingPlotPartProtectedZone));

            } catch (ValidationException ve) 
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part protected zone object failed!", "Plot microservice", "UpdatePlotPartProtectedZoneAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }  
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part protected zone object updating failed!", "Plot microservice", "UpdatePlotPartProtectedZoneAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleting plot part protected zone by given GUID.
        /// </summary>
        /// <param name="plotPartProtectedZoneId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotPartProtectedZoneId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotPartProtectedZoneAsync(Guid plotPartProtectedZoneId)
        {
            try
            {
                PlotPartProtectedZone plotPartProtectedZone = await PlotPartProtectedZoneRepository.GetPlotPartProtectedZoneByIdAsync(plotPartProtectedZoneId);

                if (plotPartProtectedZone == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part protected zone object not found!", "Plot microservice", "DeletePlotPartProtectedZoneAsync");
                    return NotFound();
                }

                await PlotPartProtectedZoneRepository.DeletePlotPartProtectedZoneAsync(plotPartProtectedZoneId);
                await PlotPartProtectedZoneRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part protected zone object deleted successfully!", "Plot microservice", "DeletePlotPartProtectedZoneAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part protected zone object deletion failed!", "Plot microservice", "DeletePlotPartProtectedZoneAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotPartProtectedZoneOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotPartProtectedZoneOptions");

            return Ok();
        }
    }
}
