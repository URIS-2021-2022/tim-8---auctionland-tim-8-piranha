using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using DocumentMicroservice.Models.Document;
using DocumentMicroservice.ServiceCalls;
using DocumentMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Controllers
{
    [ApiController]
    [Route("api/Document")]
    [Produces("application/json", "application/xml")]

    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly DocumentValidators validator;
        private readonly ILoggerService logger;
        private readonly IServiceCall<AuctionDto> auctionService;
        private readonly IServiceCall<UserDto> userService;
        private readonly IConfiguration configuration;



        public DocumentController(IDocumentRepository documentRepository, LinkGenerator linkGenerator, IMapper mapper, DocumentValidators validator, ILoggerService logger, IServiceCall<AuctionDto> auctionService, IServiceCall<UserDto> userService, IConfiguration configuration)
        {
            this.documentRepository = documentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger = logger;
            this.auctionService = auctionService;
            this.userService = userService;
            this.configuration = configuration;
            
        }

        /// <summary>
        /// Vraća sve dokumente
        /// </summary>
        /// <returns>Lista dokumenata</returns>
        /// <response code = "200">Vraća listu dokumenata</response>
        /// <response code = "204">Ne postoji ni jedan dokument</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<DocumentDto>>> GetDocumentAsync(string document)
        {
            List<Document> documentList = await documentRepository.GetDocumentAsync(document);

            if (documentList == null || documentList.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Document list is empty!", "Document microservice", "GetDocumentAsync");
                return NoContent();
            }

            List<DocumentDto> documentsDto = new List<DocumentDto>();

            foreach (var doc in documentList)
            {
                DocumentDto documentDto = mapper.Map<DocumentDto>(doc);

                if (doc.auctionId is not null && doc.userId is not null)
                {
                    var auctionDto = await auctionService.SendGetRequestAsync(configuration["Services:AuctionServiceCallMock"]);
                    var userDto = await userService.SendGetRequestAsync(configuration["Services:UserServiceCallMock"]);

                    if (auctionDto is not null && userDto is not null)
                    {
                        documentDto.auction = auctionDto;
                        documentDto.user = userDto;
                    }
                }
                documentsDto.Add(documentDto);
            }

            await logger.LogMessage(LogLevel.Information, "Document list successfully returned!", "Document microservice", "GetDocumentsAsync");
            return Ok(documentsDto);

        }

        /// <summary>
        /// Vraća traženi dokument po ID-ju
        /// </summary>
        /// <param name="DocumentId">ID dokumenta</param>
        /// <returns>Tražena banka</returns>
        /// <response code = "200">Vraća traženi dokuement</response>
        /// <response code = "404">Nije pronađen traženi dokument</response>
        [HttpGet("{DocumentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentDto>> GetDocumentByIdAsync(Guid DocumentId)
        {
            Document doc = await documentRepository.GetDocumentByIdAsync(DocumentId);

            if (doc == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Document not found!", "Document microservice", "GetDocumentByIdAsync");
                return NotFound();
            }

            DocumentDto documentDto = mapper.Map<DocumentDto>(doc);

            if (doc.auctionId is not null && doc.userId is not null )
            {
                var auctionDto = await auctionService.SendGetRequestAsync(configuration["Services:AuctionServiceCallMock"]);
                var userDto = await userService.SendGetRequestAsync(configuration["Services:UserServiceCallMock"]);

                if (auctionDto is not null && userDto is not null)
                {
                    documentDto.auction = auctionDto;
                    documentDto.user = userDto;
                }
            }

            await logger.LogMessage(LogLevel.Information, "Document found and successfully returned!", "Document microservice", "GetDocumentByIdAsync");
            return Ok(documentDto);

         
        }

        /// <summary>
        /// Kreira novi dokument
        /// </summary>
        /// <param name="document"> model dokumenta</param>
        /// <returns>Potvrda o kreiranom dokumentu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dokumenta \
        /// POST /api/Document \
        /// { \
        ///  "RegistrationNumber" : "119833332", \
        ///  "DocumentCreationDate" : "11-02-2020,08:00:00", \
        ///  "DocumentDate" : "11-02-2020,08:00:00", \
        ///  "DocumentTemplate" : "Kreiranje predloga plana", \
        ///  "DocStatusID" : Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226") \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreirani dokument</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja dokumenta</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentConfirmationDto>> CreateDocumentAsync([FromBody] DocumentCreationDto document)
        {
            try
            {
                Document doc = mapper.Map<Document>(document);

                validator.ValidateAndThrow(doc);


                DocumentConfirmation confirmation = await documentRepository.CreateDocumentAsync(doc);
                await documentRepository.SaveChangesAsync();

                
               string uri = linkGenerator.GetPathByAction("GetDocumentById", "Document", new { documentId = confirmation.documentId });
                //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
                await logger.LogMessage(LogLevel.Information, "Document  protected zone successfully created!", "Plot microservice", "CreateDocumentAsync");

                return Created(uri, mapper.Map<DocumentConfirmationDto>(confirmation));
            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for document object failed!", "Document microservice", "CreateDocumentAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch ( Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Document object creation failed!", "Document microservice", "CreateDocumentAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan dokument
        /// </summary>
        /// <param name="document">Model dokuementa koji se ažurira</param>
        /// <returns>Potvrda o ažuriranom dokumentu</returns>
        /// <response code="200">Vraća ažurirani dokument</response>
        /// <response code="404">Nije pronađen dokument za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dokumenta</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentDto>> UpdateDocumentAsync(DocumentUpdateDto document)
        {
            try
            {
                Document existingDocument = await documentRepository.GetDocumentByIdAsync(document.DocumentId);

                if (existingDocument == null)
                {
                   await logger.LogMessage(LogLevel.Warning, "Document object not found!", "Document microservice", "UpdateDocumentAsync");
                    return NotFound();
                }

                Document doc = mapper.Map<Document>(document);

                validator.ValidateAndThrow(doc);

                mapper.Map(doc, existingDocument);
                await documentRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Document object updated successfully!", "Document microservice", "UpdateDocumentAsync");
                return Ok(mapper.Map<DocumentDto>(existingDocument));

            }catch(ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for document object failed!", "Document microservice", "UpdateDocumentAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Document object updating failed!", "Document microservice", "UpdateDocumentAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Briše dokument na osnovu ID-ja
        /// </summary>
        /// <param name="documentId">ID dokument</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Dokument uspešno obrisan</response>
        /// <response code="404">Nije pronađen dokument za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dokumenta</response>
        [HttpDelete("{documentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDocumentAsync(Guid documentId)
        {
            try
            {
                Document doc = await documentRepository.GetDocumentByIdAsync(documentId);
                if (doc == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Document object not found!", "Document microservice", "DeleteDocumentAsync");
                    return NotFound();
                }

                await documentRepository.DeleteDocumentAsync(documentId);
                 await documentRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Document object deleted successfully!", "Document microservice", "DeleteDocumentAsync");
                return NoContent(); // Successful deletion -- sve je okej proslo ali ne vraca nikakav sadrzaj--> iz familije je 200

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Document object deletion failed!", "Document microservice", "DeleteDocumentAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve tipove dokumenta
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetDocumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Document microservice", "GetDocumentOptions");

            return Ok();
        }


    }
}
