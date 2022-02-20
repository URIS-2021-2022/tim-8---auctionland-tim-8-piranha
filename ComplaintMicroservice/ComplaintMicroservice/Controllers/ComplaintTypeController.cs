using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
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
    [Route("api/complaintTypes")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class ComplaintTypeController : ControllerBase
    {
        private readonly IComplaintTypeRepository complaintTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ComplaintTypeController(IComplaintTypeRepository complaintTypeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintTypeRepository = complaintTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve tipove zalbe
        /// </summary>
        /// <param name="complaintType">Tip zalbe</param>
        /// <returns>Lista tipova zalbe</returns>
        /// <response code="200">Vraća listu tipova zalbe</response>
        /// <response code="404">Nije pronađen nijedan tip zalbe</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ComplaintTypeDto>> GetComplaintTypes(string complaintType)
        {
            List<ComplaintTypeModel> types = complaintTypeRepository.GetComplaintTypes(complaintType);
            if(types==null || types.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintTypeDto>>(types));
        }

        /// <summary>
        /// Vraća jedan tip zalbe na osnovu ID-a
        /// </summary>
        /// <returns>Tip zalbe</returns>
        /// <response code="200">Vraća jedan tip zalbe</response>
        /// <response code="404">Nije pronađen tip zalbe sa tim ID-em</response>
        [HttpGet("{complaintTypeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ComplaintTypeDto> GetComplaintTypeById(Guid complaintTypeId)
        {
            ComplaintTypeModel type = complaintTypeRepository.GetComplaintTypeById(complaintTypeId);

            if (type == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintTypeDto>(type));
        }

        /// <summary>
        /// Kreira tip zalbe
        /// </summary>
        /// <returns>Kreiran tip zalbe</returns>
        /// <response code="200">Vraća listu tipova zalbe</response>
        /// <response code="500">Došlo je do greške na serveru prilikom unosa novog tipa zalbe</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintTypeConfirmationDto> CreateComplaintType([FromBody] ComplaintTypeCreationDto complaintType)
         {
            try
            {
                bool modelValid = ValidateComplaintType(complaintType);

                if (!modelValid)
                {
                    return BadRequest("Complaint type should not be empty");
                }

                ComplaintTypeModel type= mapper.Map<ComplaintTypeModel>(complaintType);
                ComplaintTypeConfirmation confirmation = complaintTypeRepository.CreateComplaintType(type);
                string location = linkGenerator.GetPathByAction("GetComplaintTypeById", "ComplaintType", new { complaintTypeId = type.ComplaintTypeId });
                return Created(location, mapper.Map<ComplaintTypeConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan Tip zalbe
        /// </summary>
        /// <returns>Potvrdu o modifikovanom tipu zalbe</returns>
        /// <response code="200">Vraća ažuriran tip zalbe</response>
        /// <response code="400">Tip zalbe koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa zalbe</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintTypeConfirmationDto> UpdateComplaintType(ComplaintTypeUpdateDto complaintType)
        {
            try
            {
                var oldComplaintType = complaintTypeRepository.GetComplaintTypeById(complaintType.ComplaintTypeId);

                if (oldComplaintType == null)
                {
                    return NotFound();
                }
                ComplaintTypeModel type = mapper.Map<ComplaintTypeModel>(complaintType);
                mapper.Map(type, oldComplaintType);
                complaintTypeRepository.SaveChanges();
                return Ok(mapper.Map<ComplaintTypeDto>(oldComplaintType));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog tipa zalbe na osnovu ID-a 
        /// </summary>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip zalbe uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip zalbe za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja tipa zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{complaintTypeId}")]
        public IActionResult DeleteComplaintType(Guid complaintTypeId)
        {
            try
            {
                ComplaintTypeModel type = complaintTypeRepository.GetComplaintTypeById(complaintTypeId);

                if (type == null)
                {
                    return NotFound();
                }
                complaintTypeRepository.DeleteComplaintType(complaintTypeId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa tipom zalbe
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetComplaintTypeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private bool ValidateComplaintType(ComplaintTypeCreationDto complaintTypeModel)
        {
            if (complaintTypeModel.ComplaintType == "")
            {
                return false;
            }
            return true;
        }
    }
}
