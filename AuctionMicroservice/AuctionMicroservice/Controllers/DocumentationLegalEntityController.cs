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

        /// <summary>
        /// Returns list of all legal entity documentations
        /// </summary>
        /// <returns>List of legal entity documentations</returns>
        /// <response code="200">Returns list of legal entity documentations</response>
        /// <response code="404">No legal entity documentations found</response>
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



        /// <summary>
        /// Returns legal enitity documentation by ID
        /// </summary>
        /// <param name="DocumentationLegalEntityId"></param>
        /// <returns>legal enitity documentation by ID</returns>
        ///<response code="200">returns found legal enitity documentation</response>
        ///<response code="404">no legal enitity documentation found</response>
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

        /// <summary>
        /// Returns legal entity documentations that belong to one auction by AuctionId
        /// </summary>
        /// <param name="AuctionId"></param>
        /// <returns>legal entity documentations by AuctionId</returns>
        ///<response code="200">returns found legal entity documentations</response>
        ///<response code="404">no legal entity documentations found</response>
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


        /// <summary>
        /// Creates new legal entity documentation
        /// </summary>
        /// <param name="documentation">legal entity documentation model</param>
        /// <returns>Creation confirmation</returns>
        /// <remarks>
        /// Example of legal entity documentation creation model \
        /// POST /api/DocumentationLegalEntities \
        /// {   \
        /// "name": "Test", \
        /// "identificationNumber": "21412946612", \
        /// "address": "Bulevar 2", \
        /// "auctionId": "6a421c13-a195-48f7-8dbd-67596c3974c0" \
        /// } \
        /// </remarks>
        /// <response code="200">returns created documentation</response>
        /// <response code="500">There's been server error</response>
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


        /// <summary>
        /// Updates one legal entity documentation 
        /// </summary>
        /// <param name="documentation">legal entity documentation model that is updated</param>
        /// <returns>Update confirmation model</returns>
        /// <response code="200">Returns updated legal entity documentation</response>
        /// <response code="400">legal entity documentation not found</response>
        /// <response code="500">There has been internal server error</response>
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


        /// <summary>
        /// Deletes one legal entity documentation by ID
        /// </summary>
        /// <param name="DocumentationLegalEntityId"></param>
        /// <returns>Status 204</returns>
        /// <response code="204">legal entity documentation successesfuly deleted</response>
        /// <response code="404">legal entity documentation not found</response>
        /// <response code="500">There has been internal server error</response>
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

        /// <summary>
        /// returns possible options to work with documentations
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetDocumentationLegalEntityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
