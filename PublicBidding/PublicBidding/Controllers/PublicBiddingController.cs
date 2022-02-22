using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
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
    /// <summary>
    /// Kontroler za javno nadmetanje
    /// </summary>
    [ApiController]
    [Route("api/publicBidding")]
    [Produces("application/json", "application/xml")]
    public class PublicBiddingController : ControllerBase
    {
        private readonly IPublicBiddingService publicBiddingService;
        private readonly IPublicBiddingRepository publicBiddingRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        //private readonly IService<AddressDto> addressMock;
        //private readonly IService<AuthorizedPersonDto> authorizedPersonMock;
        //private readonly IService<BuyerDto> buyerMock;
        //private readonly IService<PlotPartDto> plotPartMock;
        private readonly ILoggerService logger;

        public PublicBiddingController(IPublicBiddingService publicBiddingService, IPublicBiddingRepository publicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService logger)
        {
            this.publicBiddingRepository = publicBiddingRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            this.publicBiddingService = publicBiddingService;
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
                //await logger.LogMessage(LogLevel.Warning, "Public bidding list is empty!", "PublicBidding microservice", "GetPublicBiddings");
                return NoContent();
            }

            List<PublicBiddingDto> publicBiddingsDto = mapper.Map<List<PublicBiddingDto>>(publicBiddings);
            
            foreach (var publicBidding in publicBiddings)
            {
                publicBiddingsDto.Add(await publicBiddingService.GetInfoForListsInPublicBidding(publicBidding));
            }
            //await logger.LogMessage(LogLevel.Information, "Public bidding list successfully returned!", "PublicBidding microservice", "GetPublicBiddings");
            return Ok(publicBiddingsDto);

        }

        /// <summary>
        /// Vraća jedno javno nadmetanje na osnovu prosleđenog ID-a
        /// </summary>
        /// <param name="publicBiddingId">ID javnog nadmetanja</param>
        /// <returns>Status javnog nadmetanja</returns>
        /// <response code="200">Vraća traženo status javno nadmetanje</response>
        /// <response code="404">Nije pronađeno javno nadmetanje sa unetim ID-em</response>
        [HttpGet("{publicBiddingId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicBiddingDto>> GetPublicBiddingById(Guid publicBiddingId)
        {
            var publicBidding = await publicBiddingRepository.GetPublicBiddingById(publicBiddingId);

            if (publicBidding == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Public bidding not found!", "PublicBidding microservice", "GetPublicBiddingById");
                return NotFound();
            }

            PublicBiddingDto publicBiddingDto = await publicBiddingService.GetInfoForListsInPublicBidding(publicBidding);

            await logger.LogMessage(LogLevel.Information, "Public bidding found and successfully returned!", "PublicBidding microservice", "GetPublicBiddingById");
            return Ok(mapper.Map<PublicBiddingDto>(publicBidding));

        }


        /// <summary>
        /// Vraća listu javnih nadmetanja za druge servise na osnovu prosleđene liste ID-eva
        /// </summary>
        /// <returns>Status javnog nadmetanja</returns>
        /// <response code="200">Vraća traženo status javno nadmetanje</response>
        /// <response code="404">Nije pronađeno javno nadmetanje sa unetim ID-em</response>
        [HttpGet("info")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PublicBiddingForOtherServices>>> GetPublicBiddingsById(List<string> publicBiddings)
        {
            List<PublicBiddingForOtherServices> publicBiddingsForOtherServices = new List<PublicBiddingForOtherServices>();

            foreach (var publicBiddingId in publicBiddings)
            {
                var publicBiddingForOtherServices = mapper.Map<PublicBiddingForOtherServices>(await publicBiddingRepository.GetPublicBiddingById(Guid.Parse(publicBiddingId)));
                publicBiddingsForOtherServices.Add(publicBiddingForOtherServices);
            }

            if (publicBiddingsForOtherServices is null)
            {
                await logger.LogMessage(LogLevel.Warning, "Public biddings not found!", "PublicBidding microservice", "GetPublicBiddingsById");
                return NotFound();
            }

            await logger.LogMessage(LogLevel.Information, "Public biddings found and successfully returned!", "PublicBidding microservice", "GetPublicBiddingsById");
            return Ok(publicBiddingsForOtherServices);

        }

        /// <summary>
        /// Kreira novo javno nadmetanje
        /// </summary>
        /// <param name="publicBidding">Model javnog nadmetanja</param>
        /// <returns>Potvrda o kreiranju javnog nadmetanja</returns>
        /// <response code="201">Vraća kreirano javno nadmetanje</response>
        /// <response code="500">Desila se greška prilikom kreiranja novog javnog nadmetanja</response>
        /// <remarks>
        /// Primer POST zahteva \
        /// POST /api/publicBidding \
        /// { \
        ///     "Date":"2018-12-10T00:00:00.000Z", \
        ///     "StartTime": "2018-12-10T13:45:00.000Z", \
        ///     "EndTime":"2018-12-10T15:45:00.000Z", \
        ///     "PlotParts":["9c87cb08-dbf6-41cc-bea3-4070429867d0"], \
        ///     "StartPricePerHa":500.5, \
        ///     "IsExcepted": false, \
        ///     "TypeId": "8010f254-e872-49d9-9c2c-1d5783719019", \
        ///     "StatusId": "2233cbba-607a-4182-9f83-7ff8ffe6e5ac", \
        ///     "Price":600.2, \
        ///     "BuyerId": "5adf06b6-605c-40b2-92bc-5fff5ca3d6f8", \
        ///     "RentPeriod":2, \
        ///     "Bidders":["5adf06b6-605c-40b2-92bc-5fff5ca3d6f8"], \
        ///     "AuthorizedPersons":["92c0db66-b124-4eed-8d3f-ba5ce3e1db8e"], \
        ///     "NumberOfApplicants": 1, \
        ///     "Round":4 \
        /// }
        /// </remarks>
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

                await logger.LogMessage(LogLevel.Information, "Public bidding successfully created!", "PublicBidding microservice", "CreatePublicBidding");
                return Created(location, mapper.Map<PublicBiddingConfirmationDto>(newPublicBidding));
            }
            catch (Exception e)
            {
                await logger.LogMessage(LogLevel.Error, "Public bidding object creation failed!", "PublicBidding microservice", "CreatePublicBidding");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri kreiranju novog javnog nadmetanja." + e);
            }
        }

        ///<summary>
        /// Modifikacija javnog nadmetanja
        /// </summary>
        /// <param name="publicBidding">Model javnog nadmetanja</param>
        /// <param name="publicBiddingId">Id javnog nadmetanja</param>
        /// <returns>Potvrda o izmeni javnog nadmetanja</returns>
        /// <response code="200">Izmenjeno javno nadmetanje</response>
        /// <response code="404">Nije pronađeno javno nadmetanje sa unetim ID-em</response>
        /// <response code="500">Serverska greška tokom izmene javnog nadmetanja</response>
        /// /// <remarks>
        /// Primer PUT zahteva \
        /// PUT /api/person \
        /// {   \
        ///    "publicBiddingId": "62c28c9a-7306-45c7-a5b3-1603eed4fd5a", \
        ///     "type": "8010f254-e872-49d9-9c2c-1d5783719019", \
        ///     "status": "2233cbba-607a-4182-9f83-7ff8ffe6e5ac", \
        ///     "startTime": "2018-08-09T17:45:00", \
        ///     "endTime": "2018-08-09T19:45:00", \
        ///     "date": "2018-08-09T02:00:00", \
        ///     "startPricePerHa": 1100.6, \
        ///     "isExcepted": false, \
        ///     "addressId": "aa73f8ec-3971-42e7-b9f7-9ae3490889eb", \
        ///     "price": 1600.4, \
        ///     "bestBidder": "440a108d-8fa7-419c-9892-ab9f9c623908", \
        ///     "rentPeriod": 3, \
        ///     "numberOfApplicants": 4, \
        ///     "depositSupplement": 200.2, \
        ///     "round": 2, \
        ///     "plotParts": ["f1681b2d-b26d-445b-9caa-7ccf8034dcd1"], \
        ///     "authorizedPersons": null, \
        ///     "bidders": null \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicBiddingDto>> UpdatePublicBidding([FromBody] PublicBiddingUpdateDto publicBidding)
        {
            try
            {
                var oldPublicBidding = await publicBiddingRepository.GetPublicBiddingById(publicBidding.PublicBiddingId);

                //var oldPublicBidding = mapper.Map<Entities.PublicBidding>(publicBiddingRepository.GetPublicBiddingById(publicBidding.PublicBiddingId));

                if (oldPublicBidding == null)
                {
                    //await logger.LogMessage(LogLevel.Warning, "Public bidding object not found!", "PublicBidding microservice", "UpdatePublicBidding");
                    return NotFound();
                }

                //Entities.PublicBidding newPublicBidding = mapper.Map<Entities.PublicBidding>(publicBidding);

                //mapper.Map(newPublicBidding, oldPublicBidding);

                await publicBiddingRepository.UpdatePublicBidding(mapper.Map<Entities.PublicBidding>(publicBidding));
                await publicBiddingRepository.SaveChanges();

                //await logger.LogMessage(LogLevel.Information, "Public bidding object updated successfully!", "PublicBidding microservice", "UpdatePublicBidding");
                return Ok(mapper.Map<PublicBiddingDto>(oldPublicBidding));
            }
            catch (Exception ex)
            {
                //await logger.LogMessage(LogLevel.Error, "Public bidding object updating failed!", "PublicBidding microservice", "UpdatePublicBidding");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri modifikaciji javnog nadmetanja. " + ex);
            }

        }

        /// <summary>
        /// Brisanje javnog nadmetanja na osnovu ID-a
        /// </summary>
        /// <param name="publicBiddingId">ID javnog nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Javno nadmetanje je uspešno obrisana</response>
        /// <response code="404">Nije pronađeno javno nadmetanje za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja javnog nadmetanja</response>
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
                    await logger.LogMessage(LogLevel.Warning, "Public bidding object not found!", "PublicBidding microservice", "DeletePublicBidding");
                    return NotFound();
                }

                await publicBiddingRepository.DeletePublicBidding(publicBiddingId);
                await publicBiddingRepository.SaveChanges();

                await logger.LogMessage(LogLevel.Information, "Public bidding object deleted successfully!", "PublicBidding microservice", "DeletePublicBidding");
                return NoContent();

            }
            catch (Exception)
            {
                await logger.LogMessage(LogLevel.Error, "Public bidding object deletion failed!", "PublicBidding microservice", "DeletePublicBidding");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri brisanju javnog nadmetanja.");
            }
        }

        /// <summary>
        /// Pregled zaglavlja odgovora
        /// </summary>
        /// <returns>Zaglavlje odgovora</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicBiddingOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "PublicBidding microservice", "GetPublicBiddingOptions");

            return Ok();
        }
    }
}
