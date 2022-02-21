using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PersonMicroservice.Data;
using PersonMicroservice.Entities;
using PersonMicroservice.Models;
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

        public PersonController(IPersonRepository personRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.personRepository = personRepository;
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
        public async Task<ActionResult<List<PersonDto>>> GetAllPersonss()
        {
            var persons = await personRepository.GetAllPersons();

            if (persons == null || persons.Count == 0)
            {
                return NoContent();
            }

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
        public async Task<ActionResult<PersonDto>> GetPersonById(Guid personId)
        {
            var person = await personRepository.GetPersonById(personId);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PersonDto>(person));
        }

        /// <summary>
        /// Kreira novu ličnost
        /// </summary>
        /// <param name="board">Model ličnosti</param>
        /// <returns>Potvrda o kreiranju ličnosti</returns>
        /// <response code="201">Vraća kreiranu ličnost</response>
        /// <response code="500">Desila se greška prilikom kreiranja nove ličnosti</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonConfirmationDto>> CreateBoard([FromBody] PersonCreationDto person)
        {
            try
            {
                Person mappedPerson = mapper.Map<Person>(person);

                PersonConfirmation newPerson = await personRepository.CreatePerson(mappedPerson);

                string location = linkGenerator.GetPathByAction("GetPersonById", "Person", new { personId = newPerson.PersonId });

                return Created(location, mapper.Map<PersonConfirmationDto>(newPerson));
            }
            catch (Exception ex)
            {
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
        [HttpPut("{personId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonUpdateDto>> UpdatePerson(Guid personId, [FromBody] PersonUpdateDto person)
        {
            try
            {
                var oldPerson = await personRepository.GetPersonById(personId);

                if (oldPerson == null)
                {
                    return NotFound();
                }

                mapper.Map(person, oldPerson);

                await personRepository.UpdatePerson(mapper.Map<Person>(person));
                
                return Ok(person);
            }
            catch (Exception)
            {
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
        public async Task<ActionResult> DeletePerson(Guid personId)
        {
            try
            {
                var person = await personRepository.GetPersonById(personId);

                if (person == null)
                {
                    return NotFound();
                }

                await personRepository.DeletePerson(personId);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom brisanja licnosti");
            }
        }
    }
}
