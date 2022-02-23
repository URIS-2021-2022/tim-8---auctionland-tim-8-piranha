using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.GuaranteeType;
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
        [Route("api/guaranteeType")]
        [Produces("application/json", "application/xml")]
        [Authorize]
    public class GuaranteeTypeController : ControllerBase
        {
            private readonly IGuaranteeTypeRepository GuaranteeTypeRepository;
            private readonly LinkGenerator LinkGeneration;
            private readonly IMapper Mapper;
            private readonly GuaranteeTypeValidators validator;
            private readonly ILoggerService logger;


        public GuaranteeTypeController(IGuaranteeTypeRepository guaranteeTypeRepository, LinkGenerator linkGeneration, IMapper mapper, GuaranteeTypeValidators validator, ILoggerService logger)
            {
                GuaranteeTypeRepository = guaranteeTypeRepository;
                LinkGeneration = linkGeneration;
                Mapper = mapper;
                this.validator = validator;
                this.logger = logger;
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
            [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task< ActionResult<List<GuaranteeTypeDto>>> GetGuaranteeTypeAsync (string guaranteeType)
            {
                List<GuaranteeType> guaranteeTypeList =await  GuaranteeTypeRepository.GetGuaranteeTypeAsync(guaranteeType);

                if (guaranteeTypeList == null || guaranteeTypeList.Count == 0)
                {
                await logger.LogMessage(LogLevel.Warning, "Guarantee Type list is empty!", "Document microservice", "GetGuaranteeTypeAsync");

                return NoContent();
                }

            await logger.LogMessage(LogLevel.Information, "Guarantee Type list successfully returned!", "Document microservice", "GetGuaranteeTypeAsync");

            return Ok(Mapper.Map<List<GuaranteeTypeDto>>(guaranteeTypeList));
            }

            /// <summary>
            /// Vraća traženi tip garancije po ID-ju
            /// </summary>
            /// <param name="GuaranteeTypeId">ID tipa garancije</param>
            /// <returns>Traženi tip garacnije</returns>
            /// <response code = "200">Vraća traženi tip garancije</response>
            /// <response code = "404">Nije pronađen traženi tip garancije</response>
            [HttpGet("{guaranteeTypeId}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
            public async Task<ActionResult<GuaranteeTypeDto>> GetGuaranteeTypeByIdAsync(Guid GuaranteeTypeId)
            {
                GuaranteeType guaranteeType = await GuaranteeTypeRepository.GetGuaranteeTypeByIdAsync(GuaranteeTypeId);

                if (guaranteeType == null)
                {
                await logger.LogMessage(LogLevel.Warning, "Guarantee Type not found!", "Document microservice", "GetGuaranteeTypeByIdAsync");

                return NotFound();
                }
            await logger.LogMessage(LogLevel.Information, "Guarantee Type found and successfully returned!", "Document microservice", "GetGuaranteeTypeByIdAsync");

            return Ok(Mapper.Map<GuaranteeTypeDto>(guaranteeType));
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
            [HttpPost]
            [Consumes("application/json")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task< ActionResult<GuaranteeTypeConfirmationDto>> CreateGuaranteeTypeAsync([FromBody] GuaranteeTypeCreationDto guaranteeType)
            {
                try
                {
                    GuaranteeType guarType = Mapper.Map<GuaranteeType>(guaranteeType);

                    validator.ValidateAndThrow(guarType); 

                    GuaranteeTypeConfirmation confirmation = await GuaranteeTypeRepository.CreateGuaranteeTypeAsync(guarType);
                    await GuaranteeTypeRepository.SaveChangesAsync();


                    string uri = LinkGeneration.GetPathByAction("GetGuaranteeTypeById", "GuaranteeType", new { GuaranteeTypeId = confirmation.guaranteeTypeID });

                await logger.LogMessage(LogLevel.Information, "Guarantee Type  protected zone successfully created!", "Plot microservice", "CreateGuaranteeTypeAsync");

                return Created(uri, Mapper.Map<GuaranteeTypeConfirmationDto>(confirmation));
                }
            catch (ValidationException ve)
            {
               await logger.LogMessage(LogLevel.Error, "Validation for Guarantee Type object failed!", "Document microservice", "CreateGuaranteeTypeAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Guarantee Type object creation failed!", "Document microservice", "CreateGuaranteeTypeAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan tip garancije
        /// </summary>
        /// <param name="guaranteeType">Model tipa garancije koji se ažurira</param>
        /// <returns>Potvrda o ažuriranom tipu garancije</returns>
        /// <response code="200">Vraća ažurirani tip garancije</response>
        /// <response code="404">Nije pronađen tip garancije za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa garancije</response>
        [HttpPut]
            [Consumes("application/json")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
            public async Task<ActionResult<GuaranteeTypeDto>> UpdateGuaranteeTypeAsync(GuaranteeTypeUpdateDto guaranteeType)
            {
                try
                {
                GuaranteeType existingGuaranteeType = await GuaranteeTypeRepository.GetGuaranteeTypeByIdAsync(guaranteeType.GuaranteeTypeID);

                    if (existingGuaranteeType == null)
                    {
                    await logger.LogMessage(LogLevel.Warning, "Guarantee Type object not found!", "Document microservice", "UpdateGuaranteeTypeAsync");

                    return NotFound();
                    }

                    GuaranteeType guarType = Mapper.Map<GuaranteeType>(guaranteeType);

                    validator.ValidateAndThrow(guarType);

                    Mapper.Map(guarType, existingGuaranteeType);
                    await GuaranteeTypeRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Guarantee Type object updated successfully!", "Document microservice", "UpdateGuaranteeTypeAsync");

                return Ok(Mapper.Map<GuaranteeTypeDto>(existingGuaranteeType));

                }
            catch (ValidationException ve)
            {
               await logger.LogMessage(LogLevel.Error, "Validation for GuaranteeType object failed!", "Document microservice", "UpdateGuaranteeTypeAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, " GuaranteeType object updating failed!", "Document microservice", "UpdateGuaranteeTypeAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Briše tip garancije na osnovu ID-ja
        /// </summary>
        /// <param name="guaranteeTypeId">ID tipa garancije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip garancije uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip garancije za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja tipa garancije</response>
        [HttpDelete("{guaranteeTypeId}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<IActionResult> DeleteGuaranteeTypeAsync(Guid guaranteeTypeId)
            {
                try
                {
                GuaranteeType guaranteeType = await  GuaranteeTypeRepository.GetGuaranteeTypeByIdAsync(guaranteeTypeId);
                    if (guaranteeType == null)
                    {
                    await logger.LogMessage(LogLevel.Warning, "Guarantee Type object not found!", "Document microservice", "DeleteGuaranteeTypeAsync");

                    return NotFound();
                    }

                    await GuaranteeTypeRepository.DeleteGuaranteeTypeAsync(guaranteeTypeId);
                    await GuaranteeTypeRepository.SaveChangesAsync();
                await logger.LogMessage(LogLevel.Information, "Guarantee Type object deleted successfully!", "Document microservice", "DeleteGuaranteeTypeAsync");

                return NoContent(); // Successful deletion

                }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Guarantee Type object deletion failed!", "Document microservice", "DeleteGuaranteeTypeAsync");
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
            [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
         public async Task<IActionResult>  GetGuaranteeTypeOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Document microservice", "GetGuaranteeTypeOptions");

            return Ok();
            }
        }
}
