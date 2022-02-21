using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
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
    [Route("api/DocumentStatus")]
    [Produces("application/json", "application/xml")]
    //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
    public class DocumentStatusController : ControllerBase
    {
        private readonly IDocumentStatusRepository DocumentStatusRepository;
        private readonly LinkGenerator LinkGeneration;
        private readonly IMapper Mapper;
        private readonly DocumentStatusValidators validator;

        public DocumentStatusController(IDocumentStatusRepository documentStatusRepository, LinkGenerator linkGeneration, IMapper mapper, DocumentStatusValidators validator)
        {
            DocumentStatusRepository = documentStatusRepository;
            LinkGeneration = linkGeneration;
            Mapper = mapper;
            this.validator = validator;
        }

        /// <summary>
        /// Vraca sve statuse dokumenta na osnovu prosledjenih filtera
        /// </summary>
        /// <param name="documentStatus">Status dokumenta</param>
        /// <returns>Lista svih statusa dokumenata</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task< ActionResult<List<DocumentStatusDto>>> GetDocumentStatusAsync(string documentStatus)
        {
            List<DocumentStatus> documentStatusList = await DocumentStatusRepository.GetDocumentStatusAsync(documentStatus);

            if (documentStatusList == null || documentStatusList.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map <List<DocumentStatusDto>>(documentStatusList));
        }

        /// <summary>
        /// Vraća traženi dokument po ID-ju
        /// </summary>
        /// <param name="DocumentStatusId">ID statusa dokumenta</param>
        /// <returns>Tražena banka</returns>
        /// <response code = "200">Vraća traženi status dokumenta</response>
        /// <response code = "404">Nije pronađen traženi status dokumenta</response>
        [HttpGet("{DocumentStatusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentStatusDto>> GetDocumentStatusByIdAsync(Guid DocumentStatusId)
        {
            DocumentStatus docStatus = await DocumentStatusRepository.GetDocumentStatusByIdAsync(DocumentStatusId);

            if (docStatus == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<DocumentStatusDto>(docStatus));
        }

        /// <summary>
        /// Kreira novi status dokumenta
        /// </summary>
        /// <param name="documentStatus"> model statusa dokumenta</param>
        /// <returns>Potvrda o kreiranom statusu dokumenta</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog statusa dokumenta \
        /// POST /api/DocumentStatus \
        /// { \
        ///  "DocStatusID" : "93a08cc2-1d17-46e6-bd95-4fa70bb11226", \
        ///  "Status" : "Usvojen", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreirani status dokumenta</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja statusa dokumenta</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentStatusConfirmationDto>> CreateDocumentStatusAsync([FromBody] DocumentStatusCreationDto documentStatus)
        {
            try
            {
                DocumentStatus docStatus = Mapper.Map<DocumentStatus>(documentStatus);

                validator.ValidateAndThrow(docStatus);

                DocumentStatusConfirmation confirmation = await DocumentStatusRepository.CreateDocumentStatusAsync(docStatus);
                await DocumentStatusRepository.SaveChangesAsync();

                string uri = LinkGeneration.GetPathByAction("GetDocumentStatusById", "DocumentStatus", new { DocumentStatusId = confirmation.docStatusID });

               


               //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
                return Created(uri, Mapper.Map<DocumentStatusConfirmationDto>(confirmation));
            }
            catch (Exception e )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan status dokumenta
        /// </summary>
        /// <param name="documentStatus">Model statusa dokumenta koji se ažurira</param>
        /// <returns>Potvrda o ažuriranom statusu dokumenta</returns>
        /// <response code="200">Vraća ažurirani status dokumenta</response>
        /// <response code="404">Nije pronađen status dokumenta za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja statusa dokumenta</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentStatusDto>> UpdateDocumentStatusAsync(DocumentStatusUpdateDto documentStatus)
        {
            try
            {
                DocumentStatus existingDocumentStatus = await DocumentStatusRepository.GetDocumentStatusByIdAsync(documentStatus.DocStatusID);

                if (existingDocumentStatus == null)
                {
                    return NotFound();
                }

                DocumentStatus docStatus = Mapper.Map<DocumentStatus>(documentStatus);

                validator.ValidateAndThrow(docStatus);

                Mapper.Map(docStatus, existingDocumentStatus);
                await DocumentStatusRepository .SaveChangesAsync();

                return Ok(Mapper.Map<DocumentStatusDto>(existingDocumentStatus));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating document status object");
            }
        }

        /// <summary>
        /// Briše status dokumenta na osnovu ID-ja
        /// </summary>
        /// <param name="documentStatusId">ID statusa dokumenta</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Status dokumenta uspešno obrisan</response>
        /// <response code="404">Nije pronađen status dokumenta za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja statusa dokumenta</response>
        [HttpDelete("{documentStatusId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDocumentStatusAsync(Guid documentStatusId)
        {
            try
            {
                DocumentStatus docStatus = await DocumentStatusRepository.GetDocumentStatusByIdAsync(documentStatusId);
                if (docStatus == null)
                {
                    return NotFound();
                }

                await DocumentStatusRepository.DeleteDocumentStatusAsync(documentStatusId);
                await DocumentStatusRepository.SaveChangesAsync();
                return NoContent(); // Successful deletion -- sve je okej proslo ali ne vraca nikakav sadrzaj--> iz familije je 200

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting document status object!");
            }
        }
        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve tipove stutusa dokumenta
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetDocumentStatusOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
