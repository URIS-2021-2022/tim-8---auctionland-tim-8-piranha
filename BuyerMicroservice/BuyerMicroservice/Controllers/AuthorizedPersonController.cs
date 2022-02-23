using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.AuthorizedPerson;
using BuyerMicroservice.Models.AuthorizedPersonBuyer;
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
    [Route("api/authorized-persons")]
    [Produces("application/json", "application/xml")]
    public class AuthorizedPersonController : ControllerBase
    {
        private readonly IAuthorizedPersonRepository authorizedPersonRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly AuthorizedPersonValidator validator;
        private readonly IBuyerRepository buyerRepository;
        private readonly ILoggerService logger;



        public AuthorizedPersonController(IAuthorizedPersonRepository authorizedPersonRepository, IMapper mapper, LinkGenerator linkGenerator, AuthorizedPersonValidator validator, IBuyerRepository buyerRepository, ILoggerService logger)
        {
            this.authorizedPersonRepository = authorizedPersonRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.buyerRepository = buyerRepository;
            this.logger = logger;

        }

<<<<<<< HEAD
        /// <summary>
        /// Vraća sva ovlascena lica
        /// </summary>
        /// <returns>Lista ovlascenih lica</returns>
        /// <response code = "200">Vraća listu ovlascenih lica</response>
        /// <response code = "204">Ne postoji nijedno ovlasceno lice</response>
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
        public async Task<ActionResult<List<AuthorizedPersonDto>>> GetAuthorizedPersonAsync(string personalDocNum = null)
        {
            List<AuthorizedPerson> authorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonAsync(personalDocNum);

            if (authorizedPerson == null || authorizedPerson.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Authorized person culture list is empty!", "Buyer microservice", "GetAuthorizedPersonAsync");

                return NoContent();
            }
          await logger.LogMessage(LogLevel.Information, "Authorized person list successfully returned!", "Buyer microservice", "GetAuthorizedPersonAsync");

            return Ok(mapper.Map<List<AuthorizedPersonDto>>(authorizedPerson));
        }

<<<<<<< HEAD
        /// <summary>
        /// Vraća traženo ovlasceno lice po ID-ju
        /// </summary>
        /// <param name="AuthorizedPersonID">ID ovlascenog lica</param>
        /// <returns>Tražena banka</returns>
        /// <response code = "200">Vraća traženo ovlasceno lice</response>
        /// <response code = "404">Nije pronađeno traženo ovlasceno lice</response>
        [HttpGet("{authorizedPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
=======
        [HttpGet("{authorizedPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task<ActionResult<AuthorizedPersonDto>> GetAuthorizedPersonByIdAsync(Guid AuthorizedPersonID)
        {
            AuthorizedPerson authorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(AuthorizedPersonID);

            if (authorizedPerson == null)
            {
              await logger.LogMessage(LogLevel.Warning, "Authorized person not found!", "Buyer microservice", "GetAuthorizedPersonByIdAsync");

                return NotFound();
            }
            await logger.LogMessage(LogLevel.Information, "Authorized person found and successfully returned!", "Buyer microservice", "GetAuthorizedPersonByIdAsync");

            return Ok(mapper.Map<AuthorizedPersonDto>(authorizedPerson));
        }

<<<<<<< HEAD
        /// <summary>
        /// Kreira novo ovlasceno lice
        /// </summary>
        /// <param name="authorizedPersonCreation"> model ovlascenog lica </param>
        /// <returns>Potvrda o kreiranom ovlascenom licu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog ovlascenog lica \
        /// POST /api/authorized-persons \
        /// { \
        ///  "authorizedPersonID" : "07af89f2-feee-4680-b489-9d0e31699588", \
        ///  "boardNumbID" : "21200907-0d08-44f3-8506-dc807ca2215b", \
        ///  "name" : "Marko", \
        ///  "surname" : "Markovic", \
        ///  "personalDocNum" : "8227834666274", \
        ///  "address" : "Bulevar Oslobodjenja 55", \
        ///  "country" : "Srbija", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreirano ovlasceno lice</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja ovlascenog lica</response>
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
        public async Task< ActionResult<AuthorizedPersonConfirmationDto>> CreateAuthorizedPersonAsync([FromBody] AuthorizedPersonCreationDto authorizedPersonCreation)
        {
            try
            {
                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonCreation);

                validator.ValidateAndThrow(authorizedPerson);


                AuthorizedPersonConfirmation authorizedPersonConfirmation =await authorizedPersonRepository.CreateAuthorizedPersonAsync(authorizedPerson);
                await authorizedPersonRepository.SaveChangesAsync();

                
                string uri = linkGenerator.GetPathByAction("GetAuthorizedPerson", "AuthorizedPerson", new { authorizedPersonId = authorizedPersonConfirmation.authorizedPersonID });
               
               await logger.LogMessage(LogLevel.Information, "Authorized person  successfully created!", "Buyer microservice", "CreateAuthorizedPersonAsync");

                return Created(uri, mapper.Map<AuthorizedPersonConfirmationDto>(authorizedPersonConfirmation));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for authorized person object failed!", "Buyer microservice", "CreateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            }
            catch (Exception ex)
            {
               await logger.LogMessage(LogLevel.Error, "Authorized Person object creation failed!", "Buyer microservice", "CreateAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

<<<<<<< HEAD

        /// <summary>
        /// Ažurira jedno ovlasceno lice 
        /// </summary>
        /// <param name="authorizedPersonUpdate">Model ovlascenog lica koje se ažurira</param>
        /// <returns>Potvrda o ažuriranom ovlascenom licu</returns>
        /// <response code="200">Vraća ažuriranu banku</response>
        /// <response code="404">Nije pronađeno ovlasceno lice za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ovlascenog lica</response>
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
        public async  Task<ActionResult<AuthorizedPersonDto>> UpdateAuthorizedPersonAsync(AuthorizedPersonUpdateDto authorizedPersonUpdate)
        {
            try
            {
                AuthorizedPerson existingAuthorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(authorizedPersonUpdate.authorizedPersonID);

                if (existingAuthorizedPerson == null)
                {
                   await logger.LogMessage(LogLevel.Warning, "Authorized Person object not found!", "Buyer microservice", "UpdateAuthorizedPersonAsync");

                    return NotFound();
                }

                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonUpdate);

                validator.ValidateAndThrow(authorizedPerson);

                mapper.Map(authorizedPerson, existingAuthorizedPerson);

                await authorizedPersonRepository.SaveChangesAsync();
               await logger.LogMessage(LogLevel.Information, "Authorized person  object updated successfully!", "Buyer microservice", "UpdateAuthorizedPersonAsync");

                return Ok(mapper.Map<AuthorizedPersonDto>(existingAuthorizedPerson));

            }
            catch (ValidationException ve)
            {
               await logger.LogMessage(LogLevel.Error, "Validation for Authorized person  object failed!", "Buyer microservice", "UpdateAuthorizedPersonAsync");
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
        /// Briše ovlascena lica na osnovu ID-ja
        /// </summary>
        /// <param name="authorizedPersonId">ID ovlascenog lica</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ovlasceno lice uspešno obrisano</response>
        /// <response code="404">Nije pronađeno ovlasceno lice za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ovlascenog lica</response>
=======

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        [HttpDelete("{authorizedPersonId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
<<<<<<< HEAD
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task<IActionResult> DeleteAuthorizedPersonAsync(Guid authorizedPersonId)
        {
            try
            {
                AuthorizedPerson authorizedPerson =await authorizedPersonRepository.GetAuthorizedPersonByIdAsync(authorizedPersonId);

                if (authorizedPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Authorized person object not found!", "Buyer microservice", "DeleteAuthorizedPersonAsync");

                    return NotFound();
                }

                await authorizedPersonRepository.DeleteAuthorizedPersonAsync(authorizedPersonId);
                await authorizedPersonRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Authorized person object deleted successfully!", "Buyer microservice", "DeleteAuthorizedPersonAsync");

                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Authorized erson object deletion failed!", "Buyer microservice", "DeleteAuthorizedPersonAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

<<<<<<< HEAD
        /// <summary>
        /// Povezivanje kupca sa ovlascenim licem
        /// </summary>
        /// <param name="apbDto"> model ovlascenog lica kome se dodaje kupac</param>
        /// <returns>Potvrda o uspesnom ubacivanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove banke \
        /// post /api/authorized-persons/AddBuyer \
        /// </remarks>
        /// <response code = "201"> potvrda o uspesnom povezivanju </response>
        /// <response code = "500">Došlo je do greške na serveru prilikom povezivanja kupca i ovlascenog lica</response>
        [HttpPost("AddBuyer")]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
=======
        [HttpPost("AddBuyer")]
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task<IActionResult> AddAuthorizedPersonBuyer(AuthorizedPersonBuyerDto apbDto)
        {
            Buyer b = await buyerRepository.GetBuyerByIdAsync(apbDto.buyerId);
            await authorizedPersonRepository.AddBuyerToAuthorizedPerson(b, apbDto.authorizedPersonId);
            await authorizedPersonRepository.SaveChangesAsync();

            await logger.LogMessage(LogLevel.Error, "Buyer has been successfully added !", "Buyer microservice", "AddAuthorizedPersonBuyer");

            return NoContent();
        }

<<<<<<< HEAD
        /// <summary>
        /// Brisanje veze ovlascenog lica sa kupcem
        /// </summary>
        /// <param name="apbDto"> model ovlascenog lica kome se brise kupac</param>
        /// <returns>Potvrda o uspesnom brisanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove banke \
        /// DELETE /api/authorized-persons/DeleteBuyer \
        /// </remarks>
        /// <response code = "201"> potvrda o uspesnom brisanju </response>
        /// <response code = "500">Došlo je do greške na serveru prilikom brisanja veze kupca i ovlascenog lica</response>
        [HttpDelete("DeleteBuyer")]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
=======
        [HttpDelete("DeleteBuyer")]
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task<IActionResult> DeleteAuthorizedPersonBuyer(AuthorizedPersonBuyerDto apbDto)
        {
            Buyer b = await buyerRepository.GetBuyerByIdAsync(apbDto.buyerId);
            await authorizedPersonRepository.RemoveBuyerFromAuthorizedPerson(b, apbDto.authorizedPersonId);
            await authorizedPersonRepository.SaveChangesAsync();

           await logger.LogMessage(LogLevel.Error, "Buyer has been successfully deleted !", "Buyer microservice", "DeleteAuthorizedPersonBuyer");


            return NoContent();
        }

<<<<<<< HEAD
        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sva ovlascena lica
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
=======
        [HttpOptions]
        [AllowAnonymous]
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public async Task<IActionResult> GetAuthorizedPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

           await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Buyer microservice", "GetAuthorizedPersonOptions");

            return Ok();
        }

    }
}
