using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Priority;
using BuyerMicroservice.ServiceCalls;
using BuyerMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Controllers
{
    [ApiController]
    [Route("api/prioritys")]
    [Produces("application/json", "application/xml")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityRepository priorityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly PriorityValidator validator;
        private readonly ILoggerService logger;

        public PriorityController(IPriorityRepository priorityRepository, IMapper mapper, LinkGenerator linkGenerator, PriorityValidator validator, ILoggerService logger)
        {
            this.priorityRepository = priorityRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger = logger;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<List<PriorityDto>>> GetPriorityAsync(string priorityType = null)
        {
            List<Priority> priority = await priorityRepository.GetPriorityAsync(priorityType);

            if (priority == null || priority.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Priority culture list is empty!", "Buyer microservice", "GetPriorityAsync");

                return NoContent();
            }
            await logger.LogMessage(LogLevel.Information, "Priority list successfully returned!", "Buyer microservice", "GetPriorityAsync");

            return Ok(mapper.Map<List<PriorityDto>>(priority));
        }

        [HttpGet("{priorityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task < ActionResult<PriorityDto>> GetPriorityByIdAsync(Guid priorityID)
        {
            Priority priority = await priorityRepository.GetPriorityByIdAsync(priorityID);

            if (priority == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Priority not found!", "Buyer microservice", "GetContactPersonByIdAsync");

                return NotFound();
            }

            await logger.LogMessage(LogLevel.Information, "Priority found and successfully returned!", "Buyer microservice", "GetContactPersonByIdAsync");

            return Ok(mapper.Map<PriorityDto>(priority));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task< ActionResult<PriorityConfirmationDto>> CreatePriorityAsync([FromBody] PriorityCreationDto priorityCreation)
        {
            try
            {
                Priority priority = mapper.Map<Priority>(priorityCreation);

                validator.ValidateAndThrow(priority);

                PriorityConfirmation priorityConfirmation = await priorityRepository.CreatePriorityAsync(priority);
                await priorityRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetPriority", "Priority", new { priorityId = priorityConfirmation.priorityID });

                await logger.LogMessage(LogLevel.Information, "Priority  successfully created!", "Buyer microservice", "CreatePriorityAsync");

                return Created(uri, mapper.Map<PriorityConfirmationDto>(priorityConfirmation));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Priority object failed!", "Buyer microservice", "CreatePriorityAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Priority object creation failed!", "Buyer microservice", "CreatePriorityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<PriorityDto>> UpdatePriorityAsync(PriorityUpdateDto priorityUpdate)
        {
            try
            {
                Priority existingPriority =await priorityRepository.GetPriorityByIdAsync(priorityUpdate.priorityID);

                if (existingPriority == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Priority object not found!", "Buyer microservice", "UpdatePriorityAsync");

                    return NotFound();
                }

                Priority priority = mapper.Map<Priority>(priorityUpdate);

                validator.ValidateAndThrow(priority);

                mapper.Map(priority, existingPriority);

                await priorityRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Priority object updated successfully!", "Buyer microservice", "UpdatePriorityAsync");

                return Ok(mapper.Map<PriorityDto>(existingPriority));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Priority  object failed!", "Buyer microservice", "UpdatePriorityAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Priority object updating failed!", "Buyer microservice", "UpdatePriorityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{priorityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task< IActionResult> DeletePriorityAsync(Guid priorityId)
        {
            try
            {
                Priority priority = await priorityRepository.GetPriorityByIdAsync(priorityId);

                if (priority == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Priority object not found!", "Buyer microservice", "DeletePriorityAsync");

                    return NotFound();
                }

                await priorityRepository.DeletePriorityAsync(priorityId);
               await priorityRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Priority object deleted successfully!", "Buyer microservice", "DeletePriorityAsync");

                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Priority object deletion failed!", "Buyer microservice", "DeletePriorityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<IActionResult> GetPriorityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetPriorityOptions");

            return Ok();
        }
    }
}
