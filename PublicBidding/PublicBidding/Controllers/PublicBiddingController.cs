using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PublicBidding.Data;
using PublicBidding.Entities;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Controllers
{
    [ApiController]
    [Route("api/publicBiddings")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class PublicBiddingController : ControllerBase
    {
        private readonly IPublicBiddingRepository publicBiddingRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;

        public PublicBiddingController(IPublicBiddingRepository publicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.publicBiddingRepository = publicBiddingRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sva javna nadmetanja.
        /// </summary>
        /// <param name="studentIndex">Indeks studenta (npr. IT1/2020)</param>
        /// <param name="subjectCode">Šifra predmeta (npr. S12020)</param>
        /// <param name="subjectName">Naziv predmeta</param>
        /// <returns>Lista javnih nadmetanja</returns>
        /// <response code="200">Vraća listu javnih nadmetanja</response>
        /// <response code="404">Nije pronađeno ni jedno javno nadmetanje</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PublicBiddingDto>> GetPublicBiddings(int numberOfApplicants, Entities.Type type, Status status)
        {
            var publicBiddings = publicBiddingRepository.GetPublicBiddings(numberOfApplicants, type, status);

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (publicBiddings == null || publicBiddings.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<PublicBiddingDto>>(publicBiddings));
        }
    }
}
