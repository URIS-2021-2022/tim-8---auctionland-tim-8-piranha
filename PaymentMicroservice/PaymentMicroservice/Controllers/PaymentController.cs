using AutoMapper;
using PaymentMicroservice.Data.Interfaces;
using PaymentMicroservice.Entities;
using PaymentMicroservice.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using CustomValidationException = PaymentMicroservice.Models.Exceptions.ValidationException;
using DocumentMicroservice.ServiceCalls;
using PaymentMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PaymentMicroservice.Controllers
{
    [ApiController]
    [Route("api/Payment")]
    [Produces("application/json", "application/xml")]
    //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly LinkGenerator linkGeneration;
        private readonly IMapper mapper;
        private readonly IValidator<Payment> Validator;
        private readonly IServiceCall<PublicBiddingDto> publicBiddingService;
        private readonly IConfiguration configuration;
        private readonly ILoggerService logger;

        public PaymentController(IPaymentRepository paymentRepository, LinkGenerator linkGeneration, IMapper mapper, IValidator<Payment> Validator, IConfiguration configuration, ILoggerService logger, IServiceCall<PublicBiddingDto> publicBiddingService)
        {
            this.paymentRepository = paymentRepository;
            this.linkGeneration = linkGeneration;
            this.mapper = mapper;
            this.Validator = Validator;
            this.configuration = configuration;
            this.publicBiddingService = publicBiddingService;
            this.logger = logger;
        }
        /// <summary>
        /// Vraća sve uplate
        /// </summary>
        /// <returns>Lista uplata</returns>
        /// <response code = "200">Vraća listu uplata</response>
        /// <response code = "204">Ne postoji ni jedna uplata</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PaymentDto>>> GetPayment(string accountNumber, string referenceNumber)
        {
            List<Payment> PaymentList = paymentRepository.GetPayment(accountNumber, referenceNumber);

            if (PaymentList == null || PaymentList.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Payment list is empty!", "Payment microservice", "GetPayment");

                return NoContent();
            }
            var dtos = new List<PaymentDto>();
            foreach (var p in PaymentList)
            {
                var dto = mapper.Map<PaymentDto>(p);
                dto.publicBinding = await publicBiddingService.SendGetRequestAsync(configuration["Services:PublicBidding"] + p.PublicBiddingId);
                dtos.Add(dto);
            }

            await logger.LogMessage(LogLevel.Information, "Payment list successfully returned!", "Payment microservice", "GetPayment");
            return Ok(dtos);
        }

        /// <summary>
        /// Vraća traženu uplatu po ID-ju
        /// </summary>
        /// <param name="PaymentId">ID uplate</param>
        /// <returns>Tražena uplata</returns>
        /// <response code = "200">Vraća traženu uplatu</response>
        /// <response code = "404">Nije pronađena tražena uplata</response>
        [HttpGet("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDto>> GetPaymentById(Guid paymentId)
        {
            Payment Payment = paymentRepository.GetPaymentById(paymentId);

            var dto = mapper.Map<PaymentDto>(Payment);
            dto.publicBinding = await publicBiddingService.SendGetRequestAsync(configuration["Services:PublicBidding"] + Payment.PublicBiddingId);

            await logger.LogMessage(LogLevel.Information, "Payment successfully returned!", "Payment microservice", "GetPaymentById");
            return Ok(dto);
        }
        /// <summary>
        /// Kreira novu uplatu
        /// </summary>
        /// <param name="payment"> model uplate</param>
        /// <returns>Potvrda o kreiranoj uplati</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove uplate \
        /// POST /api/Payment \
        /// { \
        ///"AccountNumber" : "56285695865825",
        ///"ReferenceNUmber" : "256352",
        ///"Amount" : "20000",
        ///"PurposeOfPayment" : "Uplata prve rate",
        ///"PaymentDate" : "23-05-2020"
        ///"CourseID" : "93a08cc2-1d17-46e6-bd95-4fa70bb11226",
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu uplatu</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja uplate</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentConfirmationDto>> CreatePaymentAsync([FromBody] PaymentCreationDto paymentDto)
        {
            Payment payment = mapper.Map<Payment>(paymentDto);

            var result = await Validator.ValidateAsync(payment);
            if (!result.IsValid)
            {
                await logger.LogMessage(LogLevel.Warning, "Payment validation failded!", "Payment microservice", "CreatePaymentAsync");
                throw new CustomValidationException(result.Errors);
            }

            PaymentConfirmation confirmation = paymentRepository.CreatePayment(payment);
            paymentRepository.SaveChanges();

            string uri = linkGeneration.GetPathByAction("GetPaymentById", "Payment", new { paymentId = confirmation.PaymentId });
            //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
            await logger.LogMessage(LogLevel.Information, "Payment successfully created!", "Payment microservice", "CreatePaymentAsync");
            return Created(uri, mapper.Map<PaymentConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Ažurira jedanu uplatu
        /// </summary>
        /// <param name="payment">Model uplate koja se ažurira</param>
        /// <returns>Potvrda o ažuriranoj uplati</returns>
        /// <response code="200">Vraća ažuriranu uplatu</response>
        /// <response code="404">Nije pronađena uplata za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja uplate</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentDto>> UpdatePaymentAsync(PaymentUpdateDto paymentDto)
        {
            Payment existingPayment = paymentRepository.GetPaymentById(paymentDto.PaymentId);

            Payment payment = mapper.Map<Payment>(paymentDto);

            var result = await Validator.ValidateAsync(payment);
            if (!result.IsValid)
            {
                await logger.LogMessage(LogLevel.Warning, "Payment validation failded!", "Payment microservice", "UpdatePaymentAsync");
                throw new CustomValidationException(result.Errors);
            }

            mapper.Map(payment, existingPayment);
            paymentRepository.SaveChanges();

            PaymentConfirmation confirmation = mapper.Map<PaymentConfirmation>(existingPayment);

            await logger.LogMessage(LogLevel.Information, "Payment successfully updated!", "Payment microservice", "UpdatePaymentAsync");
            return Ok(mapper.Map<PaymentConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Briše uplatu na osnovu ID-ja   
        /// </summary>
        /// <param name="paymentId">ID payment</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uplata je uspešno obrisana</response>
        /// <response code="404">Nije pronađena uplata za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja uplate</response>
        [HttpDelete("paymentId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            Payment payment = paymentRepository.GetPaymentById(paymentId);

            paymentRepository.DeletePayment(payment);
            paymentRepository.SaveChanges();

            await logger.LogMessage(LogLevel.Information, "Payment successfully deleted!", "Payment microservice", "DeletePayment");
            return NoContent(); // Successful deletion -- sve je okej proslo ali ne vraca nikakav sadrzaj--> iz familije je 200
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetAddressOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
