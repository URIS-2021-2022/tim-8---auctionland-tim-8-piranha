using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AuctionMicroservice.Validatiors;
using AutoMapper;
using FluentValidation;
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
    [Route("api/DocumentationIndividuals")]
    [Produces("application/json", "application/xml")] 
    public class DocumentationIndividualController : ControllerBase
    {
        private readonly IDocumentationIndividualRepository documentationIndividualRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;
        private readonly DocumentationIndividualValidator validator;

        public DocumentationIndividualController(IDocumentationIndividualRepository documentationIndividualRepository,LinkGenerator linkGenerator, IMapper mapper, DocumentationIndividualValidator validator)
        {
            this.documentationIndividualRepository = documentationIndividualRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpGet]
        [HttpHead]     
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DocumentationIndividualDto>> GetDocumentationIndividuals()
        {
            var documentations = documentationIndividualRepository.GetDocumentationIndividuals();

            if (documentations == null || documentations.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<DocumentationIndividualDto>>(documentations));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("auction/{AuctionId}")]
        public ActionResult<List<DocumentationIndividualDto>> GetDocumentationIndividualsByAuction(Guid AuctionId)
        {
            var documentations = documentationIndividualRepository.GetDocumentationIndividualsByAuction(AuctionId);

            if (documentations == null || documentations.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<DocumentationIndividualDto>>(documentations));
        }



        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{DocumentationIndividualId}")]
        public ActionResult<DocumentationIndividualDto> GetDocumentationById(Guid DocumentationIndividualId)
        {
            var documentation = documentationIndividualRepository.GetDocumentationById(DocumentationIndividualId);

            if(documentation == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DocumentationIndividualDto>(documentation));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentationIndividualConfirmationDto> CreateDocumentationIndividual([FromBody] DocumentatonLegalEntitylCreationDto documentation)
        {
            try
            {
                DocumentationIndividual documentationEntity = mapper.Map<DocumentationIndividual>(documentation);

                validator.ValidateAndThrow(documentationEntity);

                DocumentationIndividualConformation conformation = documentationIndividualRepository.CreateDocumentationIndividual(documentationEntity);
                documentationIndividualRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetDocumentationIndividuals", "DocumentationIndividual", new { DocumentationIndividualId = conformation.DocumentationIndividualId });

                return Created(location, mapper.Map<DocumentationIndividualConfirmationDto>(conformation));
            }
            catch (ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }


        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentationIndividualDto> UpdateDocumentationIndividual(DocumentationIndividualUpdateDto documentation)
        {
            try
            {
                var oldDocumentation = documentationIndividualRepository.GetDocumentationById(documentation.DocumentationIndividualId);

                if(oldDocumentation == null)
                {
                    return NotFound();
                }

                DocumentationIndividual documentationEntity = mapper.Map<DocumentationIndividual>(documentation);

                mapper.Map(documentationEntity, oldDocumentation);

                documentationIndividualRepository.SaveChanges();

                return Ok(mapper.Map<DocumentationIndividualDto>(oldDocumentation));
            }
            catch(Exception e )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{DocumentationIndividualId}")]
        public IActionResult DeleteDocumentationIndividual(Guid DocumentationIndividualId)
        {
            try
            {
                var documentation = documentationIndividualRepository.GetDocumentationById(DocumentationIndividualId);

                if (documentation == null)
                {
                    return NotFound();
                }

                documentationIndividualRepository.DeleteDocumentation(DocumentationIndividualId);
                documentationIndividualRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
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
