﻿using AuctionMicroservice.Data;
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
using Microsoft.Extensions.Logging;
using AuctionMicroservice.Services;

namespace AuctionMicroservice.Controllers
{
    [ApiController]
    [Route("api/Auctions")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
   // [Authorize]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository auctionRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly AuctionValidator auctionValidator;
        private readonly ILoggerService logger;
        private readonly IService<PublicBiddingDto> publicBiddingService;

        public AuctionController(IAuctionRepository auctionRepository, LinkGenerator linkGenerator, IMapper mapper, AuctionValidator auctionValidator, ILoggerService logger, IService<PublicBiddingDto> publicBiddingService)
        {
            this.linkGenerator = linkGenerator;
            this.auctionRepository = auctionRepository;
            this.mapper = mapper;
            this.auctionValidator = auctionValidator;
            this.logger = logger;
            this.publicBiddingService = publicBiddingService;

        }

        
        /// <summary>
        /// Returns list of all auctions.
        /// </summary>
        /// <returns>List of auctions</returns>
        /// <response code="200">Returns list of auctions</response>
        /// <response code="404">No auctions found</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<AuctionDto>>> GetAuctionsAsync()
        {

            var auctions = await auctionRepository.GetAuctionsAsync();

            if(auctions == null || auctions.Count == 0)
            {
                await logger.LogMessage(LogLevel.Information, "No auctions found", "Auction microservice", "GetAuctionsAsync");
                return NoContent();
            }


            List<AuctionDto> auctionDtos = new List<AuctionDto>();

            foreach(var auction in auctions)
            {
                AuctionDto auctionDto = mapper.Map<AuctionDto>(auction);

                List<PublicBiddingDto> publicBiddingDtos = await publicBiddingService.SendGetRequestAsync("");

                if(publicBiddingDtos != null && publicBiddingDtos.Count != 0)
                {
                    auctionDto.publicBiddings.AddRange(publicBiddingDtos);
                } 

                auctionDtos.Add(auctionDto);
            }
            
            await logger.LogMessage(LogLevel.Information, "Auction list successfully returned!", "Auction microservice", "GetAuctionsAsync");

            return Ok(auctionDtos);

            
        }


        /// <summary>
        /// Returns Auction by ID of auction
        /// </summary>
        /// <param name="AuctionId"></param>
        /// <returns>Auction by ID</returns>
        ///<response code="200">returns found auction</response>
        ///<response code="404">no auction found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{AuctionId}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionByIdAsync(Guid AuctionId)
        {
            var auction = await auctionRepository.GetAuctionByIdAsync(AuctionId);

            if(auction == null)
            {
                await logger.LogMessage(LogLevel.Information, "Auction not found", "Auction microservice", "GetAuctionsAsyncById");
                return NotFound();
            }


            

            AuctionDto auctionDto = mapper.Map<AuctionDto>(auction);

            var publicBiddingDtos = await publicBiddingService.SendGetRequestAsync("");

            if(publicBiddingDtos != null && publicBiddingDtos.Count != 0)
            {
                auctionDto.publicBiddings.AddRange(publicBiddingDtos);
            }
            

            await logger.LogMessage(LogLevel.Information, "Auction returned", "Auction microservice", "GetAuctionsAsyncById");
            return Ok(auctionDto);
            
        }

        
        /// <summary>
        /// Creates new auction
        /// </summary>
        /// <param name="auctionCreationDto">Auction model</param>
        /// <returns>Creation confirmation</returns>
        /// <remarks>
        /// Example of auction creation model \
        /// POST /api/Auctions \
        /// {       \
          ///      "year": 2021, \
            ///   "AuctionNum" : 4,\
            ///    "date": "2021-01-01T00:00:00",\
               /// "restriction": 33,\
                ///"priceStep": 13,\
               /// "applicationDeadline": "2023-01-01T00:00:00"\
            ///}
    /// }
    /// </remarks>
    /// <response code="200">returns created auction</response>
    /// <response code="500">There's been server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<AuctionConformationDto>> CreateAuctionAsync([FromBody] AuctionCreationDto auctionCreationDto)
        {
            try
            {
                Auction auction = mapper.Map<Auction>(auctionCreationDto);

                auctionValidator.ValidateAndThrow(auction);

                AuctionConfirmation confirmation = await auctionRepository.CreateAuctionAsync(auction);
                await auctionRepository.SaveChangesAsync();

                string location = linkGenerator.GetPathByAction("GetAuctions", "Auction", new { AuctionId = confirmation.AuctionId });

                await logger.LogMessage(LogLevel.Information, "Auction created", "Auction microservice", "PostAuctionAsync");
                return Created(location, mapper.Map<AuctionConformationDto>(confirmation));
            }
            catch(ValidationException v)
            {
                await logger.LogMessage(LogLevel.Information, "Bad request, check parameters", "Auction microservice", "PostAuctionAsync");
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch(Exception e)
            {
                await logger.LogMessage(LogLevel.Information, "Internal server error", "Auction microservice", "PostAuctionAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Updates one auction 
        /// </summary>
        /// <param name="auctionUpdate">Auction model that is updated</param>
        /// <returns>Update confirmation model</returns>
        /// <response code="200">Returns updated auction</response>
        /// <response code="400">Auction not found</response>
        /// <response code="500">There has been internal server error</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuctionDto>> UpdateAuctionAsync(AuctionUpdateDto auctionUpdate)
        {
            try
            {
                var oldAuction = await auctionRepository.GetAuctionByIdAsync(auctionUpdate.AuctionId);

                if (oldAuction == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Auction object not found!", "Auction microservice", "UpdateAuctionAsync");
                    return NotFound();
                }

                Auction auction = mapper.Map<Auction>(auctionUpdate);

                mapper.Map(auction, oldAuction);

                await auctionRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Auction updated", "Auction microservice", "UpdateAuctionAsync");
                return Ok(mapper.Map<AuctionDto>(oldAuction));

            }
            catch(Exception e)
            {
                await logger.LogMessage(LogLevel.Information, "Internal server error", "Auction microservice", "UpdateAuctionAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Deletes one auction by ID
        /// </summary>
        /// <param name="AuctionId"></param>
        /// <returns>Status 204</returns>
        /// <response code="204">Auction successesfuly deleted</response>
        /// <response code="404">Auction not found</response>
        /// <response code="500">There has been internal server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{AuctionId}")]
        public async Task<IActionResult> DeleteAuctionAsync(Guid AuctionId)
        {
            try
            {
                var auction = await auctionRepository.GetAuctionByIdAsync(AuctionId);

                if(auction == null)
                {
                    await logger.LogMessage(LogLevel.Information, "Auction not found", "Auction microservice", "DeleteAuctionAsync");
                    return NotFound();

                }

                await auctionRepository.DeleteAuctionAsync(AuctionId);
                await auctionRepository.SaveChangesAsync();
                await logger.LogMessage(LogLevel.Information, "Auction deleted", "Auction microservice", "DeleteAuctionAsync");
                return NoContent();


                
            }
            catch(Exception e)
            {
                await logger.LogMessage(LogLevel.Information, "Internal server error", "Auction microservice", "DeleteAuctionAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        /// <summary>
        /// returns possible options to work with auctions
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetAuctionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logger.LogMessage(LogLevel.Information, "Auction options", "Auction microservice", "GetAuctionOptions");
            return Ok();
        }
    }
}
