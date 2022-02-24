using AutoMapper;
using ComplaintMicroservice.Data.Status;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Models.ComplaintStatusDto;
using ComplaintMicroservice.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Controllers
{
    [ApiController]
    [Route("api/complaints/complaintStatus")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class ComplaintStatusController : ControllerBase
    {
        private readonly IComplaintStatusRepository complaintStatusRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService Logger;

        public ComplaintStatusController(IComplaintStatusRepository complaintStatusRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.complaintStatusRepository = complaintStatusRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            Logger = loggerService;
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
        public async Task<ActionResult<List<ComplaintStatusDto>>> GetComplaintStatuses(string Status)
        {
            List<ComplaintStatus> statuses = await complaintStatusRepository.GetComplaintStatuses(Status);
            if (statuses == null || statuses.Count == 0)
            {
                await Logger .LogMessage(LogLevel.Warning, "Complaint statuses list is empty!", "Complaint microservice", "GetComplaintStatuses");
                return NoContent();
            }
            await Logger.LogMessage(LogLevel.Information, "Complaint statuses list successfully returned!", "Complaint microservice", "GetComplaintStatuses");
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
        public async Task<ActionResult<ComplaintStatusDto>> GetComplaintStatusById(Guid complaintStatusId)
        {
            ComplaintStatus status = await complaintStatusRepository.GetComplaintStatusById(complaintStatusId);

            if (status == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Complaint status not found!", "Complaint microservice", "GetComplaintStatusById");
                return NotFound();
            }
            await Logger.LogMessage(LogLevel.Information, "Complaint status found and successfully returned!", "Complaint microservice", "GetComplaintStatusById");
            return Ok(mapper.Map<ComplaintStatusDto>(status));
        }

        /// <summary>
        /// Kreira status zalbe
        /// </summary>
        /// <returns>Kreiran status zalbe</returns>
        /// <response code="200">Vraća listu statusa zalbe</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa novog statusa zalbe</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComplaintStatusConfirmationDto>> CreateComplaintStatus([FromBody] ComplaintStatusCreationDto complaintStatus)
        {
            try
            {
                bool modelValid = ValidateComplaintType(complaintStatus);

                if (!modelValid)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint status is not valid!", "Complaint microservice", "CreateComplaintStatus");
                    return BadRequest("Complaint status should not be empty");
                }

                ComplaintStatus status = mapper.Map<ComplaintStatus>(complaintStatus);
                ComplaintStatusConfirmation confirmation = await complaintStatusRepository.CreateComplaintStatus(status);
                string location = linkGenerator.GetPathByAction("GetComplaintStatusById", "ComplaintStatus", new { complaintStatusId = status.ComplaintStatusId });
                await Logger.LogMessage(LogLevel.Information, "Complaint status successfully created!", "Complaint microservice", "CreateComplaintStatus");
                return Created(location, mapper.Map<ComplaintStatusConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "ComplaintStatus creation failed!", "Complaint microservice", "CreateComplaintStatus");
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
        public async Task<ActionResult<ComplaintStatusConfirmationDto>> UpdateComplaintStatus(ComplaintStatusUpdateDto complaintStatus)
        {
            try
            {

                var oldComplaintStatus = complaintStatusRepository.GetComplaintStatusById(complaintStatus.ComplaintStatusId);

                if (oldComplaintStatus == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint status not found!", "Complaint microservice", "UpdateComplaintStatus");
                    return NotFound();
                }
                ComplaintStatus status = mapper.Map<ComplaintStatus>(complaintStatus);
                await mapper.Map(status, oldComplaintStatus);
                await complaintStatusRepository.SaveChanges();
                await Logger.LogMessage(LogLevel.Information, "Complaint status successfully updated!", "Complaint microservice", "UpdateComplaintStatus");
                return Ok(mapper.Map<ComplaintStatusDto>(oldComplaintStatus));
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "ComplaintStatus update failed!", "Complaint microservice", "UpdateComplaintStatus");
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
        public async Task<IActionResult> DeleteComplaintStatus(Guid complaintStatusId)
        {
            try
            {
                ComplaintStatus status = await complaintStatusRepository.GetComplaintStatusById(complaintStatusId);

                if (status == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint status not found!", "Complaint microservice", "DeleteComplaintStatus");
                    return NotFound();
                }
                await complaintStatusRepository.DeleteComplaintStatus(complaintStatusId);
                await Logger.LogMessage(LogLevel.Information, "Complaint status deleted successfully!", "Complaint microservice", "DeleteComplaintStatus");
                return NoContent();
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "Complaint status deletion failed!", "Complaint microservice", "DeleteComplaintStatus");
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

        private static bool ValidateComplaintType(ComplaintStatusCreationDto complaintStatus)
        {
            if (complaintStatus.Status== "")
            {
                return false;
            }
            return true;
        }
    }
}
