using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.ContactPerson;
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
    [Route("api/contact-persons")]
    [Produces("application/json", "application/xml")]
    public class ContactPersonController : ControllerBase
    {

        private readonly IContactPersonRepository contactPersonRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ContactPersonValidator validator;
        private readonly ILoggerService logger;

        public ContactPersonController(IContactPersonRepository contactPersonRepository, IMapper mapper, LinkGenerator linkGenerator, ContactPersonValidator validator, ILoggerService logger)
        {
            this.contactPersonRepository = contactPersonRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger=logger;
         }
<<<<<<< HEAD

        /// <summary>
        /// Vraća sve kontakt osobe
        /// </summary>
        /// <returns>Lista kontakt osoba</returns>
        /// <response code = "200">Vraća listu kontakt osoba</response>
        /// <response code = "204">Ne postoji nijedna kontakt osoba</response>
=======
    

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
<<<<<<< HEAD
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task< ActionResult<List<ContactPersonDto>>> GetContactPersonAsync(string name = null)
        {
            List<ContactPerson> contactPerson =await contactPersonRepository.GetContactPersonAsync(name);

            if (contactPerson == null || contactPerson.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Contact person culture list is empty!", "Buyer microservice", "GetContactPersonAsync");

                return NoContent();
            }
            await logger.LogMessage(LogLevel.Information, "Contact person list successfully returned!", "Buyer microservice", "GetContactPersonAsync");

            return Ok(mapper.Map<List<ContactPersonDto>>(contactPerson));
        }

<<<<<<< HEAD
        /// <summary>
        /// Vraća traženu kontakt osobu po ID-ju
        /// </summary>
        /// <param name="contactPersonID">ID kontakt osobe</param>
        /// <returns>Tražena banka</returns>
        /// <response code = "200">Vraća traženu kontakt osobu</response>
        /// <response code = "404">Nije pronađena tražena kontakt osoba</response>
        [HttpGet("{contactPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
=======
        [HttpGet("{contactPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task< ActionResult<ContactPersonDto>> GetContactPersonByIdAsync(Guid contactPersonID)
        {
            ContactPerson contactPerson = await contactPersonRepository.GetContactPersonByIdAsync(contactPersonID);

            if (contactPerson == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Contact person not found!", "Buyer microservice", "GetContactPersonByIdAsync");

                return NotFound();
            }
            await logger.LogMessage(LogLevel.Information, "Contact person found and successfully returned!", "Buyer microservice", "GetContactPersonByIdAsync");

            return Ok(mapper.Map<ContactPersonDto>(contactPerson));
        }
<<<<<<< HEAD
        /// <summary>
        /// Kreira novu kontakt osobu
        /// </summary>
        /// <param name="contactPersonCreation"> model banke</param>
        /// <returns>Potvrda o kreiranoj kontakt osobi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove kontakt osobe \
        /// POST /api/banke \
        /// { \
        ///  "NazivBanke" : "OTP banka", \
        ///  "Adresa" : "OTP banka", \
        ///  "Grad" : "Novi Sad", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu banku</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje banke nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja banke</response>
=======

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
<<<<<<< HEAD
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task< ActionResult<ContactPersonConfirmationDto>> CreateContactPersonAsync([FromBody] ContactPersonCreationDto contactPersonCreation)
        {
            try
            {
                ContactPerson contactPerson = mapper.Map<ContactPerson>(contactPersonCreation);

                validator.ValidateAndThrow(contactPerson);

                ContactPersonConfirmation contactPersonConfirmation =await contactPersonRepository.CreateContactPersonAsync(contactPerson);
                await contactPersonRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetContactPerson", "ContactPerson", new { contactPersonId = contactPersonConfirmation.contactPersonID });

                await logger.LogMessage(LogLevel.Information, "Contact person  successfully created!", "Buyer microservice", "CreateContactPersonAsync");

                return Created(uri, mapper.Map<ContactPersonConfirmationDto>(contactPersonConfirmation));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Contact person object failed!", "Buyer microservice", "CreateContactPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contact Person object creation failed!", "Buyer microservice", "CreateContactPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

<<<<<<< HEAD
        /// <summary>
        /// Ažurira jednu kontak osobu
        /// </summary>
        /// <param name="contactPersonUpdate">Model kontakt osobe koja se ažurira</param>
        /// <returns>Potvrda o ažuriranoj kontakt osobi</returns>
        /// <response code="200">Vraća ažuriranu kontakt osobu</response>
        /// <response code="404">Nije pronađena kontakt osoba za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja kontakt osobe</response>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
<<<<<<< HEAD
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task< ActionResult<ContactPersonDto>> UpdateContactPersonAsync(ContactPersonUpdateDto contactPersonUpdate)
        {
            try
            {
                ContactPerson existingContactPerson = await contactPersonRepository .GetContactPersonByIdAsync(contactPersonUpdate.contactPersonID);

                if (existingContactPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contact Person object not found!", "Buyer microservice", "UpdateContactPersonAsync");

                    return NotFound();
                }

                ContactPerson contactPerson = mapper.Map<ContactPerson>(contactPersonUpdate);

                validator.ValidateAndThrow(contactPerson);

                mapper.Map(contactPerson, existingContactPerson);

                await contactPersonRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Contact person  object updated successfully!", "Buyer microservice", "UpdateContactPersonAsync");

                return Ok(mapper.Map<ContactPersonDto>(existingContactPerson));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Contact person  object failed!", "Buyer microservice", "UpdateContactPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Authorized person object updating failed!", "Buyer microservice", "UpdateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

<<<<<<< HEAD
        /// <summary>
        /// Briše kontakt osobu na osnovu ID-ja
        /// </summary>
        /// <param name="contactPersonId">ID kontakt osobe</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kontak osoba uspešno obrisana</response>
        /// <response code="404">Nije pronađena kontakt osoba za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja kontakt osobe</response>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        [HttpDelete("{contactPersonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
<<<<<<< HEAD
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async  Task<IActionResult> DeleteContactPersonAsync(Guid contactPersonId)
        {
            try
            {
                ContactPerson contactPerson = await contactPersonRepository.GetContactPersonByIdAsync(contactPersonId);

                if (contactPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contact person object not found!", "Buyer microservice", "DeleteContactPersonAsync");

                    return NotFound();
                }

                await contactPersonRepository.DeleteContactPersonAsync(contactPersonId);
                await contactPersonRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Contact person object deleted successfully!", "Buyer microservice", "DeleteContactPersonAsync");


                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contact person object deletion failed!", "Buyer microservice", "DeleteContactPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
<<<<<<< HEAD
        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve kontakt osobe
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
=======

        [HttpOptions]
        [AllowAnonymous]
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task< IActionResult> GetContactPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetContactPersonOptions");

            return Ok();
        }
    }
}
