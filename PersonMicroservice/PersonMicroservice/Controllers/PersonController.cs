using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PersonMicroservice.Data;
using PersonMicroservice.Entities;
using PersonMicroservice.Models;
using PersonMicroservice.ServiceCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Controllers
{
    /// <summary>
    /// Kontroler za licnost
    /// </summary>
    [ApiController]
    [Route("api/person")]
    [Produces("application/json", "application/xml")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService logger;

        public PersonController(IPersonRepository personRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService logger)
        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.personRepository = personRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve ličnosti
        /// </summary>
        /// <returns>Lista ličnosti</returns>
        /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="404">Nije pronađena ni jedna ličnost</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<List<PersonDto>>> GetAllPersonss()
        {
            var persons = await personRepository.GetAllPersons();

            if (persons == null || persons.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Person list is empty!", "Person microservice", "GetAllPersonss");
                return NoContent();
            }

            await logger.LogMessage(LogLevel.Information, "Person list successfully returned!", "Person microservice", "GetAllPersonss");
            return Ok(mapper.Map<List<PersonDto>>(persons));
        }

        /// <summary>
        /// Vraća jednu ličnost sa prosleđenim ID-em
        /// </summary>
        /// <param name="personId">ID ličnosti</param>
        /// <returns>Komisija</returns>
        /// <response code="200">Vraća traženu ličnost</response>
        /// <response code="404">Nije pronađena ličnost sa unetim ID-em</response>
        [HttpGet("{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<PersonDto>> GetPersonById(Guid personId)
        {
            var person = await personRepository.GetPersonById(personId);

            if (person == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Person not found!", "Person microservice", "GetPersonById");
                return NotFound();
            }

            await logger.LogMessage(LogLevel.Information, "Person found and successfully returned!", "Person microservice", "GetPersonById");
            return Ok(mapper.Map<PersonDto>(person));
        }

        /// <summary>
        /// Kreira novu ličnost
        /// </summary>
        /// <param name="person">Model ličnosti</param>
        /// <returns>Potvrda o kreiranju ličnosti</returns>
        /// <response code="201">Vraća kreiranu ličnost</response>
        /// <response code="500">Desila se greška prilikom kreiranja nove ličnosti</response>
        /// <remarks>
        /// Primer POST zahteva \
        /// POST /api/person \
        /// { \
        ///     "name": "Dragan", \
        ///     "surname": "Majkic", \
        ///     "function": "Member" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<PersonConfirmationDto>> CreatePerson([FromBody] PersonCreationDto person)
        {
            try
            {
                Person mappedPerson = mapper.Map<Person>(person);

                PersonConfirmation newPerson = await personRepository.CreatePerson(mappedPerson);

                string location = linkGenerator.GetPathByAction("GetPersonById", "Person", new { personId = newPerson.PersonId });

                await logger.LogMessage(LogLevel.Information, "Person successfully created!", "Person microservice", "CreatePerson");
                return Created(location, mapper.Map<PersonConfirmationDto>(newPerson));
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Person object creation failed!", "Person microservice", "CreatePerson");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja nove ličnosti." + ex);
            }
        }

        ///<summary>
        /// Modifikacija ličnosti
        /// </summary>
        /// <param name="person">Model ličnosti</param>
        /// <param name="personId">Id ličnosti</param>
        /// <returns>Potvrda o izmeni ličnosti</returns>
        /// <response code="200">Izmenjena ličnost</response>
        /// <response code="404">Nije pronađena ličnost sa unetim ID-em</response>
        /// <response code="500">Serverska greška tokom izmene ličnosti</response>
        /// /// <remarks>
        /// Primer PUT zahteva \
        /// PUT /api/person \
        /// {   \
        ///    "personId": "81f63012-16d7-4f1a-a330-55dc295a6dcd",\
        ///    "name": "Miha",\
        ///    "surname": "Strajin",\
        ///    "function": "Member"\
        /// }
        /// </remarks>
        [HttpPut("{personId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<PersonUpdateDto>> UpdatePerson(Guid personId, [FromBody] PersonUpdateDto person)
        {
            try
            {
                var oldPerson = await personRepository.GetPersonById(personId);

                if (oldPerson == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Person object not found!", "Person microservice", "UpdatePerson");
                    return NotFound();
                }

                mapper.Map(person, oldPerson);

                await personRepository.UpdatePerson(mapper.Map<Person>(person));

                await logger.LogMessage(LogLevel.Information, "Person object updated successfully!", "Person microservice", "UpdatePerson");
                return Ok(person);
            }
            catch (Exception)
            {
                await logger.LogMessage(LogLevel.Error, "Person object updating failed!", "Person microservice", "UpdatePerson");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom modifikacije licnosti.");
            }
        }

        /// <summary>
        /// Brisanje ličnosti na osnovu ID-a
        /// </summary>
        /// <param name="personId">ID ličnosti</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ličnost je uspešno obrisana</response>
        /// <response code="404">Nije pronađena ličnost za uneti ID</response>
        /// <response code="500">Serverska greška tokom brisanja ličnosti</response>
        [HttpDelete("{personId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult> DeletePerson(Guid personId)
        {
            try
            {
                var person = await personRepository.GetPersonById(personId);

                if (person == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Person object not found!", "Person microservice", "DeletePerson");
                    return NotFound();
                }

                await personRepository.DeletePerson(personId);

                await logger.LogMessage(LogLevel.Information, "Person object deleted successfull!", "Person microservice", "DeletePerson");
                return Ok();
            }
            catch (Exception)
            {
                await logger.LogMessage(LogLevel.Error, "Person object deletion failed!", "Person microservice", "DeletePerson");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom brisanja licnosti");
            }
        }

        /// <summary>
        /// Zaglavlje odgovora
        /// </summary>
        /// <returns>Zaglavlje odgovora</returns>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<IActionResult> GetPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Person microservice", "GetPersonOptions");

            return Ok();
        }
    }
}
