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
    [Consumes("application/json")]
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


        /// <summary>
        /// Returns list of all individual documentations
        /// </summary>
        /// <returns>List of individual documentations</returns>
        /// <response code="200">Returns list of individual documentations</response>
        /// <response code="404">No individual documentations found</response>
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

        /// <summary>
        /// Returns individual documentations that belong to one auction by AuctionId
        /// </summary>
        /// <param name="AuctionId"></param>
        /// <returns>individual documentations by AuctionId</returns>
        ///<response code="200">returns found individual documentations</response>
        ///<response code="404">no individual documentations found</response>
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



        /// <summary>
        /// Returns individual documentations by ID
        /// </summary>
        /// <param name="DocumentationIndividualId"></param>
        /// <returns>individual documentations by ID</returns>
        ///<response code="200">returns found individual documentation</response>
        ///<response code="404">no individual documentation found</response>
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


        /// <summary>
        /// Creates new individual documentation
        /// </summary>
        /// <param name="documentation">individual documentation model</param>
        /// <returns>Creation confirmation</returns>
        /// <remarks>
        /// Example of individual documentation creation model \
        /// POST /api/DocumentationIndividuals \
       /// {   \
       /// "firstName": "Luka", \
       /// "surname": "Panic", \
       /// "identificationNumber": "21412946612", \
       /// "auctionId": "6a421c13-a195-48f7-8dbd-67596c3974c0" \
       /// } \
     /// </remarks>
    /// <response code="200">returns created documentation</response>
    /// <response code="500">There's been server error</response>
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


        /// <summary>
        /// Updates one individual documentation 
        /// </summary>
        /// <param name="documentation">Individual documentation model that is updated</param>
        /// <returns>Update confirmation model</returns>
        /// <response code="200">Returns updated individual documentation</response>
        /// <response code="400">Individual documentation not found</response>
        /// <response code="500">There has been internal server error</response>
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


        /// <summary>
        /// Deletes one individual documentation by ID
        /// </summary>
        /// <param name="DocumentationIndividualId"></param>
        /// <returns>Status 204</returns>
        /// <response code="204">Individual documentation successesfuly deleted</response>
        /// <response code="404">Individual documentation not found</response>
        /// <response code="500">There has been internal server error</response>
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


        /// <summary>
        /// returns possible options to work with documentations
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetDocumentationIndividualOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
