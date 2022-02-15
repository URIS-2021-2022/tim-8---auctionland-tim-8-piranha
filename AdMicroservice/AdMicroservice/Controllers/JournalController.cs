using AdMicroservice.Data.Journal;
using AdMicroservice.Entities.Journal;
using AdMicroservice.Models.Journal;
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
    [Route("api/journals")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalRepository journalRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public JournalController(IJournalRepository journalRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.journalRepository= journalRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<JournalDto>> GetJournals(string journalNumber)
        {
            List<JournalModel> journals = journalRepository.GetJournals(journalNumber);
            if (journals == null || journals.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<JournalDto>>(journals));
        }

        [HttpGet("{journalId}")]
        public ActionResult<JournalDto> GetJournalById(Guid journalId)
        {
            JournalModel journal = journalRepository.GetJournalById(journalId);

            if (journal == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<JournalDto>(journal));
        }

        [HttpPost]
        public ActionResult<JournalConfirmationDto> CreateJournal([FromBody] JournalCreationDto journal)
        {
            try
            {
                JournalModel jr = mapper.Map<JournalModel>(journal);
                JournalConfirmation confirmation = journalRepository.CreateJournal(jr);
                string location = linkGenerator.GetPathByAction("GetJournalById", "Journal", new { journalId = jr.JournalId });
                return Created(location, mapper.Map<JournalConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<JournalConfirmationDto> UpdateJournal(JournalUpdateDto journal)
        {
            try
            {

                if (journalRepository.GetJournalById(journal.JournalId) == null)
                {
                    return NotFound();
                }
                JournalModel jr = mapper.Map<JournalModel>(journal);
                JournalConfirmation confirmaion = journalRepository.UpdateJournal(jr);
                return Ok(mapper.Map<JournalConfirmationDto>(confirmaion));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{adId}")]
        public IActionResult DeleteJournal(Guid journalId)
        {
            try
            {
                JournalModel journal = journalRepository.GetJournalById(journalId);

                if (journal == null)
                {
                    return NotFound();
                }
                journalRepository.DeleteJournal(journalId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }



        [HttpOptions]
        public IActionResult GetJournalOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
