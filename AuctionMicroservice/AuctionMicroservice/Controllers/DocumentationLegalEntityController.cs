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
    [Route("api/DocumentationLegalEntities")]
    [Produces("application/json", "application/xml")]
    public class DocumentationLegalEntityController : ControllerBase
    {
        private readonly IDocumentationLegalEntityRepository documentationLegalEntityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly DocumentationLegalEntityValidator validator;

        public DocumentationLegalEntityController(IDocumentationLegalEntityRepository documentationLegalEntityRepository, LinkGenerator linkGenerator, IMapper mapper, DocumentationLegalEntityValidator validator)
        {
            this.documentationLegalEntityRepository = documentationLegalEntityRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DocumentationLegalEntityDto>> GetDocumentationLegalEntites()
        {
            var documentations = documentationLegalEntityRepository.GetDocumentationLegalEntities();

            if (documentations == null || documentations.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<DocumentationLegalEntityDto>>(documentations));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{DocumentationLegalEntityId}")]
        public ActionResult<DocumentationLegalEntityDto> GetDocumentationById(Guid DocumentationLegalEntityId)
        {
            var documentation = documentationLegalEntityRepository.GetDocumentationById(DocumentationLegalEntityId);

            if (documentation == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DocumentationLegalEntityDto>(documentation));
        }

        //documentations by auction
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("auction/{AuctionId}")]
        public ActionResult<List<DocumentationLegalEntityDto>> GetDocumentationLegalEntitesByAuction(Guid AuctionId)
        {
            var documentations = documentationLegalEntityRepository.GetDocumentationLegalEntitesByAuction(AuctionId);

            if (documentations == null || documentations.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<DocumentationLegalEntityDto>>(documentations));
        }



        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentationLegalEntityConfirmationDto> CreateDocumentationLegalEntity([FromBody] DocumentationLegalEntityCreationDto documentation)
        {
            try
            {
                DocumentationLegalEntity documentationEntity = mapper.Map<DocumentationLegalEntity>(documentation);

                validator.ValidateAndThrow(documentationEntity);

                DocumentationLegalEntityConfirmation conformation = documentationLegalEntityRepository.CreateDocumentationLegalEntity(documentationEntity);
                documentationLegalEntityRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetDocumentationLegalEntites", "DocumentationLegalEntity", new { DocumentationLegalEntityId = conformation.DocumentationLegalEntityId });

                return Created(location, mapper.Map<DocumentationLegalEntityConfirmationDto>(conformation));
            }
            catch(ValidationException v)
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
        public ActionResult<DocumentationLegalEntityDto> UpdateDocumentationLegalEntity(DocumentationLegalEntityUpdateDto documentation)
        {
            try
            {
                var oldDocumentation = documentationLegalEntityRepository.GetDocumentationById(documentation.DocumentationLegalEntityId);

                if (oldDocumentation == null)
                {
                    return NotFound();
                }

                DocumentationLegalEntity documentationEntity = mapper.Map<DocumentationLegalEntity>(documentation);

                mapper.Map(documentationEntity, oldDocumentation);

                documentationLegalEntityRepository.SaveChanges();

                return Ok(mapper.Map<DocumentationLegalEntityDto>(oldDocumentation));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{DocumentationLegalEntityId}")]
        public IActionResult DeleteDocumentationLegalEntity(Guid DocumentationLegalEntityId)
        {
            try
            {
                var documentation = documentationLegalEntityRepository.GetDocumentationById(DocumentationLegalEntityId);

                if (documentation == null)
                {
                    return NotFound();
                }

                documentationLegalEntityRepository.DeleteDocumentation(DocumentationLegalEntityId);
                documentationLegalEntityRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetDocumentationLegalEntityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
