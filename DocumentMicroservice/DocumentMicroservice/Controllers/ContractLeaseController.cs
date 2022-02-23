using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using DocumentMicroservice.Models.ContractLease;
using DocumentMicroservice.ServiceCalls;
using DocumentMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Controllers
{
    [ApiController]
    [Route("api/contractLease")]
    [Produces("application/json", "application/xml")]
    public class ContractLeaseController : ControllerBase
    {
        private readonly IContractLeaseRepository contractLeaseRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ContractLeaseValidators validator;
        private readonly ILoggerService logger;
        private readonly IServiceCall<BuyerDto> buyerService;
        private readonly IServiceCall<PersonDto> personService;
        private readonly IServiceCall<PlotDto> plotService;

        public ContractLeaseController(IContractLeaseRepository contractLeaseRepository, LinkGenerator linkGenerator, IMapper mapper, ContractLeaseValidators validator, ILoggerService logger, IServiceCall<BuyerDto> buyerService, IServiceCall<PersonDto> personService, IServiceCall<PlotDto> plotService)
        {
            this.contractLeaseRepository = contractLeaseRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger = logger;
            this.buyerService = buyerService;
            this.personService = personService;
            this.plotService = plotService;
        }

        /// <summary>
        /// Vraća sve tipove garancija
        /// </summary>
        /// <returns>Lista tipova garancija</returns>
        /// <response code = "200">Vraća listu tipova garancija</response>
        /// <response code = "204">Ne postoji nijedan tip garancije</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ContractLeaseDto>>> GetContractLeaseAsync(string serialNumber)
        {
            List<ContractLease> contractLeaseList = await contractLeaseRepository.GetContractLeaseAsync(serialNumber);

            if (contractLeaseList == null || contractLeaseList.Count == 0)
            {
<<<<<<< Updated upstream
                await logger.LogMessage(LogLevel.Warning, "ContractLease list is empty!", "Document microservice", "GetContractLeaseAsync");
                return NoContent();
            }

            List<ContractLeaseDto> contractLeasesDto = new List<ContractLeaseDto>();

            foreach (var cl in contractLeaseList)
            {
                ContractLeaseDto contractLeasestDto = mapper.Map<ContractLeaseDto>(cl);

                if (cl.buyerId is not null && cl.personId is not null && cl.plotId is not null)
                {
                    var buyerDto = await buyerService.SendGetRequestAsync("http://localhost:40004/buyer");
                    var personDto = await personService.SendGetRequestAsync("http://localhost:40008/person");
                    var plotDto = await plotService.SendGetRequestAsync("http://localhost:40009/plot");

                    if (buyerDto is not null && personDto is not null && plotDto is not null)
                    {
                        contractLeasestDto.buyer = buyerDto;
                        contractLeasestDto.person = personDto;
                        contractLeasestDto.plot = plotDto;
                    }
                }
                contractLeasesDto.Add(contractLeasestDto);
            }

            await logger.LogMessage(LogLevel.Information, "Contract lease list successfully returned!", "Document microservice", "GetContractLeaseAsync");
            return Ok(contractLeasesDto);
        }

        /// <summary>
        /// Vraća traženi tip garancije po ID-ju
        /// </summary>
        /// <param name="GuaranteeTypeId">ID tipa garancije</param>
        /// <returns>Traženi tip garacnije</returns>
        /// <response code = "200">Vraća traženi tip garancije</response>
        /// <response code = "404">Nije pronađen traženi tip garancije</response>
=======
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
>>>>>>> Stashed changes
        [HttpGet("{contractLeaseID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContractLeaseDto>> GetContractLeaseByIdAsync(Guid contractLeaseID)
        {
            ContractLease contractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);

            if (contractLease == null)
            {
<<<<<<< Updated upstream
                await logger.LogMessage(LogLevel.Warning, "Contract lease not found!", "Document  microservice", "GetContractLeaseByIdAsync");
                return NotFound();
            }
            ContractLeaseDto contractLeaseDto = mapper.Map<ContractLeaseDto>(contractLease);

            if (contractLease.buyerId is not null && contractLease.personId is not null && contractLease.plotId is not null)
            {
                var buyerDto = await buyerService.SendGetRequestAsync("http://localhost:40004/buyer");
                var personDto = await personService.SendGetRequestAsync("http://localhost:40008/person");
                var plotDto = await plotService.SendGetRequestAsync("http://localhost:40009/plot");

                if (buyerDto is not null && personDto is not null && plotDto is not null)
                {
                    contractLeaseDto.buyer = buyerDto;
                    contractLeaseDto.person = personDto;
                    contractLeaseDto.plot = plotDto;
                }
            }

            //await logger.LogMessage(LogLevel.Information, "Contract lease found and successfully returned!", "Document microservice", "GetContractLeaseByIdAsync");
            return Ok(contractLeaseDto);
        }


        /// <summary>
        /// Kreira novi tip garancije
        /// </summary>
        /// <param name="guaranteeType"> model tipa garancije </param>
        /// <returns>Potvrda o kreiranom tipu garancije</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa garancije \
        /// POST /api/guaranteeType \
        /// { \
        ///  "GuaranteeTypeID" : "f5f92ac7-0682-48a6-bd34-f2f5d89be9a0", \
        ///  "Type" : "Jemstvo", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreirani tip garancije</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja tipa garancije</response>
=======
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
>>>>>>> Stashed changes
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
<<<<<<< Updated upstream
        public async Task<ActionResult<ContractLeaseConfirmationDto>> CreateContractLeaseAsync([FromBody] ContractLeaseCreationDto ContractLeaseCreation)
        {
            try
            {
                ContractLease contractLease = mapper.Map<ContractLease>(ContractLeaseCreation);

                validator.ValidateAndThrow(contractLease);

                ContractLeaseConfirmation confirmation = await contractLeaseRepository.CreateContractLeaseAsync(contractLease);
                await contractLeaseRepository.SaveChangesAsync();


                string uri = linkGenerator.GetPathByAction("GetContractLeaseById", "ContractLease", new { contractLeaseID = confirmation.contractLeaseID });
                await logger.LogMessage(LogLevel.Information, "Contract Lease  protected zone successfully created!", "Document microservice", "CreateContractLeaseAsync");

                return Created(uri, mapper.Map<ContractLeaseConfirmationDto>(confirmation));
            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for contract lease object failed!", "Document microservice", "CreateContractLeaseAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contract lease object creation failed!", "Document microservice", "CreateContractLeaseAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
=======
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
>>>>>>> Stashed changes
            }
        }

        /// <summary>
<<<<<<< Updated upstream
        /// Ažurira jedan tip garancije
        /// </summary>
        /// <param name="guaranteeType">Model tipa garancije koji se ažurira</param>
        /// <returns>Potvrda o ažuriranom tipu garancije</returns>
        /// <response code="200">Vraća ažurirani tip garancije</response>
        /// <response code="404">Nije pronađen tip garancije za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa garancije</response>
=======
        /// Ažurira jedan dokument
        /// </summary>
        /// <param name="document">Model dokuementa koji se ažurira</param>
        /// <returns>Potvrda o ažuriranom dokumentu</returns>
        /// <response code="200">Vraća ažurirani dokument</response>
        /// <response code="404">Nije pronađen dokument za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dokumenta</response>
>>>>>>> Stashed changes
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContractLeaseDto>> UpdateContractLeaseAsync(ContractLeaseUpdateDto contractLease)
        {
            try
            {
<<<<<<< Updated upstream
                ContractLease existingContractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLease.contractLeaseID);

                if (existingContractLease == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contract Lease object not found!", "Document microservice", "UpdateContractLeaseAsync");
=======
                ContractLease existingContractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLease.ContractLeaseID);

                if (existingContractLease == null)
                {
>>>>>>> Stashed changes
                    return NotFound();
                }

                ContractLease cl = mapper.Map<ContractLease>(contractLease);

                validator.ValidateAndThrow(cl);

                mapper.Map(cl, existingContractLease);
                await contractLeaseRepository.SaveChangesAsync();

<<<<<<< Updated upstream
                await logger.LogMessage(LogLevel.Information, "Contract Lease object updated successfully!", "Document microservice", "UpdateContractLeaseAsync");

                return Ok(mapper.Map<ContractLeaseDto>(existingContractLease));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for contract lease object failed!", "Document microservice", "UpdateContractLeaseAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contract lease object updating failed!", "Document microservice", "UpdateContractLeaseAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
=======
                return Ok(mapper.Map<ContractLeaseDto>(existingContractLease));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating Contract lease object");
>>>>>>> Stashed changes
            }
        }

        /// <summary>
<<<<<<< Updated upstream
        /// Briše tip garancije na osnovu ID-ja
        /// </summary>
        /// <param name="guaranteeTypeId">ID tipa garancije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip garancije uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip garancije za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja tipa garancije</response>
=======
        /// Briše dokument na osnovu ID-ja
        /// </summary>
        /// <param name="documentId">ID dokument</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Dokument uspešno obrisan</response>
        /// <response code="404">Nije pronađen dokument za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dokumenta</response>
>>>>>>> Stashed changes
        [HttpDelete("{contractLeaseID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContractLeaseAsync(Guid contractLeaseID)
        {
            try
            {
<<<<<<< Updated upstream
                ContractLease contractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);
                if (contractLease == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contract lease object not found!", "Document microservice", "DeleteContractLeaseAsync");
=======
                ContractLease cl = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);
                if (cl == null)
                {
>>>>>>> Stashed changes
                    return NotFound();
                }

                await contractLeaseRepository.DeleteContractLeaseAsync(contractLeaseID);
                await contractLeaseRepository.SaveChangesAsync();
<<<<<<< Updated upstream

                await logger.LogMessage(LogLevel.Information, "Contract lease object deleted successfully!", "Document microservice", "DeleteContractLeaseAsync");

                return NoContent(); // Successful deletion

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contract lease object deletion failed!", "Document microservice", "DeleteContractLeaseAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve tipove garancije
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetContractLeaseOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Document microservice", "GetDocumentOptions");

            return Ok();
        }
=======
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

>>>>>>> Stashed changes
    }
}
