using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AuctionMicroservice.Services;
using AuctionMicroservice.Validatiors;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Controllers
{
    [ApiController]
    [Route("api/DocumentationIndividuals")]
    [Produces("application/json")] 
    [Consumes("application/json")]
    
    public class DocumentationIndividualController : ControllerBase
    {
        private readonly IDocumentationIndividualRepository documentationIndividualRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;
        private readonly DocumentationIndividualValidator validator;
        private readonly ILoggerService logger;

        public DocumentationIndividualController(IDocumentationIndividualRepository documentationIndividualRepository,LinkGenerator linkGenerator, IMapper mapper, DocumentationIndividualValidator validator, ILoggerService logger)
        {
            this.documentationIndividualRepository = documentationIndividualRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger = logger;
        }


        /// <summary>
        /// Returns list of all individual documentations
        /// </summary>
        /// <returns>List of individual documentations</returns>
        /// <response code="200">Returns list of individual documentations</response>
        /// <response code="404">No individual documentations found</response>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<DocumentationIndividualDto>>> GetDocumentationIndividualsAsync()
        {
            var documentations = await documentationIndividualRepository.GetDocumentationIndividualsAsync();

            if (documentations == null || documentations.Count == 0)
            {
                //await logger.LogMessage(LogLevel.Information, "No individual documentations found", "Auction microservice", "GetDocumentationsIndividualAsync");
                return NoContent();
            }

            //await logger.LogMessage(LogLevel.Information, "Getting all individual documentations", "Auction microservice", "GetDocumentationsIndividualAsync");

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
        public async Task<ActionResult<List<DocumentationIndividualDto>>> GetDocumentationIndividualsByAuctionAsync(Guid AuctionId)
        {
            var documentations = await documentationIndividualRepository.GetDocumentationIndividualsByAuctionAsync(AuctionId);

            if (documentations == null || documentations.Count == 0)
            {
                //await logger.LogMessage(LogLevel.Information, "Individual documentation not found", "Auction microservice", "GetDocumentationIndividualsByAuctionAsync");
                return NoContent();
            }

            
            //await logger.LogMessage(LogLevel.Information, "Getting all the individual documentations by auction ID", "Auction microservice", "GetDocumentationIndividualsByAuctionAsync");
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
        public async Task<ActionResult<DocumentationIndividualDto>> GetDocumentationByIdAsync(Guid DocumentationIndividualId)
        {
            var documentation = await documentationIndividualRepository.GetDocumentationByIdAsync(DocumentationIndividualId);

            if(documentation == null)
            {
                await logger.LogMessage(LogLevel.Information, "Individual documentation not found", "Auction microservice", "GetDocumentationIndividualsByIdAsync");
                return NotFound();
            }

            
            await logger.LogMessage(LogLevel.Information, "Getting individual documentation by ID", "Auction microservice", "GetDocumentationIndividualsByIdAsync");
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
        public async Task<ActionResult<DocumentationIndividualConfirmationDto>> CreateDocumentationIndividualAsync([FromBody] DocumentatonIndividualCreationDto documentation)
        {
            try
            {
                DocumentationIndividual documentationEntity = mapper.Map<DocumentationIndividual>(documentation);

                validator.ValidateAndThrow(documentationEntity);


                DocumentationIndividualConformation conformation = await documentationIndividualRepository.CreateDocumentationIndividualAsync(documentationEntity);
                await documentationIndividualRepository.SaveChangesAsync();

                string location = linkGenerator.GetPathByAction("GetDocumentationIndividuals", "DocumentationIndividual", new { DocumentationIndividualId = conformation.DocumentationIndividualId });

                //await logger.LogMessage(LogLevel.Information, "Documentation created", "Auction microservice", "CreateDocumentationIndividualAsync");
                return Created(location, mapper.Map<DocumentationIndividualConfirmationDto>(conformation));
            }
            catch (ValidationException v)
            {
                //await logger.LogMessage(LogLevel.Information, "Bad request", "Auction microservice", "CreateDocumentationIndividualAsync");
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                
                //await logger.LogMessage(LogLevel.Information, "There has been internal server error", "Auction microservice", "CreateDocumentationIndividualAsync");
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
        public async Task<ActionResult<DocumentationIndividualDto>> UpdateDocumentationIndividualAsync(DocumentationIndividualUpdateDto documentation)
        {
            try
            {
                var oldDocumentation = await documentationIndividualRepository.GetDocumentationByIdAsync(documentation.DocumentationIndividualId);

                if(oldDocumentation == null)
                {
                    //await logger.LogMessage(LogLevel.Warning, "Documentation object not found!", "Auction microservice", "UpdateDocumentationIndividualAsync");
                    return NotFound();
                }

                DocumentationIndividual documentationEntity = mapper.Map<DocumentationIndividual>(documentation);

                validator.ValidateAndThrow(documentationEntity);

                mapper.Map(documentationEntity, oldDocumentation);

                await documentationIndividualRepository.SaveChangesAsync();

               
                //await logger.LogMessage(LogLevel.Information, "Documentation has been updated", "Auction microservice", "UpdateDocumentationIndividualAsync");
                return Ok(mapper.Map<DocumentationIndividualDto>(oldDocumentation));
            }
            catch(Exception e )
            {
                
                await logger.LogMessage(LogLevel.Information, "There has been internal server error", "Auction microservice", "UpdateDocumentationIndividualAsync");
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
        public async Task<IActionResult> DeleteDocumentationIndividualAsync(Guid DocumentationIndividualId)
        {
            try
            {
                var documentation = await documentationIndividualRepository.GetDocumentationByIdAsync(DocumentationIndividualId);

                if (documentation == null)
                {
                    await logger.LogMessage(LogLevel.Information, "documentation not found", "Auction microservice", "DeleteDocumentationIndividualAsync");
                    return NotFound();
                }

                await documentationIndividualRepository.DeleteDocumentationAsync(DocumentationIndividualId);
                await documentationIndividualRepository.SaveChangesAsync();
                await logger.LogMessage(LogLevel.Information, "Documentation deleted", "Auction microservice", "DeleteDocumentationIndividualAsync");
                return NoContent();
            }
            catch (Exception e)
            {
                
                await logger.LogMessage(LogLevel.Information, "There has been internal server error", "Auction microservice", "DeleteDocumentationIndividualAsync");
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
            logger.LogMessage(LogLevel.Information, "Getting all the options", "Auction microservice", "GetDocumentationIndividualOptions");
            return Ok();
        }

    }
}
