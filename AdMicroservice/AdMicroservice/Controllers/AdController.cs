using AdMicroservice.Data;
using AdMicroservice.Entities.Ad;
using AdMicroservice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    [ApiController]
    [Route("api/ads")]
    public class AdController : ControllerBase
    {
        private readonly IAdRepository adRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public AdController(IAdRepository adRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.adRepository = adRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<AdDto>> GetComplaintTypes(string publicationDate)
        {
            List<AdModel> ads = adRepository.GetAds(publicationDate);
            if (ads == null || ads.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<AdDto>>(ads));
        }

        [HttpGet("{adId}")]
        public ActionResult<AdDto> GetAdById(Guid adId)
        {
            AdModel ad = adRepository.GetAdById(adId);

            if (ad == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AdDto>(ad));
        }

        [HttpPost]
        public ActionResult<AdConfirmationDto> CreateAd([FromBody] AdCreationDto ad)
        {
            try
            {
                AdModel a = mapper.Map<AdModel>(ad);
                AdConfirmation confirmation = adRepository.CreateAd(a);
                string location = linkGenerator.GetPathByAction("GetAdById", "Ad", new { adId = a.AdId });
                return Created(location, mapper.Map<AdConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<AdConfirmationDto> UpdateAd(AdUpdateDto ad)
        {
            try
            {

                if (adRepository.GetAdById(ad.AdId) == null)
                {
                    return NotFound();
                }
                AdModel a = mapper.Map<AdModel>(ad);
                AdConfirmation confirmaion = adRepository.UpdateAd(a);
                return Ok(mapper.Map<AdConfirmationDto>(confirmaion));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{adId}")]
        public IActionResult DeleteComplaintType(Guid adId)
        {
            try
            {
                AdModel ad = adRepository.GetAdById(adId);

                if (ad == null)
                {
                    return NotFound();
                }
                adRepository.DeleteAd(adId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }



        [HttpOptions]
        public IActionResult GetAdOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

