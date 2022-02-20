using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Data.Event;
using ComplaintMicroservice.Entities.Event;
using ComplaintMicroservice.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Controllers
{
    [ApiController]
    [Route("api/complaintEvents")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class ComplaintEventController : ControllerBase
    {
        private readonly IComplaintEventRepository complaintEventRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ComplaintEventController(IComplaintEventRepository complaintEventRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintEventRepository = complaintEventRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve dogadjaje na osnovu zalbe
        /// </summary>
        /// <param name="complaintEvent">dogadjaj na osnovu zalbe</param>
        /// <returns>Lista dogadjaja na osnovu zalbe</returns>
        /// <response code="200">Vraća listu dogadjaja na osnovu zalbe</response>
        /// <response code="404">Nije pronađen nijedan dogadjaj na osnovu zalbe</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ComplaintEventDto>> GetComplaintEvents(string complaintEvent)
        {
            List<ComplaintEvent> events = complaintEventRepository.GetComplaintEvents(complaintEvent);
            if (events == null || events.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintEventDto>>(events));
        }

        /// <summary>
        /// Vraća jedan dogadjaj na osnovu zalbe na osnovu ID-a
        /// </summary>
        /// <returns>dogadjaj na osnovu zalbe</returns>
        /// <response code="200">Vraća jedan dogadjaj na osnovu zalbe </response>
        /// <response code="404">Nije pronađen dogadjaj na osnovu zalbe sa tim ID-em</response>
        [HttpGet("{complaintEventId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ComplaintEventDto> GetComplaintEventById(Guid complaintEventId)
        {
            ComplaintEvent ev = complaintEventRepository.GetComplaintEventById(complaintEventId);

            if (ev == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintEventDto>(ev));
        }

        /// <summary>
        /// Kreira dogadjaj na osnovu zalbe
        /// </summary>
        /// <returns>Potvrda o kreiranju dogadjaja na osnovu zalbe</returns>
        /// <response code="200">kreiran dogadjaj na osnovu zalbe</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa novog dogadjaja na osnovu zalbe</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintEventConfirmationDto> CreateComplaintEvent([FromBody] ComplaintEventCreationDto complaintEvent)
        {
            try
            {
                bool modelValid = ValidateComplaintType(complaintEvent);

                if (!modelValid)
                {
                    return BadRequest("Complaint event should not be empty");
                }

                ComplaintEvent ev = mapper.Map<ComplaintEvent>(complaintEvent);
                ComplaintEventConfirmation confirmation = complaintEventRepository.CreateComplaintEvent(ev);
                string location = linkGenerator.GetPathByAction("GetComplaintEventById", "ComplaintEvent", new { complaintEventId = ev.ComplaintEventId });
                return Created(location, mapper.Map<ComplaintEventConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan dogadjaj na osnovu zalbe
        /// </summary>
        /// <returns>Potvrdu o modifikovanom dogadjaju na osnovu zalbe</returns>
        /// <response code="200">Vraća ažuriran dogadjaj na osnovu zalbe</response>
        /// <response code="400">Dogadjaj na osnovu zalbe koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dogadjaja na osnovu zalbe</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintEventConfirmationDto> UpdateComplaintEvent(ComplaintEventUpdateDto complaintEvent)
        {
            try
            {
                var oldComplaintEvent = complaintEventRepository.GetComplaintEventById(complaintEvent.ComplaintEventId);

                if (oldComplaintEvent == null)
                {
                    return NotFound();
                }
                ComplaintEvent ev = mapper.Map<ComplaintEvent>(complaintEvent);
                mapper.Map(ev, oldComplaintEvent);
                complaintEventRepository.SaveChanges();
                return Ok(mapper.Map<ComplaintEventDto>(oldComplaintEvent));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog dogadjaja na osnovu zalbe na osnovu ID-a 
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Dogadjaj na osnovu zalbe uspešno obrisan</response>
        /// <response code="404">Nije pronađen dogadjaj na osnovu zalbe za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dogadjaja na osnovu zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{complaintEventId}")]
        public IActionResult DeleteComplaintEvent(Guid complaintEventId)
        {
            try
            {
                ComplaintEvent ev = complaintEventRepository.GetComplaintEventById(complaintEventId);

                if (ev == null)
                {
                    return NotFound();
                }
                complaintEventRepository.DeleteComplaintEvent(complaintEventId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa dogadjajem na osnovu zalbe
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetComplaintEventOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private bool ValidateComplaintType(ComplaintEventCreationDto complaintEvent)
        {
            if (complaintEvent.Event == "")
            {
                return false;
            }
            return true;
        }
    }
}
