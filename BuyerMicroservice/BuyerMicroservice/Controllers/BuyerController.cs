using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Data.Repositories;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models;
using BuyerMicroservice.Models.AuthorizedPersonBuyer;
using BuyerMicroservice.Models.Buyer;
using BuyerMicroservice.Models.Individual;
using BuyerMicroservice.Models.LegalEntity;
using BuyerMicroservice.ServiceCalls;
using BuyerMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
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
        private readonly ILoggerService logger;
        private readonly IServiceCall<AddressDto> addressService;
        private readonly IServiceCall<PaymentDto> paymentService;

        public readonly IAuthorizedPersonRepository authorizedPersonRepository;



        public BuyerController(IBuyerRepository buyerRepository, IMapper mapper, LinkGenerator linkGenerator, BuyerValidator validator, ILoggerService logger, IServiceCall<AddressDto> addressService, IServiceCall<PaymentDto> paymentService, IAuthorizedPersonRepository authorizedPersonRepository)
        {
            this.buyerRepository = buyerRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.addressService = addressService;
            this.logger = logger;
            this.paymentService = paymentService;
            this.authorizedPersonRepository = authorizedPersonRepository;


        }

        /// <summary>
        /// Vraća sve kupce
        /// </summary>
        /// <returns>Lista kupaca</returns>
        /// <response code = "200">Vraća listu kupaca</response>
        /// <response code = "204">Ne postoji nijedan kupac</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<List<BuyerDto>>> GetBuyerAsync(int realizedArea = 0)
        {
            List<Buyer> buyers = await buyerRepository.GetBuyerAsync(realizedArea);

            if (buyers == null || buyers.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Buyer list is empty!", "Buyer microservice", "GetBuyerAsync");
                return NoContent();
            }

            List<BuyerDto> buyersDto = new List<BuyerDto>();

            foreach (var buyer in buyers)
            {
                BuyerDto buyerDto = mapper.Map<BuyerDto>(buyer);

                if (buyer.addressId is not null && buyer.paymentId is not null)
                {
                    var addressDto = await addressService.SendGetRequestAsync("http://localhost:40001/address");
                    var paymentDto = await paymentService.SendGetRequestAsync("http://localhost:40008/payment");
                    if (addressDto is not null && paymentDto is not null )
                    {
                        buyerDto.address = addressDto;
                        buyerDto.payment = paymentDto;
                    }
                }
                buyersDto.Add(buyerDto);
            }

            await logger.LogMessage(LogLevel.Information, "Buyer list successfully returned!", "Buyer microservice", "GetBuyerAsync");
            return Ok(buyersDto);
        }

        /// <summary>
        /// Vraća trazenog kupca po ID-ju
        /// </summary>
        /// <param name="buyerID">ID kupca</param>
        /// <returns>Traženi kupac</returns>
        /// <response code = "200">Vraća traženog kupca</response>
        /// <response code = "404">Nije pronađen traženi kupac</response>
        [HttpGet("{buyerID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<BuyerDto>> GetBuyerByIdAsync(Guid buyerID)
        {
            Buyer buyer = await buyerRepository.GetBuyerByIdAsync(buyerID);

            if (buyer == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Buyer not found!", "Buyer microservice", "GetBuyerByIdAsync");
                return NotFound();
            }

            BuyerDto buyerDto = mapper.Map<BuyerDto>(buyer);

            if(buyer.addressId is not null && buyer.paymentId is not null)
            {
                var addressDto = await addressService.SendGetRequestAsync("http://localhost:40001/address");
                var paymentDto = await paymentService.SendGetRequestAsync("http://localhost:40008/payment");

                if ( addressDto is not null && paymentDto is not null)
                {
                    buyerDto.address = addressDto;
                    buyerDto.payment = paymentDto;
                }
            }
            await logger.LogMessage(LogLevel.Information, "Buyer found and successfully returned!", "Buyer microservice", "GetBuyerByIdAsync");
            return Ok(
                buyer is Individual
                    ? mapper.Map<IndividualDto>(buyer)
                    : mapper.Map<LegalEntityDto>(buyer)
            );
        }

        /// <summary>
        /// Briše kupca na osnovu ID-ja
        /// </summary>
        /// <param name="buyerId">ID kupca</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kupac uspešno obrisan</response>
        /// <response code="404">Nije pronađen kupac za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja kupca</response>
        [HttpDelete("{buyerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<IActionResult> DeleteBuyerAsync(Guid buyerId)
        {
            try
            {
                Buyer buyer = await buyerRepository.GetBuyerByIdAsync(buyerId);

                if (buyer == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Buyer object not found!", "Buyer microservice", "DeleteBuyerAsync");
                }

                await buyerRepository.DeleteBuyerAsync(buyerId);
                await buyerRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Buyer object deleted successfully!", "Buyer microservice", "DeleteBuyerAsync");
                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Buyer object deletion failed!", "Buyer microservice", "DeleteBuyerAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Povezivanje ovlascenog lica sa kupcem
        /// </summary>
        /// <param name="apbDto"> model kupca kome se dodaje ovlasceno lice</param>
        /// <returns>Potvrda o uspesnom povezivanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove banke \
        /// post /api/buyers/AddAuthorizedPerson \
        /// </remarks>
        /// <response code = "201"> potvrda o uspesnom povezivanju </response>
        /// <response code = "500">Došlo je do greške na serveru prilikom povezivanja kupca i ovlascenog lica</response>
        [HttpPost("AddAuthorizedPerson")]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<IActionResult> AddBuyerAuthorizedPerson(AuthorizedPersonBuyerDto apbDto)
        {
            AuthorizedPerson ap = await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(apbDto.authorizedPersonId);
            await buyerRepository.AddAuthorizedPersonToBuyer(ap,apbDto.buyerId);
            await buyerRepository.SaveChangesAsync();


            return NoContent();
        }
        /// <summary>
        /// Brisanje veze kupcem sa ovlascenog lica
        /// </summary>
        /// <param name="apbDto"> model kupca kome se brise ovlasceno lice</param>
        /// <returns>Potvrda o uspesnom brisanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove banke \
        /// DELETE /api/buyers/DeleteAuthorizedPerson \
        /// </remarks>
        /// <response code = "201"> potvrda o uspesnom brisanju </response>
        /// <response code = "500">Došlo je do greške na serveru prilikom brisanja veze kupca i ovlascenog lica</response>
        [HttpDelete("DeleteAuthorizedPerson")]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<IActionResult> DeleteBuyerAuthorizedPerson(AuthorizedPersonBuyerDto apbDto)
        {
            AuthorizedPerson ap = await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(apbDto.authorizedPersonId);
            await buyerRepository.RemoveAuthorizedPersonFromBuyer(ap, apbDto.buyerId);
            await buyerRepository.SaveChangesAsync();


            return NoContent();
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve kupce
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<IActionResult> GetBuyerOptions()
        {
            Response.Headers.Add("Allow", "GET,DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetBuyerOptions");
            return Ok();
        }

    }
}
