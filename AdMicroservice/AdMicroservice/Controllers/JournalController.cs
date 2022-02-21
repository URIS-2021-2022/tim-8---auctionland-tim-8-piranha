using AdMicroservice.Data.Journal;
using AdMicroservice.Entities.Journal;
using AdMicroservice.Models.Journal;
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
    [Route("api/ads/journals")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class JournalController : ControllerBase
    {
        private readonly IJournalRepository journalRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService Logger;

        public JournalController(IJournalRepository journalRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.journalRepository= journalRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            Logger = loggerService;
        }

        /// <summary>
        /// Vraća sve sluzbene listove
        /// </summary>
        /// <param name="journalNumber">Broj sluzbenog lista</param>
        /// <returns>Lista sluzbenih listova</returns>
        /// <response code="200">Vraća listu sluzbenih listova</response>
        /// <response code="404">Nije pronađen nijedan sluzbeni list</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<JournalDto>> GetJournals(string journalNumber)
        {
            List<JournalModel> journals = journalRepository.GetJournals(journalNumber);
            if (journals == null || journals.Count == 0)
            {
                Logger.LogMessage(LogLevel.Warning, "Journals list is empty!", "Ad microservice", "GetJournals");
                return NoContent();
            }
            Logger.LogMessage(LogLevel.Information, "Journals list successfully returned!", "Ad microservice", "GetJournals");
            return Ok(mapper.Map<List<JournalDto>>(journals));
        }

        /// <summary>
        /// Vraća jedan sluzbeni list na osnovu ID-a
        /// </summary>
        /// <returns>sluzbeni list</returns>
        /// <response code="200">Vraća jedan sluzbeni list</response>
        /// <response code="404">Nije pronađen sluzbeni list sa tim ID-em</response>
        [HttpGet("{journalId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<JournalDto> GetJournalById(Guid journalId)
        {
            JournalModel journal = journalRepository.GetJournalById(journalId);

            if (journal == null)
            {
                Logger.LogMessage(LogLevel.Warning, "Journal not found!", "Ad microservice", "GetJournalById");
                return NotFound();
            }
            Logger.LogMessage(LogLevel.Information, "Journal found and successfully returned!", "Ad microservice", "GetJournalById");
            return Ok(mapper.Map<JournalDto>(journal));
        }

        /// <summary>
        /// Kreira sluzbeni list
        /// </summary>
        /// <returns>Kreiran sluzbeni list</returns>
        /// <response code="200">Vraća potvrdu o kreiranju sluzbenog lista</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa novog sluzbenog lista</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JournalConfirmationDto> CreateJournal([FromBody] JournalCreationDto journal)
        {
            try
            {
                JournalModel jr = mapper.Map<JournalModel>(journal);
                JournalConfirmation confirmation = journalRepository.CreateJournal(jr);
                string location = linkGenerator.GetPathByAction("GetJournalById", "Journal", new { journalId = jr.JournalId });
                Logger.LogMessage(LogLevel.Information, "Journal successfully created!", "Ad microservice", "CreateJournal");
                return Created(location, mapper.Map<JournalConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                Logger.LogMessage(LogLevel.Error, "Journal creation failed!", "Ad microservice", "CreateJournal");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan sluzbeni list
        /// </summary>
        /// <returns>Potvrdu o modifikovanom sluzbenom listu</returns>
        /// <response code="200">Vraća ažuriran sluzbeni list</response>
        /// <response code="400">sluzbeni list koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja sluzbenog lista</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JournalConfirmationDto> UpdateJournal(JournalUpdateDto journal)
        {
            try
            {

                var oldJournal = journalRepository.GetJournalById(journal.JournalId);

                if (oldJournal == null)
                {
                    Logger.LogMessage(LogLevel.Warning, "Journal not found!", "Ad microservice", "UpdateJournal");
                    return NotFound();
                }
                JournalModel jr = mapper.Map<JournalModel>(journal);
                mapper.Map(jr, oldJournal);
                journalRepository.SaveChanges();
                Logger.LogMessage(LogLevel.Information, "Journal successfully updated!", "Ad microservice", "UpdateJournal");
                return Ok(mapper.Map<JournalDto>(oldJournal));
            }
            catch
            {
                Logger.LogMessage(LogLevel.Error, "Journal update failed!", "Ad microservice", "UpdateJournal");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog sluzbenog lista na osnovu ID-a 
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Sluzbeni list uspešno obrisan</response>
        /// <response code="404">Nije pronađen sluzbeni list za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja sluzbenog lista</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{journalId}")]
        public IActionResult DeleteJournal(Guid journalId)
        {
            try
            {
                JournalModel journal = journalRepository.GetJournalById(journalId);

                if (journal == null)
                {
                    Logger.LogMessage(LogLevel.Warning, "Journal not found!", "Ad microservice", "DeleteJournal");
                    return NotFound();
                }
                journalRepository.DeleteJournal(journalId);
                Logger.LogMessage(LogLevel.Information, "Journal deleted successfully!", "Ad microservice", "DeleteJournal");
                return NoContent();
            }
            catch
            {
                Logger.LogMessage(LogLevel.Error, "Journal deletion failed!", "Ad microservice", "DeleteJournal");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa sluzbenim listom
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetJournalOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
