using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartFormOfOwnershipModel;
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
    /// Plot part form of ownership controller. Gives access to fields and methods of plot part form of ownership.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-part-form-of-ownerships")]
    [Produces("application/json", "application/xml")]
    public class PlotPartFormOfOwnershipController : ControllerBase
    {
        private readonly IPlotPartFormOfOwnershipRepository PlotPartFormOfOwnershipRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotPartFormOfOwnershipValidator Validator;
        private readonly ILoggerService Logger;

        /// <summary>
        /// Plot part form of ownership constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotPartFormOfOwnershipRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        /// <param name="logger"></param>
        public PlotPartFormOfOwnershipController(IPlotPartFormOfOwnershipRepository plotPartFormOfOwnershipRepository, IMapper mapper, LinkGenerator linkGenerator, PlotPartFormOfOwnershipValidator validator, ILoggerService logger)
        {
            PlotPartFormOfOwnershipRepository = plotPartFormOfOwnershipRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
            Logger = logger;
        }

        /// <summary>
        /// Getting all instances of plot part form of ownerships for given filter.
        /// </summary>
        /// <param name="formOfOwnership">Plot part form of ownership (ex. Privatna svojina)</param>
        /// <returns>List of plot part form of onwerships.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotPartFormOfOwnershipDto>>> GetPlotPartFormOfOwnershipsAsync(string formOfOwnership)
        {
            List<PlotPartFormOfOwnership> plotPartFormOfOwnerships = await PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipsAsync(formOfOwnership);

            if(plotPartFormOfOwnerships == null || plotPartFormOfOwnerships.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part form of ownership list is empty!", "Plot microservice", "GetPlotPartFormOfOwnershipsAsync");
                return NoContent();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part form of ownership list successfully returned!", "Plot microservice", "GetPlotPartFormOfOwnershipsAsync");
            return Ok(Mapper.Map<List<PlotPartFormOfOwnershipDto>>(plotPartFormOfOwnerships));
        }

        /// <summary>
        /// Getting plot part form of ownership by given GUID of plot part form of ownership as parameter.
        /// </summary>
        /// <param name="plotPartFormOfOwnershipId"></param>
        /// <returns>Single plot part form of ownership.</returns>
        [HttpGet("{plotPartFormOfOwnershipId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotPartFormOfOwnershipDto>> GetPlotPartFormOfOwnershipByIdAsync(Guid plotPartFormOfOwnershipId)
        {
            PlotPartFormOfOwnership plotPartFormOfOwnership = await PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipByIdAsync(plotPartFormOfOwnershipId);

            if(plotPartFormOfOwnership == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Plot part form of ownership not found!", "Plot microservice", "GetPlotPartFormOfOwnershipByIdAsync");
                return NotFound();
            }

            await Logger.LogMessage(LogLevel.Information, "Plot part form of ownership found and successfully returned!", "Plot microservice", "GetPlotPartFormOfOwnershipByIdAsync");
            return Ok(Mapper.Map<PlotPartFormOfOwnershipDto>(plotPartFormOfOwnership));
        }

        /// <summary>
        /// Creating new plot part form of ownership.
        /// </summary>
        /// <param name="plotPartFormOfOwnershipCreation"></param>
        /// <returns>Confirmation about created plot part form of ownership.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-part-form-of-ownerships \
        /// {   \
        ///     "formOfOwnership": "TestFormOfOwnership" \
        /// }
        ///
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartFormOfOwnershipConfirmationDto>> CreatPlotPartFormOfOwnershipAsync([FromBody] PlotPartFormOfOwnershipCreationDto plotPartFormOfOwnershipCreation)
        {
            try
            {
                PlotPartFormOfOwnership plotPartFormOfOwnership = Mapper.Map<PlotPartFormOfOwnership>(plotPartFormOfOwnershipCreation);

                Validator.ValidateAndThrow(plotPartFormOfOwnership);

                PlotPartFormOfOwnershipConfirmation plotPartFormOfOwnershipConfirmation = await PlotPartFormOfOwnershipRepository.CreatPlotPartFormOfOwnershipAsync(plotPartFormOfOwnership);
                await PlotPartFormOfOwnershipRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlotPartFormOfOwnerships", "PlotPartFormOfOwnership", new { plotPartFormOfOwnershipId = plotPartFormOfOwnershipConfirmation.PlotPartFormOfOwnershipId });

                await Logger.LogMessage(LogLevel.Information, "Plot part form of ownership successfully created!", "Plot microservice", "CreatPlotPartFormOfOwnershipAsync");
                return Created(uri, Mapper.Map<PlotPartFormOfOwnershipConfirmationDto>(plotPartFormOfOwnershipConfirmation));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part form of ownership object failed!", "Plot microservice", "CreatPlotPartFormOfOwnershipAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part form of ownership object creation failed!", "Plot microservice", "CreatPlotPartFormOfOwnershipAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot part form of ownership by given GUID.
        /// </summary>
        /// <param name="plotPartFormOfOwnershipUpdate"></param>
        /// <returns>Updated plot part form of ownership.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT api/plot-part-form-of-ownerships \
        /// {   \
        ///     "plotPartFormOfOwnershipId": "98684996-3154-4ec1-b7e0-08d9e44f163b", \
        ///     "formOfOwnership": "UpdatedFormOfOwnership" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotPartFormOfOwnershipDto>> UpdatePlotPartFormOfOwnershipAsync(PlotPartFormOfOwnershipUpdateDto plotPartFormOfOwnershipUpdate)
        {
            try
            {
                PlotPartFormOfOwnership existingPlotPartFormOfOwnership = await PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipByIdAsync(plotPartFormOfOwnershipUpdate.PlotPartFormOfOwnershipId);

                if(existingPlotPartFormOfOwnership == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part form of ownership object not found!", "Plot microservice", "UpdatePlotPartFormOfOwnershipAsync");
                    return NotFound();
                }

                PlotPartFormOfOwnership plotPartFormOfOwnership = Mapper.Map<PlotPartFormOfOwnership>(plotPartFormOfOwnershipUpdate);

                Validator.ValidateAndThrow(plotPartFormOfOwnership);
                
                Mapper.Map(plotPartFormOfOwnership, existingPlotPartFormOfOwnership);

                await PlotPartFormOfOwnershipRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part form of ownership object updated successfully!", "Plot microservice", "UpdatePlotPartFormOfOwnershipAsync");
                return Ok(Mapper.Map<PlotPartFormOfOwnershipDto>(existingPlotPartFormOfOwnership));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot part form of ownership object failed!", "Plot microservice", "UpdatePlotPartFormOfOwnershipAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part form of ownership object updating failed!", "Plot microservice", "UpdatePlotPartFormOfOwnershipAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleting plot part form of ownership by given GUID.
        /// </summary>
        /// <param name="plotPartFormOfOwnershipId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotPartFormOfOwnershipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotPartFormOfOwnershipAsync(Guid plotPartFormOfOwnershipId)
        {
            try
            {
                PlotPartFormOfOwnership plotPartFormOfOwnership = await PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipByIdAsync(plotPartFormOfOwnershipId);

                if(plotPartFormOfOwnership == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Plot part form of ownership object not found!", "Plot microservice", "DeletePlotPartFormOfOwnershipAsync");
                    return NotFound();
                }

                await PlotPartFormOfOwnershipRepository.DeletePlotPartFormOfOwnershipAsync(plotPartFormOfOwnershipId);
                await PlotPartFormOfOwnershipRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot part form of ownership object deleted successfully!", "Plot microservice", "DeletePlotPartFormOfOwnershipAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot part form of ownership object deletion failed!", "Plot microservice", "DeletePlotPartFormOfOwnershipAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotPartFormOfOwnershipOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotPartFormOfOwnershipOptions");

            return Ok();
        }
    }
}
