using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AuctionMicroservice.Validatiors;

namespace AuctionMicroservice.Controllers
{
    [ApiController]
    [Route("api/Auctions")]
    [Produces("application/json", "application/xml")]
   // [Authorize]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository auctionRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly AuctionValidator auctionValidator;

        public AuctionController(IAuctionRepository auctionRepository, LinkGenerator linkGenerator, IMapper mapper, AuctionValidator auctionValidator)
        {
            this.linkGenerator = linkGenerator;
            this.auctionRepository = auctionRepository;
            this.mapper = mapper;
            this.auctionValidator = auctionValidator;

        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AuctionDto>> GetAuctions()
        {
            var auctions = auctionRepository.GetAuctions();

            if(auctions == null || auctions.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<AuctionDto>>(auctions));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{AuctionId}")]
        public ActionResult<AuctionDto> GetAuctionById(Guid AuctionId)
        {
            var auction = auctionRepository.GetAuctionById(AuctionId);

            if(auction == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AuctionDto>(auction));
        }

        

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<AuctionConformationDto> CreateAuction([FromBody] AuctionCreationDto auctionCreationDto)
        {
            try
            {
                Auction auction = mapper.Map<Auction>(auctionCreationDto);

                auctionValidator.ValidateAndThrow(auction);

                AuctionConfirmation confirmation = auctionRepository.CreateAuction(auction);
                auctionRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAuctions", "Auction", new { AuctionId = confirmation.AuctionId });

                return Created(location, mapper.Map<AuctionConformationDto>(confirmation));
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

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuctionDto> UpdateAuction(AuctionUpdateDto auctionUpdate)
        {
            try
            {
                var oldAuction = auctionRepository.GetAuctionById(auctionUpdate.AuctionId);

                if (oldAuction == null)
                {
                    return NotFound();
                }

                Auction auction = mapper.Map<Auction>(auctionUpdate);

                mapper.Map(auction, oldAuction);

                auctionRepository.SaveChanges();

                return Ok(mapper.Map<AuctionDto>(oldAuction));

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{AuctionId}")]
        public IActionResult DeleteAuction(Guid AuctionId)
        {
            try
            {
                var auction = auctionRepository.GetAuctionById(AuctionId);

                if(auction == null)
                {
                    return NotFound();

                }

                auctionRepository.DeleteAuction(AuctionId);
                auctionRepository.SaveChanges();
                return NoContent();


                
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetAuctionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
