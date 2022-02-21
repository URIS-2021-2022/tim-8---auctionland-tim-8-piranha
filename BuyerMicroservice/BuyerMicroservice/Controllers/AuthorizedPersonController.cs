using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.AuthorizedPerson;
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
    [Route("api/authorized-persons")]
    [Produces("application/json", "application/xml")]
    public class AuthorizedPersonController : ControllerBase
    {
        private readonly IAuthorizedPersonRepository authorizedPersonRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly AuthorizedPersonValidator validator;

       

        public AuthorizedPersonController(IAuthorizedPersonRepository authorizedPersonRepository, IMapper mapper, LinkGenerator linkGenerator, AuthorizedPersonValidator validator)
        {
            this.authorizedPersonRepository = authorizedPersonRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;

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
                return NoContent();
            }

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
                return NotFound();
            }

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

                return Created(uri, mapper.Map<AuthorizedPersonConfirmationDto>(authorizedPersonConfirmation));

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
        public async  Task<ActionResult<AuthorizedPersonDto>> UpdateAuthorizedPersonAsync(AuthorizedPersonUpdateDto authorizedPersonUpdate)
        {
            try
            {
                AuthorizedPerson existingAuthorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(authorizedPersonUpdate.authorizedPersonID);

                if (existingAuthorizedPerson == null)
                {
                    return NotFound();
                }

                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonUpdate);

                validator.ValidateAndThrow(authorizedPerson);

                mapper.Map(authorizedPerson, existingAuthorizedPerson);

                await authorizedPersonRepository.SaveChangesAsync();

                return Ok(mapper.Map<AuthorizedPersonDto>(existingAuthorizedPerson));

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
                    return NotFound();
                }

                await authorizedPersonRepository.DeleteAuthorizedPersonAsync(authorizedPersonId);
                await authorizedPersonRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetAuthorizedPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
