﻿using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Data.Event;
using ComplaintMicroservice.Entities.Event;
using ComplaintMicroservice.Models.Event;
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
    [Route("api/complaints/complaintEvents")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class ComplaintEventController : ControllerBase
    {
        private readonly IComplaintEventRepository complaintEventRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService Logger;

        public ComplaintEventController(IComplaintEventRepository complaintEventRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.complaintEventRepository = complaintEventRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            Logger = loggerService;
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
        public async Task<ActionResult<List<ComplaintEventDto>>> GetComplaintEvents(string complaintEvent)
        {
            List<ComplaintEvent> events = await complaintEventRepository.GetComplaintEvents(complaintEvent);
            if (events == null || events.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Complaint events list is empty!", "Complaint microservice", "GetComplaintEvents");
                return NoContent();
            }
            await Logger.LogMessage(LogLevel.Information, "Complaint events list successfully returned!", "Complaint microservice", "GetComplaintEvents");
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
        public async Task<ActionResult<ComplaintEventDto>> GetComplaintEventById(Guid complaintEventId)
        {
            ComplaintEvent ev = await complaintEventRepository.GetComplaintEventById(complaintEventId);

            if (ev == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Complaint event not found!", "Complaint microservice", "GetComplaintEventById");
                return NotFound();
            }
            await Logger.LogMessage(LogLevel.Information, "Complaint event found and successfully returned!", "Complaint microservice", "GetComplaintEventById");
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
        public async Task<ActionResult<ComplaintEventConfirmationDto>> CreateComplaintEvent([FromBody] ComplaintEventCreationDto complaintEvent)
        {
            try
            {
                bool modelValid = ValidateComplaintType(complaintEvent);

                if (!modelValid)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint event is not valid!", "Complaint microservice", "CreateComplaintEvent");
                    return BadRequest("Complaint event should not be empty");
                }

                ComplaintEvent ev = mapper.Map<ComplaintEvent>(complaintEvent);
                ComplaintEventConfirmation confirmation = await complaintEventRepository.CreateComplaintEvent(ev);
                string location = linkGenerator.GetPathByAction("GetComplaintEventById", "ComplaintEvent", new { complaintEventId = ev.ComplaintEventId });
                await Logger.LogMessage(LogLevel.Information, "Complaint event successfully created!", "Complaint microservice", "CreateComplaintEvent");
                return Created(location, mapper.Map<ComplaintEventConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "ComplaintEvent creation failed!", "Complaint microservice", "CreateComplaintEvent");
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
        public async Task<ActionResult<ComplaintEventConfirmationDto>> UpdateComplaintEvent(ComplaintEventUpdateDto complaintEvent)
        {
            try
            {
                var oldComplaintEvent = await complaintEventRepository.GetComplaintEventById(complaintEvent.ComplaintEventId);

                if (oldComplaintEvent == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint event not found!", "Complaint microservice", "UpdateComplaintEvent");
                    return NotFound();
                }
                ComplaintEvent ev = mapper.Map<ComplaintEvent>(complaintEvent);
                mapper.Map(ev, oldComplaintEvent);
                await complaintEventRepository.SaveChanges();
                await Logger.LogMessage(LogLevel.Information, "Complaint event successfully updated!", "Complaint microservice", "UpdateComplaintEvent");
                return Ok(mapper.Map<ComplaintEventDto>(oldComplaintEvent));
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "ComplaintEvent update failed!", "Complaint microservice", "UpdateComplaintType");
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
        public async Task<IActionResult> DeleteComplaintEvent(Guid complaintEventId)
        {
            try
            {
                ComplaintEvent ev = await complaintEventRepository.GetComplaintEventById(complaintEventId);

                if (ev == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint event not found!", "Complaint microservice", "DeleteComplaintEvent");
                    return NotFound();
                }
                await complaintEventRepository.DeleteComplaintEvent(complaintEventId);
                await Logger.LogMessage(LogLevel.Information, "Complaint event deleted successfully!", "Complaint microservice", "DeleteComplaintEvent");
                return NoContent();
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "Complaint event deletion failed!", "Complaint microservice", "DeleteComplaintEvent");
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

        private static bool ValidateComplaintType(ComplaintEventCreationDto complaintEvent)
        {
            if (complaintEvent.Event == "")
            {
                return false;
            }
            return true;
        }
    }
}
