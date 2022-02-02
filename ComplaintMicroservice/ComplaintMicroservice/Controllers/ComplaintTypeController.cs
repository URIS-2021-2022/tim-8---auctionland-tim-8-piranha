using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
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

        [HttpGet]
        public ActionResult<List<ComplaintTypeDto>> GetComplaintTypes(string complaintType)
        {
            List<ComplaintTypeModel> types = complaintTypeRepository.GetComplaintTypes(complaintType);
            if(types==null || types.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ComplaintTypeDto>>(types));
        }

        [HttpGet("{complaintTypeId}")]
        public ActionResult<ComplaintTypeDto> GetComplaintTypeById(Guid complaintTypeId)
        {
            ComplaintTypeModel type = complaintTypeRepository.GetComplaintTypeById(complaintTypeId);

            if (type == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<ComplaintTypeDto>>(type));
        }

        [HttpPost]
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

        [HttpPut]
        public ActionResult<ComplaintTypeConfirmationDto> UpdateComplaintType(ComplaintTypeUpdateDto complaintType)
        {
            try
            {

                if (complaintTypeRepository.GetComplaintTypeById(complaintType.ComplaintTypeId) == null)
                {
                    return NotFound();
                }
                ComplaintTypeModel type = mapper.Map<ComplaintTypeModel>(complaintType);
                ComplaintTypeConfirmation confirmaion = complaintTypeRepository.UpdateComplaintType(type);
                return Ok(mapper.Map<ComplaintTypeConfirmationDto>(confirmaion));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

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

        

        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
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
