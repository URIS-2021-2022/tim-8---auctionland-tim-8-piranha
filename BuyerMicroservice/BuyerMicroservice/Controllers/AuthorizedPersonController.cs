using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.AuthorizedPerson;
using BuyerMicroservice.Models.AuthorizedPersonBuyer;
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
    [Route("api/authorized-persons")]
    [Produces("application/json", "application/xml")]
    public class AuthorizedPersonController : ControllerBase
    {
        private readonly IAuthorizedPersonRepository authorizedPersonRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly AuthorizedPersonValidator validator;
        private readonly IBuyerRepository buyerRepository;
        private readonly ILoggerService logger;



        public AuthorizedPersonController(IAuthorizedPersonRepository authorizedPersonRepository, IMapper mapper, LinkGenerator linkGenerator, AuthorizedPersonValidator validator, IBuyerRepository buyerRepository, ILoggerService logger)
        {
            this.authorizedPersonRepository = authorizedPersonRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.buyerRepository = buyerRepository;
            this.logger = logger;

        }


        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<AuthorizedPersonDto>>> GetAuthorizedPersonAsync(string personalDocNum = null)
        {
            List<AuthorizedPerson> authorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonAsync(personalDocNum);

            if (authorizedPerson == null || authorizedPerson.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Authorized person culture list is empty!", "Buyer microservice", "GetAuthorizedPersonAsync");

                return NoContent();
            }
          await logger.LogMessage(LogLevel.Information, "Authorized person list successfully returned!", "Buyer microservice", "GetAuthorizedPersonAsync");

            return Ok(mapper.Map<List<AuthorizedPersonDto>>(authorizedPerson));
        }

        [HttpGet("{authorizedPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorizedPersonDto>> GetAuthorizedPersonByIdAsync(Guid AuthorizedPersonID)
        {
            AuthorizedPerson authorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(AuthorizedPersonID);

            if (authorizedPerson == null)
            {
              await logger.LogMessage(LogLevel.Warning, "Authorized person not found!", "Buyer microservice", "GetAuthorizedPersonByIdAsync");

                return NotFound();
            }
            await logger.LogMessage(LogLevel.Information, "Authorized person found and successfully returned!", "Buyer microservice", "GetAuthorizedPersonByIdAsync");

            return Ok(mapper.Map<AuthorizedPersonDto>(authorizedPerson));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<AuthorizedPersonConfirmationDto>> CreateAuthorizedPersonAsync([FromBody] AuthorizedPersonCreationDto authorizedPersonCreation)
        {
            try
            {
                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonCreation);

                validator.ValidateAndThrow(authorizedPerson);


                AuthorizedPersonConfirmation authorizedPersonConfirmation =await authorizedPersonRepository.CreateAuthorizedPersonAsync(authorizedPerson);
                await authorizedPersonRepository.SaveChangesAsync();

                
                string uri = linkGenerator.GetPathByAction("GetAuthorizedPerson", "AuthorizedPerson", new { authorizedPersonId = authorizedPersonConfirmation.authorizedPersonID });
               
               await logger.LogMessage(LogLevel.Information, "Authorized person  successfully created!", "Buyer microservice", "CreateAuthorizedPersonAsync");

                return Created(uri, mapper.Map<AuthorizedPersonConfirmationDto>(authorizedPersonConfirmation));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for authorized person object failed!", "Buyer microservice", "CreateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            }
            catch (Exception ex)
            {
               await logger.LogMessage(LogLevel.Error, "Authorized Person object creation failed!", "Buyer microservice", "CreateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<ActionResult<AuthorizedPersonDto>> UpdateAuthorizedPersonAsync(AuthorizedPersonUpdateDto authorizedPersonUpdate)
        {
            try
            {
                AuthorizedPerson existingAuthorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(authorizedPersonUpdate.authorizedPersonID);

                if (existingAuthorizedPerson == null)
                {
                   await logger.LogMessage(LogLevel.Warning, "Authorized Person object not found!", "Buyer microservice", "UpdateAuthorizedPersonAsync");

                    return NotFound();
                }

                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonUpdate);

                validator.ValidateAndThrow(authorizedPerson);

                mapper.Map(authorizedPerson, existingAuthorizedPerson);

                await authorizedPersonRepository.SaveChangesAsync();
               await logger.LogMessage(LogLevel.Information, "Authorized person  object updated successfully!", "Buyer microservice", "UpdateAuthorizedPersonAsync");

                return Ok(mapper.Map<AuthorizedPersonDto>(existingAuthorizedPerson));

            }
            catch (ValidationException ve)
            {
               await logger.LogMessage(LogLevel.Error, "Validation for Authorized person  object failed!", "Buyer microservice", "UpdateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
               await logger.LogMessage(LogLevel.Error, "Authorized person object updating failed!", "Buyer microservice", "UpdateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{authorizedPersonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthorizedPersonAsync(Guid authorizedPersonId)
        {
            try
            {
                AuthorizedPerson authorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(authorizedPersonId);

                if (authorizedPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Authorized person object not found!", "Buyer microservice", "DeleteAuthorizedPersonAsync");

                    return NotFound();
                }

                await authorizedPersonRepository.DeleteAuthorizedPersonAsync(authorizedPersonId);
                await authorizedPersonRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Authorized person object deleted successfully!", "Buyer microservice", "DeleteAuthorizedPersonAsync");

                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Authorized erson object deletion failed!", "Buyer microservice", "DeleteAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddBuyer")]
        public async Task<IActionResult> AddAuthorizedPersonBuyer(AuthorizedPersonBuyerDto apbDto)
        {
            Buyer b = await buyerRepository.GetBuyerByIdAsync(apbDto.buyerId);
            await authorizedPersonRepository.AddBuyerToAuthorizedPerson(b, apbDto.authorizedPersonId);
            await authorizedPersonRepository.SaveChangesAsync();

            await logger.LogMessage(LogLevel.Error, "Buyer has been successfully added !", "Buyer microservice", "AddAuthorizedPersonBuyer");

            return NoContent();
        }

        [HttpDelete("DeleteBuyer")]
        public async Task<IActionResult> DeleteAuthorizedPersonBuyer(AuthorizedPersonBuyerDto apbDto)
        {
            Buyer b = await buyerRepository.GetBuyerByIdAsync(apbDto.buyerId);
            await authorizedPersonRepository.RemoveBuyerFromAuthorizedPerson(b, apbDto.authorizedPersonId);
            await authorizedPersonRepository.SaveChangesAsync();

           await logger.LogMessage(LogLevel.Error, "Buyer has been successfully deleted !", "Buyer microservice", "DeleteAuthorizedPersonBuyer");


            return NoContent();
        }

        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthorizedPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

           await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetAuthorizedPersonOptions");

            return Ok();
        }

    }
}
