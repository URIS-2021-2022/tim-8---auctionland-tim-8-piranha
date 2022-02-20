using AutoMapper;
using ComplaintMicroservice.Data.Status;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Models.ComplaintStatusDto;
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
    [Route("api/complaintStatus")]
    [Produces("application/json", "application/xml")]
    [Authorize]
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

        /// <summary>
        /// Vraća sve statuse zalbe
        /// </summary>
        /// <param name="Status">Status zalbe</param>
        /// <returns>Lista statusa zalbe</returns>
        /// <response code="200">Vraća listu statusa zalbe</response>
        /// <response code="404">Nije pronađen nijedan status zalbe</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ComplaintStatusDto>> GetComplaintStatuses(string Status)
        {
            List<ComplaintStatus> statuses = complaintStatusRepository.GetComplaintStatuses(Status);
            if (statuses == null || statuses.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintStatusDto>>(statuses));
        }


        /// <summary>
        /// Vraća jedan status zalbe na osnovu ID-a
        /// </summary>
        /// <returns>Status zalbe</returns>
        /// <response code="200">Vraća jedan status zalbe</response>
        /// <response code="404">Nije pronađen status zalbe sa tim ID-em</response>
        [HttpGet("{complaintStatusId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ComplaintStatusDto> GetComplaintStatusById(Guid complaintStatusId)
        {
            ComplaintStatus status = complaintStatusRepository.GetComplaintStatusById(complaintStatusId);

            if (status == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintStatusDto>(status));
        }

        /// <summary>
        /// Kreira status zalbe
        /// </summary>
        /// <returns>Potvrda o kreiranju statusa zalbe</returns>
        /// <response code="200">Vraća listu statusa zalbe</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa novog statusa zalbe</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Ažurira jedan status zalbe
        /// </summary>
        /// <returns>Potvrdu o modifikovanom statusu zalbe</returns>
        /// <response code="200">Vraća ažuriran status zalbe</response>
        /// <response code="400">Status zalbe koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja statusa zalbe</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Vrši brisanje jednog statusa zalbe na osnovu ID-a 
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Status zalbe uspešno obrisan</response>
        /// <response code="404">Nije pronađen status zalbe za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja statusa zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Vraća opcije za rad sa statusom zalbe
        /// </summary>
        /// <returns></returns>
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
