using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Buyer;
using BuyerMicroservice.Models.Individual;
using BuyerMicroservice.Models.LegalEntity;
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
    [Route("api/buyer-type")]
    [Produces("application/json", "application/xml")]
    public class BuyerTypeController : ControllerBase
    {
        private readonly IBuyerRepository buyerRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IndividualValidator iValidator;
        private readonly LegalEntityValidator leValidator;
        private readonly ILoggerService logger;
        private readonly BuyerValidator validator;


        public BuyerTypeController(IBuyerRepository buyerRepository, IMapper mapper, LinkGenerator linkGenerator, IndividualValidator iValidator, LegalEntityValidator leValidator, ILoggerService logger, BuyerValidator validator)
        {
            this.buyerRepository = buyerRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.iValidator = iValidator;
            this.leValidator = leValidator;
            this.logger = logger;
            this.validator = validator;

        }
        /// <summary>
        /// Kreira novo fizicko lice
        /// </summary>
        /// <param name="individualCreation"> model fizickog lica</param>
        /// <returns>Potvrda o kreiranom fizickom licu</returns>
        /// <remarks>
        /// POST /api/buyer-type \
        /// </remarks>
        /// <response code = "201">Vraća kreirano fizicko lice</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja fizickog lica</response>
        [HttpPost("individual")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<BuyerConfirmationDto>> CreateIndividualAsync([FromBody] IndividualCreationDto individualCreation)
        {
            try
            {
                Individual individual = mapper.Map<Individual>(individualCreation);

                iValidator.ValidateAndThrow(individual);

                BuyerConfirmation buyerConfirmation = await buyerRepository.CreateBuyerAsync<IndividualConfirmation>(individual);

                await buyerRepository.SaveChangesAsync();


                string uri = linkGenerator.GetPathByAction("GetBuyerById", "Buyer", new { buyerID = buyerConfirmation.buyerID });

                await logger.LogMessage(LogLevel.Information, "Idividual successfully created!", "Buyer microservice", "CreateIndividualAsync");
                return Created(uri, buyerConfirmation);

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for individual object failed!", "Buyer microservice", "CreateIndividualAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Individual object creation failed!", "Buyer microservice", "CreateIndividualAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Kreira novo pravno lice
        /// </summary>
        /// <param name="legalEntityCreation"> model pravnog lica</param>
        /// <returns>Potvrda o kreiranom pravnom licu</returns>
        /// <remarks>
        /// POST /api/buyer-type \
        /// </remarks>
        /// <response code = "201">Vraća kreirano pravno lice</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja pravnog lica</response>
        [HttpPost("legalEntity")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<BuyerConfirmationDto>> CreateLegalEntityAsync([FromBody] LegalEntityCreationDto legalEntityCreation)
        {
            try
            {
                LegalEntity legalEntity = mapper.Map<LegalEntity>(legalEntityCreation);

                validator.ValidateAndThrow(legalEntity);
                 leValidator.ValidateAndThrow(legalEntity);

                BuyerConfirmation buyerConfirmation = await buyerRepository.CreateBuyerAsync<LegalEntityConfirmation>(legalEntity);

                await buyerRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetBuyerById", "Buyer", new { buyerID = buyerConfirmation.buyerID });

                await logger.LogMessage(LogLevel.Error, "Legal entity object creation failed!", "Buyer microservice", "CreateLegalEntityAsync");
                return Created(uri, buyerConfirmation);

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Legal entity for individual object failed!", "Buyer microservice", "CreateIndividualAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Legal entity object creation failed!", "Buyer microservice", "CreateIndividualAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedno fizicko lice
        /// </summary>
        /// <param name="individualUpdate">Model fizickog lica koji se ažurira</param>
        /// <returns>Potvrda o ažuriranom fizickom licu</returns>
        /// <response code="200">Vraća ažurirano fizicko lice</response>
        /// <response code="404">Nije pronađeno fizicko lice za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja fizickog lica</response>
        [HttpPut("individual")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<BuyerDto>> UpdateIndividualAsync(IndividualUpdateDto individualUpdate)
        {
            try
            {
                Buyer existingbuyer = await buyerRepository.GetBuyerByIdAsync(individualUpdate.buyerID);

                if (existingbuyer == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Individual object not found!", "Buyer microservice", "UpdateIndividualAsync");
                    return NotFound();
                }
                validator.ValidateAndThrow(existingbuyer);

                iValidator.ValidateAndThrow((Individual)existingbuyer);


                mapper.Map(individualUpdate, (Individual)existingbuyer);

                await buyerRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Individual object updated successfully!", "Buyer microservice", "UpdateIndividualAsync");


                return Ok(mapper.Map<IndividualDto>(existingbuyer));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for individual object failed!", "Buyer microservice", "UpdateIndividualAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Individual object updating failed!", "Buyer microservice", "UpdateIndividualAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Ažurira jedno pravno lice
        /// </summary>
        /// <param name="legalEntityUpdate">Model pravnog lica  koje se ažurira</param>
        /// <returns>Potvrda o ažuriranom pravnom licu</returns>
        /// <response code="200">Vraća ažurirano pravno lice</response>
        /// <response code="404">Nije pronađena pravno lice za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja pravnog lica</response>
        [HttpPut("legalEntity")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<BuyerDto>> UpdateLegalEntityAsync(LegalEntityUpdateDto legalEntityUpdate)
        {
            try
            {
                Buyer existingbuyer = await buyerRepository.GetBuyerByIdAsync(legalEntityUpdate.buyerID);

                if (existingbuyer == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "LegalEntity object not found!", "Buyer microservice", "UpdateLegalEntityAsync");
                    return NotFound();
                }
                validator.ValidateAndThrow(existingbuyer);

                leValidator.ValidateAndThrow((LegalEntity)existingbuyer);

                mapper.Map(legalEntityUpdate, (LegalEntity)existingbuyer);

                await buyerRepository.SaveChangesAsync();

                return Ok(mapper.Map<LegalEntityDto>(existingbuyer));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for legal entity object failed!", "Buyer microservice", "UpdateLegalEntityAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "LegalEntity object updating failed!", "Buyer microservice", "UpdateLegalEntityAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve kupce
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<IActionResult> GetBuyerOptions()
        {
            Response.Headers.Add("Allow", "POST,PUT");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetBuyerOptions");
            return Ok();
        }


    }
}
