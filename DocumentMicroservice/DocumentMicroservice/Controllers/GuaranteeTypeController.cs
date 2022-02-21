using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.GuaranteeType;
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
        [Route("api/guaranteeType")]
        [Produces("application/json", "application/xml")]
        //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
        public class GuaranteeTypeController : ControllerBase
        {
            private readonly IGuaranteeTypeRepository GuaranteeTypeRepository;
            private readonly LinkGenerator LinkGeneration;
            private readonly IMapper Mapper;
            private readonly GuaranteeTypeValidators validator;

            
            public GuaranteeTypeController(IGuaranteeTypeRepository guaranteeTypeRepository, LinkGenerator linkGeneration, IMapper mapper, GuaranteeTypeValidators validator)
            {
                GuaranteeTypeRepository = guaranteeTypeRepository;
                LinkGeneration = linkGeneration;
                Mapper = mapper;
                this.validator = validator;
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
            public async Task< ActionResult<List<GuaranteeTypeDto>>> GetGuaranteeTypeAsync (string guaranteeType)
            {
                List<GuaranteeType> guaranteeTypeList =await  GuaranteeTypeRepository.GetGuaranteeTypeAsync(guaranteeType);

                if (guaranteeTypeList == null || guaranteeTypeList.Count == 0)
                {
                    return NoContent();
                }

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
            public async Task<ActionResult<GuaranteeTypeDto>> GetGuaranteeTypeByIdAsync(Guid GuaranteeTypeId)
            {
                GuaranteeType guaranteeType = await GuaranteeTypeRepository.GetGuaranteeTypeByIdAsync(GuaranteeTypeId);

                if (guaranteeType == null)
                {
                    return NotFound();
                }
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
            public async Task< ActionResult<GuaranteeTypeConfirmationDto>> CreateGuaranteeTypeAsync([FromBody] GuaranteeTypeCreationDto guaranteeType)
            {
                try
                {
                    GuaranteeType guarType = Mapper.Map<GuaranteeType>(guaranteeType);

                    validator.ValidateAndThrow(guarType); 

                    GuaranteeTypeConfirmation confirmation = await GuaranteeTypeRepository.CreateGuaranteeTypeAsync(guarType);
                    await GuaranteeTypeRepository.SaveChangesAsync();


                    string uri = LinkGeneration.GetPathByAction("GetGuaranteeTypeById", "GuaranteeType", new { GuaranteeTypeId = confirmation.guaranteeTypeID });

                    return Created(uri, Mapper.Map<GuaranteeTypeConfirmationDto>(confirmation));
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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
            public async Task<ActionResult<GuaranteeTypeDto>> UpdateGuaranteeTypeAsync(GuaranteeTypeUpdateDto guaranteeType)
            {
                try
                {
                GuaranteeType existingGuaranteeType = await GuaranteeTypeRepository.GetGuaranteeTypeByIdAsync(guaranteeType.GuaranteeTypeID);

                    if (existingGuaranteeType == null)
                    {
                        return NotFound();
                    }

                    GuaranteeType guarType = Mapper.Map<GuaranteeType>(guaranteeType);

                    validator.ValidateAndThrow(guarType);

                    Mapper.Map(guarType, existingGuaranteeType);
                    await GuaranteeTypeRepository.SaveChangesAsync();

                    return Ok(Mapper.Map<GuaranteeTypeDto>(existingGuaranteeType));

                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating guarantee Type object");
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
            public async Task<IActionResult> DeleteGuaranteeTypeAsync(Guid guaranteeTypeId)
            {
                try
                {
                GuaranteeType docStatus =await  GuaranteeTypeRepository.GetGuaranteeTypeByIdAsync(guaranteeTypeId);
                    if (docStatus == null)
                    {
                        return NotFound();
                    }

                    await GuaranteeTypeRepository.DeleteGuaranteeTypeAsync(guaranteeTypeId);
                    await GuaranteeTypeRepository.SaveChangesAsync();
                    return NoContent(); // Successful deletion

                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting guarantee type object!");
                }
            }

            /// <summary>
            /// Vraća informacije o opcijama koje je moguće izvršiti za sve tipove garancije
            /// </summary>
            /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
            [HttpOptions]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [AllowAnonymous]
            public IActionResult GetGuaranteeTypeOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
                return Ok();
            }
        }
}
