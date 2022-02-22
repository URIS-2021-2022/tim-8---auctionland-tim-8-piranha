using AdMicroservice.Data;
using AdMicroservice.Data.Journal;
using AdMicroservice.Entities.Ad;
using AdMicroservice.Models;
using AdMicroservice.ServiceCalls;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    [ApiController]
    [Route("api/ads")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class AdController : ControllerBase
    {
        private readonly IAdRepository adRepository;
        private readonly IJournalRepository journalRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService Logger;
        private readonly IServiceCall<PublicBiddingDto> publicBiddingService;

        public AdController(IAdRepository adRepository, LinkGenerator linkGenerator, IMapper mapper, 
            IJournalRepository journalRepository, ILoggerService loggerService,
            IServiceCall<PublicBiddingDto> publicBiddingService)
        {
            this.adRepository = adRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.journalRepository = journalRepository;
            Logger = loggerService;
            this.publicBiddingService = publicBiddingService;
        }

        /// <summary>
        /// Vraća sve oglase
        /// </summary>
        /// <param name="publicationDate">Datum objavljivanja oglasa</param>
        /// <returns>Lista oglasa</returns>
        /// <response code="200">Vraća listu oglasa</response>
        /// <response code="404">Nije pronađen nijedan oglas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AdDto>>> GetComplaintTypes(string publicationDate)
        {
            List<AdModel> ads = await adRepository.GetAds(publicationDate);
            if (ads == null || ads.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Ads list is empty!", "Ad microservice", "GetAds");
                return NoContent();
            }

            List<AdDto> adDtos = new List<AdDto>();

            foreach (var ad in ads)
            {
                AdDto adDto = mapper.Map<AdDto>(ad);

                if (ad.PublicBiddingId is not null)
                {
                    var publicBiddingDto = await publicBiddingService.SendGetRequestAsync("");

                    if (publicBiddingDto is not null)
                    {
                        adDto.PublicBidding = publicBiddingDto;
                    }
                }
                adDtos.Add(adDto);
            }

            await Logger.LogMessage(LogLevel.Information, "Ads list successfully returned!", "Ad microservice", "GetAds");
            return Ok(adDtos);
        }

        /// <summary>
        /// Vraća jedan oglas na osnovu ID-a
        /// </summary>
        /// <returns>Oglas</returns>
        /// <response code="200">Vraća jedan oglas</response>
        /// <response code="404">Nije pronađen oglas sa tim ID-em</response>
        [HttpGet("{adId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AdDto>> GetAdById(Guid adId)
        {
            AdModel ad = await adRepository.GetAdById(adId);

            if (ad == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Ad not found!", "Ad microservice", "GetAdById");
                return NotFound();
            }

            AdDto adDto = mapper.Map<AdDto>(ad);

            if (ad.PublicBiddingId is not null)
            {
                var publicBiddingDto = await publicBiddingService.SendGetRequestAsync("");

                if (publicBiddingDto is not null)
                {
                    adDto.PublicBidding = publicBiddingDto;
                }
            }

            await Logger.LogMessage(LogLevel.Information, "Ad found and successfully returned!", "Ad microservice", "GetAdById");
            return Ok(adDto);
        }

        /// <summary>
        /// Kreira oglas
        /// </summary>
        /// <returns>Kreiran oglas</returns>
        /// <response code="200">Vraća kreiran oglas</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa novog oglasa</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AdConfirmationDto>> CreateAd([FromBody] AdCreationDto ad)
        {
            try
            {
                AdModel a = mapper.Map<AdModel>(ad);
                AdConfirmation confirmation = await adRepository.CreateAd(a);
                string location = linkGenerator.GetPathByAction("GetAdById", "Ad", new { adId = a.AdId });
                await Logger.LogMessage(LogLevel.Information, "Ad successfully created!", "Ad microservice", "CreateAd");
                return Created(location, mapper.Map<AdConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Ad creation failed!", "Ad microservice", "CreateAd");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan oglas
        /// </summary>
        /// <returns>Potvrdu o modifikovanom oglas</returns>
        /// <response code="200">Vraća ažuriran oglas</response>
        /// <response code="400">Oglas koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja oglasa</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AdConfirmationDto>> UpdateAd(AdUpdateDto ad)
        {
            try
            {

                var oldAd = await adRepository.GetAdById(ad.AdId);

                if (oldAd == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Ad not found!", "Ad microservice", "UpdateAd");
                    return NotFound();
                }
                AdModel a = mapper.Map<AdModel>(ad);
                mapper.Map(a, oldAd);
                a.Journal = await journalRepository.GetJournalById(oldAd.JournalId);
                await adRepository.SaveChanges();
                await Logger.LogMessage(LogLevel.Information, "Ad successfully updated!", "Ad microservice", "UpdateAd");
                return Ok(a);
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "Ad update failed!", "Ad microservice", "UpdateAd");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog oglasa na osnovu ID-a 
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Oglas uspešno obrisan</response>
        /// <response code="404">Nije pronađen oglas za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja oglasa</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{adId}")]
        public async Task<IActionResult> DeleteComplaintType(Guid adId)
        {
            try
            {
                AdModel ad = await adRepository.GetAdById(adId);

                if (ad == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Ad not found!", "Ad microservice", "DeleteAd");
                    return NotFound();
                }
                await adRepository.DeleteAd(adId);
                await Logger.LogMessage(LogLevel.Information, "Ad deleted successfully!", "Ad microservice", "DeleteAd");
                return NoContent();
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "Ad deletion failed!", "Ad microservice", "DeleteAd");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa oglasom
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetAdOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

