using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using RegistrationMicroservice.Data;
using RegistrationMicroservice.Entities;
using RegistrationMicroservice.Models;
using RegistrationMicroservice.Services;
using RegistrationMicroservice.Validators;
using System;
using System.Collections.Generic;
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
        private readonly IService<AuctionDto> auctionService;
        private readonly IService<BuyerDto> buyerService;
        private readonly ILoggerService logger;

        public RegistrationController(IRegistrationRepository registrationRepository, IMapper mapper, LinkGenerator linkGenerator, RegistrationValidator validator, IService<AuctionDto> auctionService, IService<BuyerDto> buyerService, ILoggerService logger)
        {
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.validator = validator;
            this.auctionService = auctionService;
            this.buyerService = buyerService;
            this.logger = logger;
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

            if (registrations == null || registrations.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Registration list is empty!", "Registration microservice", "GetRegistrationsAsync");
                return NoContent();
            }

            List<RegistrationDto> registrationDtos = new List<RegistrationDto>();


            foreach (var registration in registrations)
            {
                RegistrationDto registrationDto = mapper.Map<RegistrationDto>(registration);

                if (registration.BuyerId is not null)
                {
                    var buyerDto = await buyerService.SendGetRequestAsync("");


                    if (buyerDto is not null)
                    {
                        registrationDto.BuyerDto = buyerDto;
                    }
                }

                if (registration.AuctionId is not null)
                {
                    var auctionDto = await auctionService.SendGetRequestAsync("");


                    if (auctionDto is not null)
                    {
                        registrationDto.AuctionDto = auctionDto;
                    }
                }
                registrationDtos.Add(registrationDto);
            }




            await logger.LogMessage(LogLevel.Information, "Registration list successfully returned!", "Registration microservice", "GetRegistrationsAsync");
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

            if (registration == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Registration not found!", "Registration microservice", "GetRegistrationByIdAsync");
                return NotFound();
            }

            RegistrationDto registrationDto = mapper.Map<RegistrationDto>(registration);

            if (registration.BuyerId is not null)
            {
                var buyerDto = await buyerService.SendGetRequestAsync("");


                if (buyerDto is not null)
                {
                    registrationDto.BuyerDto = buyerDto;
                }
            }

            if (registration.AuctionId is not null)
            {
                var auctionDto = await auctionService.SendGetRequestAsync("");


                if (auctionDto is not null)
                {
                    registrationDto.AuctionDto = auctionDto;
                }
            }


            await logger.LogMessage(LogLevel.Information, "Registration found and successfully returned!", "Plot microservice", "GetRegistrationByIdAsync");
            return Ok(registrationDto);
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

                await logger.LogMessage(LogLevel.Information, "Registration successfully created!", "Registration microservice", "CreateRegistrationAsync");
                return Created(location, mapper.Map<RegistrationConfirmationDto>(registrationConfirmation));
            }
            catch (ValidationException v)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for registration object failed!", "Registration microservice", "CreateRegistrationAsync");
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                await logger.LogMessage(LogLevel.Error, "Registration object creation failed!", "Registration microservice", "CreateRegistrationAsync");
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

                if (oldRegistration == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Registration object not found!", "Registration microservice", "UpdateRegistrationAsync");
                    return NotFound();
                }

                Registration registration = mapper.Map<Registration>(registrationUpdate);

                validator.ValidateAndThrow(registration);

                mapper.Map(registration, oldRegistration);

                await registrationRepository.SaveChangesAsync();
                await logger.LogMessage(LogLevel.Information, "Registration object updated successfully!", "Registration microservice", "UpdateRegistrationAsync");
                return Ok(mapper.Map<RegistrationDto>(oldRegistration));
            }
            catch (ValidationException v)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for registration object failed!", "Registration microservice", "UpdateRegistrationAsync");
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                await logger.LogMessage(LogLevel.Error, "Registration object updating failed!", "Registration microservice", "UpdateRegistrationAsync");
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

                if (registration == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Registration object not found!", "Registration microservice", "DeleteRegistrationAsync");
                    return NotFound();
                }

                await registrationRepository.DeleteRegistrationAsync(RegistrationId);

                await registrationRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Registration object deleted successfully!", "Registration microservice", "DeleteRegistrationAsync");
                return NoContent();
            }
            catch (Exception e)
            {
                await logger.LogMessage(LogLevel.Error, "Registration object deletion failed!", "Registration microservice", "DeleteRegistrationAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        /// <summary>
        /// returns possible options to work with registrations
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Registration microservice", "GetRegistrationOptions");
            return Ok();
        }
    }
}
