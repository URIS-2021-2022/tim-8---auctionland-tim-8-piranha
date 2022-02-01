using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Controllers
{
    [ApiController]
    [Route("api/documentationIndividual")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class DocumentationIndividualController : ControllerBase
    {
        private readonly IDocumentationIndividualRepository documentationIndividualRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public DocumentationIndividualController(IDocumentationIndividualRepository documentationIndividualRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.documentationIndividualRepository = documentationIndividualRepository;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<DocumentationIndividualDto>> GetDocumentationIndividuals(string FirstName, string Surname, string IdentificationNumber)
        {
            var documentations = documentationIndividualRepository.GetDocumentationIndividuals(FirstName, Surname, IdentificationNumber);

            if (documentations == null || documentations.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<DocumentationIndividualDto>>(documentations));

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpGet("{DocumentationIndividualId}")]

        public ActionResult<DocumentationIndividualDto> GetDocumentationIndividualById(Guid DocumentationIndividualId)
        {
            var documentation = documentationIndividualRepository.GetDocumentationIndividualById(DocumentationIndividualId);

            if(documentation == null)
            {
                
                return NotFound();
            }

            return Ok(mapper.Map<DocumentationIndividualDto> (documentation));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<DocumentationIndividualConfirmationDto> CreateDocumentationIndividual([FromBody] DocumentationIndividualDto documentationIndividualDto)
        {
            try
            {
                DocumentationIndividual documentationIndividualEntity = mapper.Map<DocumentationIndividual>(documentationIndividualDto);
                DocumentationIndividualConformation confirmation = documentationIndividualRepository.CreateDocumentationIndividual(documentationIndividualEntity);
                documentationIndividualRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetDocumentationIndividualById", "DocumentationIndividual", new { DocumentationIndividualId = confirmation.DocumentationIndividualId });

                return Created(location, mapper.Map<DocumentationIndividualConfirmationDto>(confirmation));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<DocumentationIndividualDto> UpdateDocumentationIndividual(DocumentationIndividualUpdateDto documentation)
        {
            try
            {
                var oldDocumentation = documentationIndividualRepository.GetDocumentationIndividualById(documentation.DocumentationIndividualId);

                if (oldDocumentation == null)
                {
                    return NotFound();

                }
                DocumentationIndividual documentationIndividualEntity = mapper.Map<DocumentationIndividual>(documentation);

                mapper.Map(documentationIndividualEntity, oldDocumentation);

                documentationIndividualRepository.SaveChanges();
                return Ok(mapper.Map<DocumentationIndividualDto>(oldDocumentation));


            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error" + e.Message);
            }


            
                

                
            
        }


        [HttpDelete("{DocumentationIndividualId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteDocumentationIndividual(Guid DocumentationIndividualId)
        {
            try
            {
                var documentation = documentationIndividualRepository.GetDocumentationIndividualById(DocumentationIndividualId);

                if(documentation == null)
                {
                    return NotFound();

                }

                documentationIndividualRepository.DeleteDocumentationIndividual(DocumentationIndividualId);
                documentationIndividualRepository.SaveChanges();
                return NoContent();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpOptions]
        [AllowAnonymous]

        public IActionResult GetDocumentationIndividualOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();        
        }


    }
}
