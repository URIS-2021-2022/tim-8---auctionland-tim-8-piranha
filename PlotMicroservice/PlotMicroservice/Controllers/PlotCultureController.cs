using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotCultureModel;
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
    /// Plot culutre controller. Gives access to fields and methods of plot culuture.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-cultures")]
    [Produces("application/json", "application/xml")]
    public class PlotCultureController : ControllerBase
    {
        private readonly IPlotCultureRepository PlotCultureRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotCultureValidator Validator;
        private readonly ILoggerService Logger;

        /// <summary>
        /// Plot culture constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotCultureRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        /// <param name="logger"></param>
        public PlotCultureController(IPlotCultureRepository plotCultureRepository, IMapper mapper, LinkGenerator linkGenerator, PlotCultureValidator validator, ILoggerService logger)
        {
            PlotCultureRepository = plotCultureRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
            Logger = logger;
        }

        /// <summary>
        /// Getting all instances of plot cultures for given filter.
        /// </summary>
        /// <param name="plotCulture">Plot culture (ex. Voćnjaci)</param>
        /// <returns>List of plot cultures.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotCultureDto>>> GetPlotCulturesAsync(string plotCulture)
        {
            List<PlotCulture> plotCultures = await PlotCultureRepository.GetPlotCulturesAsync(plotCulture);
            
            if(plotCultures == null || plotCultures.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot culture list is empty!", "Plot microservice", "GetPlotCulturesAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot culture list successfully returned!", "Plot microservice", "GetPlotCulturesAsync");
            return Ok(Mapper.Map<List<PlotCultureDto>>(plotCultures));
        }

        /// <summary>
        /// Getting plot culture by given GUID of plot culture as parameter.
        /// </summary>
        /// <param name="plotCultureId"></param>
        /// <returns>Single plot culture.</returns>
        [HttpGet("{plotCultureId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotCultureDto>> GetPlotCultureByIdAsync(Guid plotCultureId)
        {
            PlotCulture plotCulture = await PlotCultureRepository.GetPlotCultureByIdAsync(plotCultureId);

            if(plotCulture == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot culture not found!", "Plot microservice", "GetPlotCultureByIdAsync");
                return NotFound();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot culture found and successfully returned!", "Plot microservice", "GetPlotCultureByIdAsync");
            return Ok(Mapper.Map<PlotCultureDto>(plotCulture));
        }

        /// <summary>
        /// Creating new plot culture.
        /// </summary>
        /// <param name="plotCultureCreation"></param>
        /// <returns>Confirmation about created plot culture.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-cultures \
        /// {   \
        ///     "culture": "New plot culture" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotCultureConfirmationDto>> CreatePlotCultureAsync([FromBody] PlotCultureCreationDto plotCultureCreation)
        {
            try
            {

                PlotCulture plotCulture = Mapper.Map<PlotCulture>(plotCultureCreation);

                Validator.ValidateAndThrow(plotCulture);

                PlotCultureConfirmation plotCultureConfirmation = await PlotCultureRepository.CreatePlotCultureAsync(plotCulture);
                
                await PlotCultureRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlotCultures", "PlotCulture", new { plotCultureId = plotCultureConfirmation.PlotCultureId });

                await Logger.LogMessage(LogLevel.Information, "Plot culture successfully created!", "Plot microservice", "CreatePlotCultureAsync");
                return Created(uri, Mapper.Map<PlotCultureConfirmationDto>(plotCultureConfirmation));

            } catch(ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot culture object failed!", "Plot microservice", "CreatePlotCultureAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            } catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot culture object creation failed!", "Plot microservice", "CreatePlotCultureAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot culuture by given GUID.
        /// </summary>
        /// <param name="plotCultureUpdate"></param>
        /// <returns>Updated plot culture.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT /api/plot-cultures \
        /// {   \
        ///     "plotCultureId": "260c190d-07b9-426b-e58b-08d9e36dc042",
        ///     "culture": "Updated culture"
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotCultureDto>> UpdatePlotCultureAsync(PlotCultureUpdateDto plotCultureUpdate)
        {
            try
            {

                PlotCulture existingPlotCulture = await PlotCultureRepository.GetPlotCultureByIdAsync(plotCultureUpdate.PlotCultureId);

                if (existingPlotCulture == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot culture object not found!", "Plot microservice", "UpdatePlotCultureAsync");
                    return NotFound();
                }

                PlotCulture plotCulture = Mapper.Map<PlotCulture>(plotCultureUpdate);

                Validator.ValidateAndThrow(plotCulture);

                Mapper.Map(plotCulture, existingPlotCulture);

                await PlotCultureRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot culture object updated successfully!", "Plot microservice", "UpdatePlotCultureAsync");
                return Ok(Mapper.Map<PlotCultureDto>(existingPlotCulture));

            } catch(ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot culture object failed!", "Plot microservice", "UpdatePlotCultureAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot culture object updating failed!", "Plot microservice", "UpdatePlotCultureAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleting plot culture by given GUID.
        /// </summary>
        /// <param name="plotCultureId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotCultureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotCultureAsync(Guid plotCultureId)
        {
            try
            {
                PlotCulture plotCulture = await PlotCultureRepository.GetPlotCultureByIdAsync(plotCultureId);

                if (plotCulture == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot object not found!", "Plot microservice", "DeletePlotCultureAsync");
                    return NotFound();
                }

                await PlotCultureRepository.DeletePlotCultureAsync(plotCultureId);
                await PlotCultureRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot culture object deleted successfully!", "Plot microservice", "DeletePlotCultureAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot culture object deletion failed!", "Plot microservice", "DeletePlotCultureAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotCultureOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotCultureOptions");

            return Ok();
        }
    }
}
