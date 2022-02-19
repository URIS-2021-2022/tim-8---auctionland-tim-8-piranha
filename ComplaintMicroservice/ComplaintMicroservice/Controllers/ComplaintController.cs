using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Entities.Complaint;
using ComplaintMicroservice.Models.Complaint;
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
    [Route("api/complaints")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintRepository complaintRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ComplaintController(IComplaintRepository complaintRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintRepository = complaintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ComplaintDto>> GetComplaints(string solutionNumber)
        {
            List<Complaint> complaints = complaintRepository.GetComplaints(solutionNumber);
            if (complaints == null || complaints.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintDto>>(complaints));
        }

        [HttpGet("{complaintId}")]
        public ActionResult<ComplaintDto> GetComplaintById(Guid complaintId)
        {
            Complaint com = complaintRepository.GetComplaintById(complaintId);

            if (com == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintDto>(com));
        }

        [HttpPost]
        public ActionResult<ComplaintConfirmationDto> CreateComplaintEvent([FromBody] ComplaintCreationDto complaint)
        {
            try
            {

                Complaint com = mapper.Map<Complaint>(complaint);
                ComplaintConfirmation confirmation = complaintRepository.CreateComplaint(com);
                string location = linkGenerator.GetPathByAction("GetComplaintById", "Complaint", new { complaintId = com.ComplaintId });
                return Created(location, mapper.Map<ComplaintConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<ComplaintConfirmationDto> UpdateComplaint(ComplaintUpdateDto complaint)
        {
            try
            {

                var oldComplaint = complaintRepository.GetComplaintById(complaint.ComplaintId);

                if (oldComplaint == null)
                {
                    return NotFound();
                }
                Complaint com = mapper.Map<Complaint>(complaint);
                mapper.Map(com, oldComplaint);
                complaintRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetComplaintById", "Complaint", new { complaintId = com.ComplaintId });
                return Ok(location);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{complaintId}")]
        public IActionResult DeleteComplaint(Guid complaintId)
        {
            try
            {
                Complaint com = complaintRepository.GetComplaintById(complaintId);

                if (com == null)
                {
                    return NotFound();
                }
                complaintRepository.DeleteComplaint(complaintId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }



        [HttpOptions]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
