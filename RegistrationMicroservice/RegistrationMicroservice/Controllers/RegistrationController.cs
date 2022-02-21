using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RegistrationMicroservice.Data;
using RegistrationMicroservice.Entities;
using RegistrationMicroservice.Models;
using RegistrationMicroservice.Services;
using RegistrationMicroservice.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Controllers
{
    [ApiController]
    [Route("api/Registrations")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository registrationRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly RegistrationValidator validator;
        private readonly IService<AuctionDto> auctionMock;
        private readonly IService<BuyerDto> buyerMock;

        public RegistrationController(IRegistrationRepository registrationRepository, IMapper mapper, LinkGenerator linkGenerator, RegistrationValidator validator, IService<AuctionDto> auctionMock, IService<BuyerDto> buyerMock)
        {
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.validator = validator;
            this.auctionMock = auctionMock;
            this.buyerMock = buyerMock;
        }

        /// <summary>
        /// Returns list of all registrations.
        /// </summary>
        /// <returns>List of registrations</returns>
        /// <response code="200">Returns list of registrations</response>
        /// <response code="404">No registrations found</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<List<RegistrationDto>>> GetRegistrationsAsync()
        {
            var registrations = await registrationRepository.GetRegistrationsAsync();

            if(registrations == null || registrations.Count == 0)
            {
                return NoContent();
            }

            List<RegistrationDto> registrationDtos = new List<RegistrationDto>();
            

            foreach(var registration in registrations)
            {
                RegistrationDto registrationDto = mapper.Map<RegistrationDto>(registration);

                if (registration.BuyerId is not null)
                {
                    var buyerDto = await buyerMock.SendGetRequestAsync();
                    

                    if (buyerDto is not null)
                    {
                        registrationDto.BuyerDto = buyerDto;
                    }
                }

                if (registration.AuctionId is not null)
                {
                    var auctionDto = await auctionMock.SendGetRequestAsync();


                    if (auctionDto is not null)
                    {
                        registrationDto.AuctionDto = auctionDto;
                    }
                }
                registrationDtos.Add(registrationDto);
            }




            //return Ok(mapper.Map<List<RegistrationDto>>(registrations));
            return Ok(registrationDtos);
        }

        /// <summary>
        /// Returns registration by ID 
        /// </summary>
        /// <param name="RegistrationId"></param>
        /// <returns>Registration by ID</returns>
        ///<response code="200">returns found registration</response>
        ///<response code="404">no registration found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{RegistrationId}")]
        public async Task<ActionResult<RegistrationDto>> GetRegistrationByIdAsync(Guid RegistrationId)
        {
            var registration = await registrationRepository.GetRegistrationByIdAsync(RegistrationId);

            if(registration == null)
            {
                return NotFound();
            }

            RegistrationDto registrationDto = new RegistrationDto();
            AuctionDto auction = await auctionMock.SendGetRequestAsync();
            BuyerDto buyer = await buyerMock.SendGetRequestAsync();

            registrationDto = mapper.Map<RegistrationDto>(registration);
            registrationDto.AuctionDto = auction;
            registrationDto.BuyerDto = buyer;

            return Ok(registrationDto);
            //return Ok(mapper.Map<RegistrationDto>(registration));
        }


        /// <summary>
        /// Creates new registration
        /// </summary>
        /// <param name="registrationCreate">Auction model</param>
        /// <returns>Creation confirmation</returns>
        /// <remarks>
        /// Example of registration creation model \
        /// POST /api/Registrations \
        /// {       \
        ///      "Date": "2023-01-01T00:00:00", \
        ///      "Location" : "Centar, Novi Sad",\
        ///      "AuctionId": "6a421c13-a195-48f7-8dbd-67596c3974c0",\
        ///      "BuyerId": "6a421c13-a195-48f7-8dbd-67596c3974c0" \
        ///
        /// }
        /// </remarks>
        /// <response code="200">returns created auction</response>
        /// <response code="500">There's been server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegistrationConfirmationDto>> CreateRegistrationAsync([FromBody] RegistrationCreateDto registrationCreate)
        {
            try
            {
                Registration registration = mapper.Map<Registration>(registrationCreate);

                validator.ValidateAndThrow(registration);

                RegistrationConfirmation registrationConfirmation = await registrationRepository.CreateRegistrationAsync(registration);
                await registrationRepository.SaveChangesAsync();

                string location = linkGenerator.GetPathByAction("GetRegistrations", "Registration", new { RegistrationId = registrationConfirmation.RegistrationId });

                return Created(location, mapper.Map<RegistrationConfirmationDto>(registrationConfirmation));
            }
            catch(ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Updates one registration 
        /// </summary>
        /// <param name="registrationUpdate">Registration model that is updated</param>
        /// <returns>Update confirmation model</returns>
        /// <response code="200">Returns updated registration</response>
        /// <response code="400">Registration not found</response>
        /// <response code="500">There has been internal server error</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegistrationDto>> UpdateRegistrationAsync(RegistrationUpdateDto registrationUpdate)
        {
            try
            {
                var oldRegistration = await registrationRepository.GetRegistrationByIdAsync(registrationUpdate.RegistrationId);

                if(oldRegistration == null)
                {
                    return NotFound();
                }

                Registration registration = mapper.Map<Registration>(registrationUpdate);

                validator.ValidateAndThrow(registration);

                mapper.Map(registration, oldRegistration);

                await registrationRepository.SaveChangesAsync();

                return Ok(mapper.Map<RegistrationDto>(oldRegistration));
            }
            catch (ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Deletes one registration by ID
        /// </summary>
        /// <param name="RegistrationId"></param>
        /// <returns>Status 204</returns>
        /// <response code="204">Registration successesfuly deleted</response>
        /// <response code="404">Registration not found</response>
        /// <response code="500">There has been internal server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{RegistrationId}")]
        public async Task<IActionResult> DeleteRegistrationAsync(Guid RegistrationId)
        {
            try
            {
                var registration = await registrationRepository.GetRegistrationByIdAsync(RegistrationId);

                if(registration == null)
                {
                    return NotFound();
                }

                await registrationRepository.DeleteRegistrationAsync(RegistrationId);

                await registrationRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        /// <summary>
        /// returns possible options to work with registrations
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
