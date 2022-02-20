using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Data.Repositories;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Buyer;
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
    [Route("api/buyers")]
    [Produces("application/json", "application/xml")]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerRepository buyerRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly BuyerValidator validator;
        private readonly IndividualValidator iValidator;
        private readonly LegalEntityValidator leValidator;


        private readonly IIndividualRepository individual;
        private readonly ILegalEntityRepository legalEntity;
        
       
        public BuyerController(IBuyerRepository buyerRepository, IMapper mapper, LinkGenerator linkGenerator,IIndividualRepository individual,ILegalEntityRepository legalEntity, BuyerValidator validator, IndividualValidator iValidator, LegalEntityValidator leValidator)
        {
            this.buyerRepository = buyerRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.individual = individual;
            this.legalEntity = legalEntity;
            this.validator = validator;
            this.iValidator = iValidator;
            this.leValidator = leValidator;
        }
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task <ActionResult<List<BuyerDto>>> GetBuyerAsync()
        {
            List<Individual> individualPersons = await individual.GetIndividualAsync();
            List<LegalEntity> legalEntityPersons =await legalEntity.GetLegalEntityAsync();

            List<Buyer> buyersLegal = legalEntityPersons.ConvertAll(x => (Buyer)x);

            List<Buyer> buyersIndividual = legalEntityPersons.ConvertAll(x => (Buyer)x);

            buyersLegal.AddRange(buyersIndividual);

            foreach (var item in buyersLegal) Console.WriteLine(item);

            if (buyersLegal == null || buyersLegal.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<BuyerDto>>(buyersLegal));
            /*List<Priority> priority = priorityRepository.GetPriority(priorityType);

            if (priority == null || priority.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<PriorityDto>>(priority));*/
        }
        [HttpGet("{buyerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuyerDto>> GetBuyerByIdAsync(Guid buyerID)
        {
            Buyer buyerModel;
            //PAZI
            //buyerModel = await (Buyer)individual.GetIndividualByIdAsync(buyerID);
            buyerModel = await individual.GetIndividualByIdAsync(buyerID);

            if (buyerModel == null)
            {
                //PAZIII!!!
                //buyerModel = await (Buyer)legalEntity.GetLegalEntityByIdAsync(buyerID);
                buyerModel = await legalEntity.GetLegalEntityByIdAsync(buyerID);
            }

            if (buyerModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BuyerDto>(buyerModel));
            /*Priority priority = priorityRepository.GetPriorityById(priorityID);

            if (priority == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PriorityDto>(priority));*/
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<BuyerConfirmationDto>> CreateBuyerAsync([FromBody] BuyerCreationDto buyerCreation)
        {
            try
            {
                Buyer buyer = mapper.Map<Buyer>(buyerCreation);

                validator.ValidateAndThrow(buyer);

                Buyer buyerCreated;

                if (buyer.IsIndividual == true)
                {
                    iValidator.ValidateAndThrow((Individual)buyer);
                    buyerCreated = await individual.CreateIndividualAsync((Individual)buyer);
                }
                else
                {
                    leValidator.ValidateAndThrow((LegalEntity)buyer);
                    buyerCreated = await legalEntity.CreateLegalEntityAsync((LegalEntity)buyer);
                }

                BuyerConfirmation buyerConfirmation = await buyerRepository.CreateBuyerAsync(buyer);
                await buyerRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetBuyer", "Buyer", new { buyerId = buyerConfirmation.buyerID });
                return Created(uri, mapper.Map<BuyerConfirmationDto>(buyerConfirmation));

                
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
        public async Task< ActionResult<BuyerDto>> UpdateBuyerAsync(BuyerUpdateDto buyerUpdate)
        {
            try
            {
                if (buyerUpdate.IsIndividual == true)
                {
                    var oldIndividual =await individual.GetIndividualByIdAsync(buyerUpdate.buyerID);

                    if (oldIndividual == null)
                    {
                        return NotFound();
                    }
                    Buyer buyer = mapper.Map<Buyer>(buyerUpdate);

                    validator.ValidateAndThrow(buyer);

                    Individual individualEntity = (Individual)buyer;

                    iValidator.ValidateAndThrow(individualEntity);

                    mapper.Map(individualEntity, oldIndividual);

                    await individual.SaveChangesAsync();
                    return Ok(mapper.Map<BuyerUpdateDto>(buyer));

                }
                else
                {
                    var oldLegalEntity = await legalEntity.GetLegalEntityByIdAsync(buyerUpdate.buyerID);
                    if (oldLegalEntity == null)
                    {
                        return NotFound();
                    }
                    Buyer buyer = mapper.Map<Buyer>(buyerUpdate);

                    LegalEntity legalEntity1 = (LegalEntity)buyer;

                    leValidator.ValidateAndThrow(legalEntity1);

                    mapper.Map(legalEntity1, oldLegalEntity);

                    await legalEntity.SaveChangesAsync();
                    return Ok(mapper.Map<BuyerUpdateDto>(buyer));
                }


            }
            catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

          
            
        }
        [HttpDelete("{priorityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBuyer(Guid buyerId)
        {
            Buyer buyer;
            try
            {
                buyer =await  legalEntity.GetLegalEntityByIdAsync(buyerId);

                if (buyer == null)
                {
                    buyer = await individual.GetIndividualByIdAsync(buyerId);
                }
                if (buyer == null)
                {
                    return NotFound();
                }

                if (buyer.IsIndividual == true)
                {
                    await individual.DeleteIndividualAsync(buyerId);
                }
                else if (buyer.IsIndividual == false)
                {
                     await legalEntity.DeleteLegalEntityAsync(buyerId);
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetBuyerOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
