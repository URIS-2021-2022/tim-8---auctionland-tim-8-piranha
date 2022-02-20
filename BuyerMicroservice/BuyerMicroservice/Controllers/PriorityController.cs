using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Priority;
using BuyerMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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

        public PriorityController(IPriorityRepository priorityRepository, IMapper mapper, LinkGenerator linkGenerator, PriorityValidator validator)
        {
            this.priorityRepository = priorityRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;

        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PriorityDto>>> GetPriorityAsync(string priorityType = null)
        {
            List<Priority> priority = await priorityRepository.GetPriorityAsync(priorityType);

            if (priority == null || priority.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<PriorityDto>>(priority));
        }

        [HttpGet("{priorityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task < ActionResult<PriorityDto>> GetPriorityByIdAsync(Guid priorityID)
        {
            Priority priority = await priorityRepository.GetPriorityByIdAsync(priorityID);

            if (priority == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PriorityDto>(priority));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<PriorityConfirmationDto>> CreatePriorityAsync([FromBody] PriorityCreationDto priorityCreation)
        {
            try
            {
                Priority priority = mapper.Map<Priority>(priorityCreation);

                validator.ValidateAndThrow(priority);

                PriorityConfirmation priorityConfirmation = await priorityRepository.CreatePriorityAsync(priority);
                await priorityRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetPriority", "Priority", new { priorityId = priorityConfirmation.priorityID });

                return Created(uri, mapper.Map<PriorityConfirmationDto>(priorityConfirmation));

            }
            catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PriorityDto>> UpdatePriority(PriorityUpdateDto priorityUpdate)
        {
            try
            {
                Priority existingPriority =await priorityRepository.GetPriorityByIdAsync(priorityUpdate.priorityID);

                if (existingPriority == null)
                {
                    return NotFound();
                }

                Priority priority = mapper.Map<Priority>(priorityUpdate);

                validator.ValidateAndThrow(priority);

                mapper.Map(priority, existingPriority);

                await priorityRepository.SaveChangesAsync();

                return Ok(mapper.Map<PriorityDto>(existingPriority));

            }
            catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{priorityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< IActionResult> DeletePriorityAsync(Guid priorityId)
        {
            try
            {
                Priority priority = await priorityRepository.GetPriorityByIdAsync(priorityId);

                if (priority == null)
                {
                    return NotFound();
                }

                await priorityRepository.DeletePriorityAsync(priorityId);
               await priorityRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPriorityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
