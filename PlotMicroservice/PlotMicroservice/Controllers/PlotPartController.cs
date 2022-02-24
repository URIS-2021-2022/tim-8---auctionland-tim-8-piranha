using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartModel;
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
    /// Plot part controller. Gives access to fields and methods of plot part.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-parts")]
    [Produces("application/json", "application/xml")]
    public class PlotPartController : ControllerBase
    {
        private readonly IPlotPartRepository PlotPartRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotPartValidator Validator;
        private readonly ILoggerService Logger;

        /// <summary>
        /// Plot part constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotPartRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        /// <param name="logger"></param>
        public PlotPartController(IPlotPartRepository plotPartRepository, IMapper mapper, LinkGenerator linkGenerator, PlotPartValidator validator, ILoggerService logger)
        {
            PlotPartRepository = plotPartRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
            Logger = logger;
        }

        /// <summary>
        /// Getting all instances of plot parts for given filter/s.
        /// </summary>
        /// <param name="plotPartClass">Plot part class (ex. III)</param>
        /// <param name="plotProtectedZone">Plot part protected zone (ex. 4)</param>
        /// <returns>List of plot parts.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotPartDto>>> GetPlotPartsAsync(string plotPartClass, string plotProtectedZone)
        {
            List<PlotPart> plotParts = await PlotPartRepository.GetPlotPartsAsync(plotPartClass, plotProtectedZone);
            
            if(plotParts == null || plotParts.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part list is empty!", "Plot microservice", "GetPlotPartsAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part list successfully returned!", "Plot microservice", "GetPlotPartsAsync");
            return Ok(Mapper.Map<List<PlotPartDto>>(plotParts));
        }

        /// <summary>
        /// Getting plot part by given GUID of plot part as parameter.
        /// </summary>
        /// <param name="plotPartId"></param>
        /// <returns>Single plot part.</returns>
        [HttpGet("{plotPartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotPartDto>> GetPlotPartByIdAsync(Guid plotPartId)
        {
            PlotPart plotPart = await PlotPartRepository.GetPlotPartByIdAsync(plotPartId);

            if(plotPart == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part not found!", "Plot microservice", "GetPlotPartByIdAsync");
                return NotFound();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part found and successfully returned!", "Plot microservice", "GetPlotPartByIdAsync");
            return Ok(Mapper.Map<PlotPartDto>(plotPart));
        }

        /// <summary>
        /// Getting plot parts by given GUID of plot as parameter.
        /// </summary>
        /// <param name="plotId"></param>
        /// <returns>List of plot parts.</returns>
        [HttpGet("plot/{plotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotPartDto>>> GetPlotPartsByPlotIdAsync(Guid plotId)
        {
            List<PlotPart> plotParts = await PlotPartRepository.GetPlotPartsByPlotIdAsync(plotId);

            if(plotParts == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part list is empty!", "Plot microservice", "GetPlotPartsByPlotIdAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part list successfully returned!", "Plot microservice", "GetPlotPartsByPlotIdAsync");
            return Ok(Mapper.Map<List<PlotPartDto>>(plotParts));
        }

        /// <summary>
        /// Creating new plot part.
        /// </summary>
        /// <param name="plotPartCreation"></param>
        /// <returns>Confirmation of created plot part.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-parts \
        /// {   \
        ///     "plotPartNumber": "97/1", \
        ///     "plotId": "5f37ba98-ca19-4c9e-8914-708e38bba8bf", \
        ///     "plotPartClassId": "3a3e6366-3a20-4d3b-ae15-be85ba277683", \
        ///     "plotPartProtectedZoneId": "4debaa6a-1a2f-43e0-bb82-1b7ca1824318", \
        ///     "plotPartFormOfOwnershipId": "07af89f2-feee-4680-b489-9d0e31699588", \
        ///     "plotPartSurfaceArea": 100, \
        ///     "plotPartCurrentClass": "", \
        ///     "plotPartCurrentProtectedZone": "" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartConfirmationDto>> CreatePlotPartAsync([FromBody] PlotPartCreationDto plotPartCreation)
        {
            try
            {
                PlotPart plotPart = Mapper.Map<PlotPart>(plotPartCreation);

                Validator.ValidateAndThrow(plotPart);
                
                PlotPartConfirmation plotPartConfirmation = await PlotPartRepository.CreatePlotPartAsync(plotPart);

                await PlotPartRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlotParts", "PlotPart", new { plotPartId = plotPartConfirmation.PlotPartId });

                await Logger.LogMessage(LogLevel.Information, "Plot part successfully created!", "Plot microservice", "CreatePlotPartAsync");
                return Created(uri, Mapper.Map<PlotPartConfirmationDto>(plotPartConfirmation));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part object failed!", "Plot microservice", "CreatePlotPartAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part object creation failed!", "Plot microservice", "CreatePlotPartAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot part by given GUID.
        /// </summary>
        /// <param name="plotPartUpdate"></param>
        /// <returns>Updated plot part.</returns>
        /// <remarks> 
        /// Example of PUT request \
        /// PUT /api/plot-parts \
        /// {   \
        ///     "plotPartId": "a11d93e1-5a89-4821-5426-08d9eaeb3bdb", \
        ///     "plotPartNumber": "97/1", \
        ///     "plotId": "5f37ba98-ca19-4c9e-8914-708e38bba8bf", \
        ///     "plotPartClassId": "3a3e6366-3a20-4d3b-ae15-be85ba277683", \
        ///     "plotPartProtectedZoneId": "4debaa6a-1a2f-43e0-bb82-1b7ca1824318", \
        ///     "plotPartFormOfOwnershipId": "07af89f2-feee-4680-b489-9d0e31699588", \
        ///     "plotPartSurfaceArea": 900 , \
        ///     "plotPartCurrentClass": "Updated class", \
        ///     "plotPartCurrentProtectedZone": "Updated protected zone" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartDto>> UpdatePlotPartAsync(PlotPartUpdateDto plotPartUpdate)
        {
            try
            {
                PlotPart existingPlotPart = await PlotPartRepository.GetPlotPartByIdAsync(plotPartUpdate.PlotPartId);

                if(existingPlotPart == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part object not found!", "Plot microservice", "UpdatePlotPartAsync");
                    return NotFound();
                }

                PlotPart plotPart = Mapper.Map<PlotPart>(plotPartUpdate);

                Validator.ValidateAndThrow(plotPart);
                
                Mapper.Map(plotPart, existingPlotPart);

                await PlotPartRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part object updated successfully!", "Plot microservice", "UpdatePlotPartAsync");
                return Ok(Mapper.Map<PlotPartDto>(existingPlotPart));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part object failed!", "Plot microservice", "UpdatePlotPartAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part object updating failed!", "Plot microservice", "UpdatePlotPartAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///  Deleting plot part by given GUID.
        /// </summary>
        /// <param name="plotPartId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotPartId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotPartAsync(Guid plotPartId)
        {
            try
            {
                PlotPart plotPart = await PlotPartRepository.GetPlotPartByIdAsync(plotPartId);

                if (plotPart == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part object not found!", "Plot microservice", "DeletePlotPartAsync");
                    return NotFound();
                }

                await PlotPartRepository.DeletePlotPartAsync(plotPartId);
                await PlotPartRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part object deleted successfully!", "Plot microservice", "DeletePlotPartAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part object deletion failed!", "Plot microservice", "DeletePlotPartAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotPartOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotPartOptions");

            return Ok();
        }
    }
}
