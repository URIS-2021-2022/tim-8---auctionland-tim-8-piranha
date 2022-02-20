using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.Document;
using DocumentMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly IDocumentRepository DocumentRepository;
        private readonly LinkGenerator LinkGeneration;
        private readonly IMapper Mapper;
        private readonly DocumentValidators validator;

        public DocumentController(IDocumentRepository documentRepository, LinkGenerator linkGeneration, IMapper mapper, DocumentValidators validator)
        {
            DocumentRepository = documentRepository;
            LinkGeneration = linkGeneration;
            Mapper = mapper;
            this.validator = validator;
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
            List<Document> documentList = await DocumentRepository.GetDocumentAsync(document);

            if (documentList == null || documentList.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<DocumentDto>>(documentList));
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
            Document doc = await DocumentRepository.GetDocumentByIdAsync(DocumentId);

            if (doc == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<DocumentDto>(doc));
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
                Document doc = Mapper.Map<Document>(document);

                validator.ValidateAndThrow(doc);


                DocumentConfirmation confirmation = await DocumentRepository.CreateDocumentAsync(doc);
                await DocumentRepository.SaveChangesAsync();

                
               string uri = LinkGeneration.GetPathByAction("GetDocumentById", "Document", new { documentId = confirmation.DocumentId });
                //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
                return Created(uri, Mapper.Map<DocumentConfirmationDto>(confirmation));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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
                Document existingDocument = await DocumentRepository.GetDocumentByIdAsync(document.DocumentId);

                if (existingDocument == null)
                {
                    return NotFound();
                }

                Document doc = Mapper.Map<Document>(document);

                validator.ValidateAndThrow(doc);

                Mapper.Map(doc, existingDocument);
                await DocumentRepository.SaveChangesAsync();

                return Ok(Mapper.Map<DocumentDto>(existingDocument));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating document object");
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
                Document doc = await DocumentRepository.GetDocumentByIdAsync(documentId);
                if (doc == null)
                {
                    return NotFound();
                }

                await DocumentRepository.DeleteDocumentAsync(documentId);
                 await DocumentRepository.SaveChangesAsync();
                return NoContent(); // Successful deletion -- sve je okej proslo ali ne vraca nikakav sadrzaj--> iz familije je 200

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve tipove dokumenta
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetDocumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
