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
        
        
       
        public BuyerController(IBuyerRepository buyerRepository, IMapper mapper, LinkGenerator linkGenerator, BuyerValidator validator)
        {
            this.buyerRepository = buyerRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
          
        }
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<BuyerDto>>> GetBuyerAsync(int realizedArea = 0)
        {
            List<Buyer> buyer = await buyerRepository.GetBuyerAsync(realizedArea);

            if (buyer == null || buyer.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<BuyerDto>>(buyer));
        }

        [HttpGet("{buyerID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuyerDto>> GetBuyerByIdAsync(Guid buyerID)
        {
            Buyer buyer = await buyerRepository.GetBuyerByIdAsync(buyerID);

            if (buyer == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BuyerDto>(buyer));
        }

        [HttpDelete("{buyerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBuyerAsync(Guid buyerId)
        {
            try
            {
                Buyer buyer = await buyerRepository.GetBuyerByIdAsync(buyerId);

                if (buyer == null)
                {
                    return NotFound();
                }

                await buyerRepository.DeleteBuyerAsync(buyerId);
                await buyerRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetBuyerOptions()
        {
            Response.Headers.Add("Allow", "GET,DELETE");
            return Ok();
        }

    }
}
