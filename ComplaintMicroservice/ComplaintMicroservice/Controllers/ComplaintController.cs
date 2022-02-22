using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Data.Event;
using ComplaintMicroservice.Data.Status;
using ComplaintMicroservice.Entities.Complaint;
using ComplaintMicroservice.Models;
using ComplaintMicroservice.Models.Complaint;
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
    [Route("api/complaints")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintRepository complaintRepository;
        private readonly IComplaintTypeRepository complaintTypeRepository;
        private readonly IComplaintStatusRepository complaintStatusRepository;
        private readonly IComplaintEventRepository complaintEventRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService Logger;
        private readonly IServiceCall<PublicBiddingDto> publicBiddingService;
        private readonly IServiceCall<BuyerDto> BuyerService;

        public ComplaintController(IComplaintRepository complaintRepository, LinkGenerator linkGenerator, 
            IMapper mapper, IComplaintTypeRepository complaintTypeRepository, 
            IComplaintStatusRepository complaintStatusRepository, 
            IComplaintEventRepository complaintEventRepository, ILoggerService loggerService, 
            IServiceCall<PublicBiddingDto> publicBiddingService, IServiceCall<BuyerDto> BuyerService)
        {
            this.complaintRepository = complaintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.complaintTypeRepository = complaintTypeRepository;
            this.complaintStatusRepository = complaintStatusRepository;
            this.complaintEventRepository = complaintEventRepository;
            Logger = loggerService;
            this.publicBiddingService = publicBiddingService;
            this.BuyerService = BuyerService;
        }

        /// <summary>
        /// Vraća sve zalbe
        /// </summary>
        /// <param name="solutionNumber">Broj zalbe</param>
        /// <returns>Lista zalbi</returns>
        /// <response code="200">Vraća listu zalb</response>
        /// <response code="404">Nije pronađena nijedna zalba</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ComplaintDto>>> GetComplaints(string solutionNumber)
        {
            List<Complaint> complaints = await complaintRepository.GetComplaints(solutionNumber);
            if (complaints == null || complaints.Count == 0)
            {
                await Logger.LogMessage(LogLevel.Warning, "Complaints list is empty!", "Complaint microservice", "GetComplaints");
                return NoContent();
            }

            List<ComplaintDto> complaintDtos = new List<ComplaintDto>();

            foreach(var complaint in complaints)
            {
                ComplaintDto complaintDto = mapper.Map<ComplaintDto>(complaint);

                if(complaint.PublicBiddingId is not null)
                {
#pragma warning disable S1075
                    var publicBiddingDto = await publicBiddingService.SendGetRequestAsync("http://localhost:40010");
#pragma warning restore S1075
                    if (publicBiddingDto is not null)
                    {
                        complaintDto.PublicBidding = publicBiddingDto;
                    }
                }

                if(complaint.BuyerId is not null)
                {
#pragma warning disable S1075
                    var buyerDto = await BuyerService.SendGetRequestAsync("http://localhost:40004");
#pragma warning restore S1075
                    if (buyerDto is not null)
                    {
                        complaintDto.Buyer = buyerDto;
                    }
                }

                complaintDtos.Add(complaintDto);
            }

            await Logger.LogMessage(LogLevel.Information, "Complaints list successfully returned!", "Complaint microservice", "GetComplaints");
            return Ok(complaintDtos);
        }

        /// <summary>
        /// Vraća jednu zalbu na osnovu ID-a
        /// </summary>
        /// <returns>Zalba</returns>
        /// <response code="200">Vraća jednu zalbu</response>
        /// <response code="404">Nije pronađena zalba sa tim ID-em</response>
        [HttpGet("{complaintId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ComplaintDto>> GetComplaintById(Guid complaintId)
        {
            Complaint com = await complaintRepository.GetComplaintById(complaintId);

            if (com == null)
            {
                await Logger.LogMessage(LogLevel.Warning, "Complaint not found!", "Complaint microservice", "GetComplaintById");
                return NotFound();
            }

            ComplaintDto complaintDto = mapper.Map<ComplaintDto>(com);

            if(com.PublicBiddingId is not null)
            {
#pragma warning disable S1075
                var publicBiddingDto = await publicBiddingService.SendGetRequestAsync("http://localhost:40010");
#pragma warning restore S1075
                if (publicBiddingDto is not null)
                {
                    complaintDto.PublicBidding = publicBiddingDto;
                }
#pragma warning disable S1075
                var buyerDto = await BuyerService.SendGetRequestAsync("http://localhost:40004");
#pragma warning restore S1075
                if (buyerDto is not null)
                {
                    complaintDto.Buyer = buyerDto;
                }
            }

            await Logger.LogMessage(LogLevel.Information, "Complaint found and successfully returned!", "Complaint microservice", "GetComplaintById");
            return Ok(complaintDto);
        }

        /// <summary>
        /// Kreira zalbu
        /// </summary>
        /// <returns>Potvrda o kreiranju zalbe</returns>
        /// <response code="200">Vraća listu zalbi</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa nove zalbe</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComplaintConfirmationDto>> CreateComplaintEvent([FromBody] ComplaintCreationDto complaint)
        {
            try
            {

                Complaint com = mapper.Map<Complaint>(complaint);
                ComplaintConfirmation confirmation = await complaintRepository.CreateComplaint(com);
                string location = linkGenerator.GetPathByAction("GetComplaintById", "Complaint", new { complaintId = com.ComplaintId });
                await Logger.LogMessage(LogLevel.Information, "Complaint successfully created!", "Complaint microservice", "CreateComplaint");
                return Created(location, mapper.Map<ComplaintConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Complaint creation failed!", "Complaint microservice", "CreateComplaint");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jednu zalbu
        /// </summary>
        /// <returns>Potvrdu o modifikovanoj zalbi</returns>
        /// <response code="200">Vraća ažuriranu zalbu</response>
        /// <response code="400">Zalba kojia se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja zalbe</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComplaintConfirmationDto>> UpdateComplaint(ComplaintUpdateDto complaint)
        {
            try
            {

                var oldComplaint = await complaintRepository.GetComplaintById(complaint.ComplaintId);

                if (oldComplaint == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint not found!", "Complaint microservice", "UpdateComplaint");
                    return NotFound();
                }
                Complaint com = mapper.Map<Complaint>(complaint);
                mapper.Map(com, oldComplaint);
                com.ComplaintType = await complaintTypeRepository.GetComplaintTypeById(oldComplaint.ComplaintTypeId);
                com.ComplaintStatus = await complaintStatusRepository.GetComplaintStatusById(oldComplaint.ComplaintStatusId);
                com.ComplaintEvent = await complaintEventRepository.GetComplaintEventById(oldComplaint.ComplaintEventId);
                await complaintRepository.UpdateComplaint(com);
                string location = linkGenerator.GetPathByAction("GetComplaintById", "Complaint", new { complaintId = com.ComplaintId });
                await Logger.LogMessage(LogLevel.Information, "Complaint successfully updated!", "Complaint microservice", "UpdateComplaint");
                return Ok(com);
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "Complaint update failed!", "Complaint microservice", "UpdateComplaint");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne zalbe na osnovu ID-a 
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Zalba uspešno obrisana</response>
        /// <response code="404">Nije pronađena zalba za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{complaintId}")]
        public async Task<IActionResult> DeleteComplaint(Guid complaintId)
        {
            try
            {
                Complaint com = await complaintRepository.GetComplaintById(complaintId);

                if (com == null)
                {
                    await Logger.LogMessage(LogLevel.Warning, "Complaint not found!", "Complaint microservice", "DeleteComplaint");
                    return NotFound();
                }
                await complaintRepository.DeleteComplaint(complaintId);
                await Logger.LogMessage(LogLevel.Information, "Complaint deleted successfully!", "Complaint microservice", "DeleteComplaint");
                return NoContent();
            }
            catch
            {
                await Logger.LogMessage(LogLevel.Error, "Complaint deletion failed!", "Complaint microservice", "DeleteComplaint");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa zalbom
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
