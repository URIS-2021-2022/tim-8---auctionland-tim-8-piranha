using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.ContactPerson;
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
    [Route("api/contact-persons")]
    [Produces("application/json", "application/xml")]
    public class ContactPersonController : ControllerBase
    {

        private readonly IContactPersonRepository contactPersonRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ContactPersonValidator validator;
        private readonly ILoggerService logger;

        public ContactPersonController(IContactPersonRepository contactPersonRepository, IMapper mapper, LinkGenerator linkGenerator, ContactPersonValidator validator, ILoggerService logger)
        {
            this.contactPersonRepository = contactPersonRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger=logger;
         }
    

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task< ActionResult<List<ContactPersonDto>>> GetContactPersonAsync(string name = null)
        {
            List<ContactPerson> contactPerson =await contactPersonRepository.GetContactPersonAsync(name);

            if (contactPerson == null || contactPerson.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Contact person culture list is empty!", "Buyer microservice", "GetContactPersonAsync");

                return NoContent();
            }
            await logger.LogMessage(LogLevel.Information, "Contact person list successfully returned!", "Buyer microservice", "GetContactPersonAsync");

            return Ok(mapper.Map<List<ContactPersonDto>>(contactPerson));
        }

        [HttpGet("{contactPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<ContactPersonDto>> GetContactPersonByIdAsync(Guid contactPersonID)
        {
            ContactPerson contactPerson = await contactPersonRepository.GetContactPersonByIdAsync(contactPersonID);

            if (contactPerson == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Contact person not found!", "Buyer microservice", "GetContactPersonByIdAsync");

                return NotFound();
            }
            await logger.LogMessage(LogLevel.Information, "Contact person found and successfully returned!", "Buyer microservice", "GetContactPersonByIdAsync");

            return Ok(mapper.Map<ContactPersonDto>(contactPerson));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<ContactPersonConfirmationDto>> CreateContactPersonAsync([FromBody] ContactPersonCreationDto contactPersonCreation)
        {
            try
            {
                ContactPerson contactPerson = mapper.Map<ContactPerson>(contactPersonCreation);

                validator.ValidateAndThrow(contactPerson);

                ContactPersonConfirmation contactPersonConfirmation =await contactPersonRepository.CreateContactPersonAsync(contactPerson);
                await contactPersonRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetContactPerson", "ContactPerson", new { contactPersonId = contactPersonConfirmation.contactPersonID });

                await logger.LogMessage(LogLevel.Information, "Contact person  successfully created!", "Buyer microservice", "CreateContactPersonAsync");

                return Created(uri, mapper.Map<ContactPersonConfirmationDto>(contactPersonConfirmation));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Contact person object failed!", "Buyer microservice", "CreateContactPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contact Person object creation failed!", "Buyer microservice", "CreateContactPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<ContactPersonDto>> UpdateContactPersonAsync(ContactPersonUpdateDto contactPersonUpdate)
        {
            try
            {
                ContactPerson existingContactPerson = await contactPersonRepository .GetContactPersonByIdAsync(contactPersonUpdate.contactPersonID);

                if (existingContactPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contact Person object not found!", "Buyer microservice", "UpdateContactPersonAsync");

                    return NotFound();
                }

                ContactPerson contactPerson = mapper.Map<ContactPerson>(contactPersonUpdate);

                validator.ValidateAndThrow(contactPerson);

                mapper.Map(contactPerson, existingContactPerson);

                await contactPersonRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Contact person  object updated successfully!", "Buyer microservice", "UpdateContactPersonAsync");

                return Ok(mapper.Map<ContactPersonDto>(existingContactPerson));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Contact person  object failed!", "Buyer microservice", "UpdateContactPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Authorized person object updating failed!", "Buyer microservice", "UpdateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{contactPersonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<IActionResult> DeleteContactPersonAsync(Guid contactPersonId)
        {
            try
            {
                ContactPerson contactPerson = await contactPersonRepository.GetContactPersonByIdAsync(contactPersonId);

                if (contactPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contact person object not found!", "Buyer microservice", "DeleteContactPersonAsync");

                    return NotFound();
                }

                await contactPersonRepository.DeleteContactPersonAsync(contactPersonId);
                await contactPersonRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Contact person object deleted successfully!", "Buyer microservice", "DeleteContactPersonAsync");


                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contact person object deletion failed!", "Buyer microservice", "DeleteContactPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public async Task< IActionResult> GetContactPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetContactPersonOptions");

            return Ok();
        }
    }
}
