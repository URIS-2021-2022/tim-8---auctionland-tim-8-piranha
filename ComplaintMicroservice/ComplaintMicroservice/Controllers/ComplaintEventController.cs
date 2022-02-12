using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Data.Event;
using ComplaintMicroservice.Entities.Event;
using ComplaintMicroservice.Models.Event;
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

        [HttpGet]
        public ActionResult<List<ComplaintEventDto>> GetComplaintEvents(string complaintEvent)
        {
            List<ComplaintEvent> events = complaintEventRepository.GetComplaintEvents(complaintEvent);
            if (events == null || events.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintEventDto>>(events));
        }

        [HttpGet("{complaintEventId}")]
        public ActionResult<ComplaintEventDto> GetComplaintEventById(Guid complaintEventId)
        {
            ComplaintEvent ev = complaintEventRepository.GetComplaintEventById(complaintEventId);

            if (ev == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintEventDto>(ev));
        }

        [HttpPost]
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

        [HttpPut]
        public ActionResult<ComplaintEventConfirmationDto> UpdateComplaintEvent(ComplaintEventUpdateDto complaintEvent)
        {
            try
            {

                if (complaintEventRepository.GetComplaintEventById(complaintEvent.ComplaintEventId) == null)
                {
                    return NotFound();
                }
                ComplaintEvent ev = mapper.Map<ComplaintEvent>(complaintEvent);
                ComplaintEventConfirmation confirmaion = complaintEventRepository.UpdateComplaintEvent(ev);
                return Ok(mapper.Map<ComplaintEventConfirmationDto>(confirmaion));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

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
