using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Buyer;
using BuyerMicroservice.Models.Individual;
using BuyerMicroservice.Models.LegalEntity;
using BuyerMicroservice.Validators;
using FluentValidation;
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
    [Route("api/buyer-type")]
    [Produces("application/json", "application/xml")]
    public class BuyerTypeController : ControllerBase
    {
        private readonly IBuyerRepository buyerRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IndividualValidator iValidator;
        private readonly LegalEntityValidator leValidator;


        public BuyerTypeController(IBuyerRepository buyerRepository, IMapper mapper, LinkGenerator linkGenerator, IndividualValidator iValidator, LegalEntityValidator leValidator)
        {
            this.buyerRepository = buyerRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.iValidator = iValidator;
            this.leValidator = leValidator;

        }

        [HttpPost("individual")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BuyerConfirmationDto>> CreateIndividualAsync([FromBody] IndividualCreationDto individualCreation)
        {
            try
            {
                Individual individual = mapper.Map<Individual>(individualCreation);

                //validator.ValidateAndThrow(authorizedPerson);

                BuyerConfirmation buyerConfirmation = await buyerRepository.CreateBuyerAsync<IndividualConfirmation>(individual);

                await buyerRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetBuyerById", "Buyer", new { buyerID = buyerConfirmation.buyerID });

                return Created(uri, buyerConfirmation);

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
        [HttpPost("legalEntity")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BuyerConfirmationDto>> CreateLegalEntityAsync([FromBody] LegalEntityCreationDto legalEntityCreation)
        {
            try
            {
                LegalEntity legalEntity = mapper.Map<LegalEntity>(legalEntityCreation);

                //validator.ValidateAndThrow(authorizedPerson);

                BuyerConfirmation buyerConfirmation = await buyerRepository.CreateBuyerAsync<LegalEntityConfirmation>(legalEntity);

                await buyerRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetBuyerById", "Buyer", new { buyerID = buyerConfirmation.buyerID });

                return Created(uri, buyerConfirmation);

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


        [HttpPut("individual")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BuyerDto>> UpdateIndividualAsync(IndividualUpdateDto individualUpdate)
        {
            try
            {
                Buyer existingbuyer = await buyerRepository.GetBuyerByIdAsync(individualUpdate.buyerID);

                if (existingbuyer == null)
                {
                    return NotFound();
                }

                // validator.ValidateAndThrow(contactPerson);

                mapper.Map(individualUpdate, (Individual)existingbuyer);

                await buyerRepository.SaveChangesAsync();

                return Ok(mapper.Map<IndividualDto>(existingbuyer));

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

        [HttpPut("legalEntity")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BuyerDto>> UpdateLegalEntityAsync(LegalEntityUpdateDto legalEntityUpdate)
        {
            try
            {
                Buyer existingbuyer = await buyerRepository.GetBuyerByIdAsync(legalEntityUpdate.buyerID);

                if (existingbuyer == null)
                {
                    return NotFound();
                }

                // validator.ValidateAndThrow(contactPerson);

                mapper.Map(legalEntityUpdate, (LegalEntity)existingbuyer);

                await buyerRepository.SaveChangesAsync();

                return Ok(mapper.Map<LegalEntityDto>(existingbuyer));

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


    }
}
