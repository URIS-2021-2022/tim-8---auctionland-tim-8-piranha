using AutoMapper;
using AddressMicroservice.Data.Interfaces;
using AddressMicroservice.Entities;
using AddressMicroservice.Models.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using CustomValidationException = AddressMicroservice.Models.Exceptions.ValidationException;
using Microsoft.Extensions.Logging;
using DocumentMicroservice.ServiceCalls;

namespace AddressMicroservice.Controllers
{
    [ApiController]
    [Route("api/Address")]
    [Produces("application/json", "application/xml")]
    //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;
        private readonly LinkGenerator linkGeneration;
        private readonly IMapper mapper;
        private readonly IValidator<Address> Validator;
        private readonly ILoggerService logger;

        public AddressController(IAddressRepository addressRepository, LinkGenerator linkGeneration, IMapper mapper, IValidator<Address> Validator, ILoggerService logger)
        {
            this.addressRepository = addressRepository;
            this.linkGeneration = linkGeneration;
            this.mapper = mapper;
            this.Validator = Validator;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve adrese
        /// </summary>
        /// <returns>Lista adresa</returns>
        /// <response code = "200">Vraća listu adresa</response>
        /// <response code = "204">Ne postoji ni jedna adresa</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AddressDto>> GetAddress(string place, string street)
        {
            List<Address> AddressList = addressRepository.GetAddress(place, street);

            if (AddressList == null || AddressList.Count == 0)
            {
                logger.LogMessage(LogLevel.Warning, "Address list is empty!", "Address microservice", "GetAddress");
                return NoContent();
            }

            return Ok(mapper.Map<List<AddressDto>>(AddressList));
        }

        /// <summary>
        /// Vraća traženu adresu po ID-ju
        /// </summary>
        /// <param name="AddressId">ID adrese</param>
        /// <returns>Tražena adresa</returns>
        /// <response code = "200">Vraća traženu adresu</response>
        /// <response code = "404">Nije pronađena tražena adresa</response>
        [HttpGet("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AddressDto> GetAddressById(Guid addressId)
        {
            Address Address = addressRepository.GetAddressById(addressId);

            return Ok(mapper.Map<AddressDto>(Address));
        }
        /// <summary>
        /// Kreira novu adresu
        /// </summary>
        /// <param name="address"> model adrese</param>
        /// <returns>Potvrda o kreiranomoj adresi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove adrese \
        /// POST /api/Address \
        /// { \
        ///"Street" : "Jadranska avenija", \
        ///"StreetNumber" : "23b",\
        ///"Place" = "Zagreb",\
        ///"ZipCode" = "10000",\
        ///"StateID" = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226")\
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu adresu</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja adrese</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressConfirmationDto>> CreateAddressAsync([FromBody] AddressCreationDto addressDto)
        {
            Address address = mapper.Map<Address>(addressDto);

            var result = await Validator.ValidateAsync(address);
            if (!result.IsValid)
            {
                throw new CustomValidationException(result.Errors);
            }

            AddressConfirmation confirmation = addressRepository.CreateAddress(address);
            addressRepository.SaveChanges();

            string uri = linkGeneration.GetPathByAction("GetAddressById", "Address", new { addressId = confirmation.AddressId });
            //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
            return Created(uri, mapper.Map<AddressConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Ažurira jedanu adresu
        /// </summary>
        /// <param name="address">Model adrese koja se ažurira</param>
        /// <returns>Potvrda o ažuriranoj adresi</returns>
        /// <response code="200">Vraća ažuriranu adresu</response>
        /// <response code="404">Nije pronađena adresa za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja adrese</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressDto>> UpdateAddressAsync(AddressUpdateDto addressDto)
        {
            Address existingAddress = addressRepository.GetAddressById(addressDto.AddressId);

            Address address = mapper.Map<Address>(addressDto);

            var result = await Validator.ValidateAsync(address);
            if (!result.IsValid)
            {
                throw new CustomValidationException(result.Errors);
            }

            mapper.Map(address, existingAddress);
            addressRepository.SaveChanges();

            AddressConfirmation confirmation = mapper.Map<AddressConfirmation>(existingAddress);

            return Ok(mapper.Map<AddressConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Briše adresa na osnovu ID-ja
        /// </summary>
        /// <param name="adressId">ID address</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Adresa je uspešno obrisana</response>
        /// <response code="404">Nije pronađena adresa za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja adrese</response>
        [HttpDelete("addressId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAddress(Guid addressId)
        {
            Address address = addressRepository.GetAddressById(addressId);

            addressRepository.DeleteAddress(address);
            addressRepository.SaveChanges();

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
