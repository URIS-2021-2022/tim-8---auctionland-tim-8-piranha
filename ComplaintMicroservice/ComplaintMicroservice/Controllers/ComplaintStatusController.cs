using AutoMapper;
using ComplaintMicroservice.Data.Status;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Models.ComplaintStatusDto;
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
    [Route("api/complaintStatus")]
    public class ComplaintStatusController : ControllerBase
    {
        private readonly IComplaintStatusRepository complaintStatusRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ComplaintStatusController(IComplaintStatusRepository complaintStatusRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintStatusRepository = complaintStatusRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ComplaintStatusDto>> GetComplaintStatuses(string Status)
        {
            List<ComplaintStatus> statuses = complaintStatusRepository.GetComplaintStatuses(Status);
            if (statuses == null || statuses.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintStatusDto>>(statuses));
        }

        [HttpGet("{complaintStatusId}")]
        public ActionResult<ComplaintStatusDto> GetComplaintStatusById(Guid complaintStatusId)
        {
            ComplaintStatus status = complaintStatusRepository.GetComplaintStatusById(complaintStatusId);

            if (status == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintStatusDto>(status));
        }

        [HttpPost]
        public ActionResult<ComplaintStatusConfirmationDto> CreateComplaintStatus([FromBody] ComplaintStatusCreationDto complaintStatus)
        {
            try
            {
                bool modelValid = ValidateComplaintType(complaintStatus);

                if (!modelValid)
                {
                    return BadRequest("Complaint status should not be empty");
                }

                ComplaintStatus status = mapper.Map<ComplaintStatus>(complaintStatus);
                ComplaintStatusConfirmation confirmation = complaintStatusRepository.CreateComplaintStatus(status);
                string location = linkGenerator.GetPathByAction("GetComplaintStatusById", "ComplaintStatus", new { complaintStatusId = status.ComplaintStatusId });
                return Created(location, mapper.Map<ComplaintStatusConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<ComplaintStatusConfirmationDto> UpdateComplaintStatus(ComplaintStatusUpdateDto complaintStatus)
        {
            try
            {

                var oldComplaintStatus = complaintStatusRepository.GetComplaintStatusById(complaintStatus.ComplaintStatusId);

                if (oldComplaintStatus == null)
                {
                    return NotFound();
                }
                ComplaintStatus status = mapper.Map<ComplaintStatus>(complaintStatus);
                mapper.Map(status, oldComplaintStatus);
                complaintStatusRepository.SaveChanges();
                return Ok(mapper.Map<ComplaintStatusDto>(oldComplaintStatus));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{complaintStatusId}")]
        public IActionResult DeleteComplaintStatus(Guid complaintStatusId)
        {
            try
            {
                ComplaintStatus status = complaintStatusRepository.GetComplaintStatusById(complaintStatusId);

                if (status == null)
                {
                    return NotFound();
                }
                complaintStatusRepository.DeleteComplaintStatus(complaintStatusId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }



        [HttpOptions]
        public IActionResult GetComplaintStatusOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private bool ValidateComplaintType(ComplaintStatusCreationDto complaintStatus)
        {
            if (complaintStatus.Status== "")
            {
                return false;
            }
            return true;
        }
    }
}
