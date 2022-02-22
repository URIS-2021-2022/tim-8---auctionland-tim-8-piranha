﻿using AdMicroservice.Data;
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

        public AdController(IAdRepository adRepository, LinkGenerator linkGenerator, IMapper mapper, 
            IJournalRepository journalRepository, ILoggerService loggerService)
        {
            this.adRepository = adRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.journalRepository = journalRepository;
            Logger = loggerService;
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
        public ActionResult<List<AdDto>> GetComplaintTypes(string publicationDate)
        {
            List<AdModel> ads = adRepository.GetAds(publicationDate);
            if (ads == null || ads.Count == 0)
            {
                Logger.LogMessage(LogLevel.Warning, "Ads list is empty!", "Ad microservice", "GetAds");
                return NoContent();
            }
            Logger.LogMessage(LogLevel.Information, "Ads list successfully returned!", "Ad microservice", "GetAds");
            return Ok(mapper.Map<List<AdDto>>(ads));
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
        public ActionResult<AdDto> GetAdById(Guid adId)
        {
            AdModel ad = adRepository.GetAdById(adId);

            if (ad == null)
            {
                Logger.LogMessage(LogLevel.Warning, "Ad not found!", "Ad microservice", "GetAdById");
                return NotFound();
            }
            Logger.LogMessage(LogLevel.Information, "Ad found and successfully returned!", "Ad microservice", "GetAdById");
            return Ok(mapper.Map<AdDto>(ad));
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
        public ActionResult<AdConfirmationDto> CreateAd([FromBody] AdCreationDto ad)
        {
            try
            {
                AdModel a = mapper.Map<AdModel>(ad);
                AdConfirmation confirmation = adRepository.CreateAd(a);
                string location = linkGenerator.GetPathByAction("GetAdById", "Ad", new { adId = a.AdId });
                Logger.LogMessage(LogLevel.Information, "Ad successfully created!", "Ad microservice", "CreateAd");
                return Created(location, mapper.Map<AdConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                Logger.LogMessage(LogLevel.Error, "Ad creation failed!", "Ad microservice", "CreateAd");
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
        public ActionResult<AdConfirmationDto> UpdateAd(AdUpdateDto ad)
        {
            try
            {

                var oldAd = adRepository.GetAdById(ad.AdId);

                if (oldAd == null)
                {
                    Logger.LogMessage(LogLevel.Warning, "Ad not found!", "Ad microservice", "UpdateAd");
                    return NotFound();
                }
                AdModel a = mapper.Map<AdModel>(ad);
                mapper.Map(a, oldAd);
                a.Journal = journalRepository.GetJournalById(oldAd.JournalId);
                adRepository.SaveChanges();
                Logger.LogMessage(LogLevel.Information, "Ad successfully updated!", "Ad microservice", "UpdateAd");
                return Ok(a);
            }
            catch
            {
                Logger.LogMessage(LogLevel.Error, "Ad update failed!", "Ad microservice", "UpdateAd");
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
        public IActionResult DeleteComplaintType(Guid adId)
        {
            try
            {
                AdModel ad = adRepository.GetAdById(adId);

                if (ad == null)
                {
                    Logger.LogMessage(LogLevel.Warning, "Ad not found!", "Ad microservice", "DeleteAd");
                    return NotFound();
                }
                adRepository.DeleteAd(adId);
                Logger.LogMessage(LogLevel.Information, "Ad deleted successfully!", "Ad microservice", "DeleteAd");
                return NoContent();
            }
            catch
            {
                Logger.LogMessage(LogLevel.Error, "Ad deletion failed!", "Ad microservice", "DeleteAd");
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
