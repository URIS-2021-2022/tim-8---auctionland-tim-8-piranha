using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PublicBidding.Data;
using PublicBidding.Entities;
using PublicBidding.Models;
using PublicBidding.ServiceCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Controllers
{
    [ApiController]
    [Route("api/publicBidding")]
    [Produces("application/json", "application/xml")]
    public class PublicBiddingController : ControllerBase
    {
        private readonly IPublicBiddingRepository publicBiddingRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IService<AddressDto> addressMock;
        private readonly IService<AuthorizedPersonDto> authorizedPersonMock;
        private readonly IService<BuyerDto> buyerMock;
        private readonly IService<PlotPartDto> plotPartMock;

        public PublicBiddingController(IPublicBiddingRepository publicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper, IService<AddressDto> addressMock, IService<AuthorizedPersonDto> authorizedPersonMock, IService<BuyerDto> buyerMock, IService<PlotPartDto> plotPartMock)
        {
            this.publicBiddingRepository = publicBiddingRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.addressMock = addressMock;
            this.authorizedPersonMock = authorizedPersonMock;
            this.buyerMock = buyerMock;
            this.plotPartMock = plotPartMock;
        }

        /// <summary>
        /// Vraća sva javna nadmetanja.
        /// </summary>
        /// <returns>Lista javnih nadmetanja</returns>
        /// <response code="200">Vraća listu javnih nadmetanja</response>
        /// <response code="404">Nije pronađeno ni jedno javno nadmetanje</response>
        [HttpGet]
        [HttpHead]    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PublicBiddingDto>>> GetPublicBiddings()
        {
            var publicBiddings = await publicBiddingRepository.GetPublicBiddings();

            if (publicBiddings == null || publicBiddings.Count == 0)
            {
                return NoContent();
            }

            PublicBiddingDto publicBiddingDto = new PublicBiddingDto();
            AddressDto address = await addressMock.SendGetRequestAsync();
            AuthorizedPersonDto authorizedPerson = await authorizedPersonMock.SendGetRequestAsync();
            BuyerDto buyer = await buyerMock.SendGetRequestAsync();
            PlotPartDto plotPart = await plotPartMock.SendGetRequestAsync();

            foreach (var publicBidding in publicBiddings)
            {
                publicBiddingDto = mapper.Map<PublicBiddingDto>(publicBidding);
                publicBiddingDto.Address = address;
                publicBiddingDto.BestBidder = buyer;
                //publicBiddingDto.Buyers.Add(buyer);
                //publicBiddingDto.AuthorizedPersons.Add(authorizedPerson);
                //publicBiddingDto.PlotParts.Add(plotPart);
            }
            return Ok(publicBiddingDto);

            //return Ok(mapper.Map<List<PublicBiddingDto>>(publicBiddings));
        }

        [HttpGet("{publicBiddingId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicBiddingDto>> GetPublicBiddingById(Guid publicBiddingId)
        {
            var publicBidding = await publicBiddingRepository.GetPublicBiddingById(publicBiddingId);

            if (publicBidding == null)
            {
                return NotFound();
            }

            AddressDto address = await addressMock.SendGetRequestAsync();
            AuthorizedPersonDto authorizedPerson = await authorizedPersonMock.SendGetRequestAsync();
            BuyerDto buyer = await buyerMock.SendGetRequestAsync();
            PlotPartDto plotPart = await plotPartMock.SendGetRequestAsync();

            PublicBiddingDto publicBiddingDto = mapper.Map<PublicBiddingDto>(publicBidding);
            publicBiddingDto.Address = address;
            publicBiddingDto.BestBidder = buyer;
            //publicBiddingDto.Buyers.Add(buyer);
            //publicBiddingDto.AuthorizedPersons.Add(authorizedPerson);
            //publicBiddingDto.PlotParts.Add(plotPart);

            return Ok(publicBiddingDto);

            //return Ok(mapper.Map<PublicBiddingDto>(publicBidding));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicBiddingConfirmationDto>> CreatePublicBidding([FromBody] PublicBiddingCreationDto publicBidding)
        {
            try
            {
                PublicBiddingConfirmation newPublicBidding = await publicBiddingRepository.CreatePublicBidding(mapper.Map<Entities.PublicBidding>(publicBidding));
                await publicBiddingRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPublicBiddingById", "PublicBidding", new { publicBiddingId = newPublicBidding.PublicBiddingId });

                return Created(location, mapper.Map<PublicBiddingConfirmationDto>(newPublicBidding));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri kreiranju novog javnog nadmetanja." + e);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicBiddingDto>> UpdatePublicBidding(PublicBiddingUpdateDto publicBidding)
        {
            try
            {
                var oldPublicBidding = await publicBiddingRepository.GetPublicBiddingById(publicBidding.PublicBiddingId);

                if (oldPublicBidding == null)
                {
                    return NotFound();
                }

                Entities.PublicBidding newPublicBidding = mapper.Map<Entities.PublicBidding>(publicBidding);

                mapper.Map(newPublicBidding, oldPublicBidding);

                await publicBiddingRepository.UpdatePublicBidding(newPublicBidding);
                await publicBiddingRepository.SaveChanges();

                return Ok(mapper.Map<PublicBiddingDto>(oldPublicBidding));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri modifikaciji javnog nadmetanja.");
            }

        }

        [HttpDelete("{publicBiddingId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePublicBidding(Guid publicBiddingId)
        {
            try
            {
                var publicBidding = await publicBiddingRepository.GetPublicBiddingById(publicBiddingId);

                if (publicBidding == null)
                {
                    return NotFound();
                }

                await publicBiddingRepository.DeletePublicBidding(publicBiddingId);
                await publicBiddingRepository.SaveChanges();

                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri brisanju javnog nadmetanja.");
            }
        }
    }
}
