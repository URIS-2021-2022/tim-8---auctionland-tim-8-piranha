using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Data.Repositories;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.ContractLease;
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
    [Route("api/ContractLease")]
    [Produces("application/json", "application/xml")]
    public class ContractLeaseController : ControllerBase
    {
        private readonly IContractLeaseRepository contractLeaseRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ContractLeaseValidators validator;

        public ContractLeaseController(IContractLeaseRepository contractLeaseRepository, LinkGenerator linkGeneration, IMapper mapper, ContractLeaseValidators validator)
        {
            this.contractLeaseRepository = contractLeaseRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
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
        public async Task<ActionResult<List<ContractLeaseDto>>> GetContractLeaseAsync(string serialNumber)
        {
            List<ContractLease> contractLeaseList = await contractLeaseRepository.GetContractLeaseAsync(serialNumber);

            if (contractLeaseList == null || contractLeaseList.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ContractLeaseDto>>(contractLeaseList));
        }

        /// <summary>
        /// Vraća traženi dokument po ID-ju
        /// </summary>
        /// <param name="DocumentId">ID dokumenta</param>
        /// <returns>Tražena banka</returns>
        /// <response code = "200">Vraća traženi dokuement</response>
        /// <response code = "404">Nije pronađen traženi dokument</response>
        [HttpGet("{contractLeaseID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContractLeaseDto>> GetContractLeaseByIdAsync(Guid contractLeaseID)
        {
            ContractLease contractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);

            if (contractLease == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ContractLeaseDto>(contractLease));
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
        public async Task<ActionResult<ContractLeaseConfirmationDto>> CreateContractLeaseAsync([FromBody] ContractLeaseCreationDto contractLease)
        {
            try
            {
                ContractLease cLease = mapper.Map<ContractLease>(contractLease);

                validator.ValidateAndThrow(cLease);


                ContractLeaseConfirmation confirmation = await contractLeaseRepository.CreateContractLeaseAsync(cLease);
                await contractLeaseRepository.SaveChangesAsync();


               // string uri = linkGenerator.GetPathByAction("GetContractLeaseById", "ContractLease", new { contractLeaseID = confirmation.contractLeaseID });
                //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
                return Created("", mapper.Map<ContractLeaseConfirmationDto>(confirmation));
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
        public async Task<ActionResult<ContractLeaseDto>> UpdateContractLeaseAsync(ContractLeaseUpdateDto contractLease)
        {
            try
            {
                ContractLease existingContractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLease.ContractLeaseID);

                if (existingContractLease == null)
                {
                    return NotFound();
                }

                ContractLease cl = mapper.Map<ContractLease>(contractLease);

                validator.ValidateAndThrow(cl);

                mapper.Map(cl, existingContractLease);
                await contractLeaseRepository.SaveChangesAsync();

                return Ok(mapper.Map<ContractLeaseDto>(existingContractLease));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating Contract lease object");
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
        [HttpDelete("{contractLeaseID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContractLeaseAsync(Guid contractLeaseID)
        {
            try
            {
                ContractLease cl = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);
                if (cl == null)
                {
                    return NotFound();
                }

                await contractLeaseRepository.DeleteContractLeaseAsync(contractLeaseID);
                await contractLeaseRepository.SaveChangesAsync();
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
