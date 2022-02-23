using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.BoardNumber;
using BuyerMicroservice.ServiceCalls;
using BuyerMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace BuyerMicroservice.Controllers
{
    [ApiController]
    [Route("api/boardNumber")]
    [Produces("application/json", "application/xml")]
    public class BoardNumberController : ControllerBase
    {
        private readonly IBoardNumberRepository boardNumberRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly BoardNumberValidator validator;
        private readonly ILoggerService logger;


        public BoardNumberController(IBoardNumberRepository boardNumberRepository, LinkGenerator linkGenerator, IMapper mapper, BoardNumberValidator validator, ILoggerService logger)
        {
            this.boardNumberRepository = boardNumberRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve brojeve table
        /// </summary>
        /// <returns>Lista brojeva table</returns>
        /// <response code = "200">Vraća listu brojeva table</response>
        /// <response code = "204">Ne postoji nijedna broj table</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<List<BoardNumberDto>>> GetBoardNumberAsync(int number = 0)
        {
            List<BoardNumber> boardNumber = await boardNumberRepository.GetBoardNumberAsync(number);

            if (boardNumber == null || boardNumber.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "BoardNumber  culture list is empty!", "Buyer  microservice", "GetBoardNumberAsync");

                return NoContent();
            }
            await logger.LogMessage(LogLevel.Information, "BoardNumber  list successfully returned!", "Buyer  microservice", "GetBoardNumberAsync");

            return Ok(mapper.Map<List<BoardNumberDto>>(boardNumber));
        }

        /// <summary>
        /// Vraća traženi broj table po ID-ju
        /// </summary>
        /// <param name="boardNumberID">ID broja table</param>
        /// <returns>Traženi broj table</returns>
        /// <response code = "200">Vraća traženi broj table</response>
        /// <response code = "404">Nije pronađen traženi broj table</response>
        [HttpGet("{boardNumberID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<ActionResult<BoardNumberDto>> GetBoardNumberByIdAsync(Guid boardNumberID)
        {
            BoardNumber boardNumber = await boardNumberRepository.GetBoardNumberByIdAsync(boardNumberID);

            if (boardNumber == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Board Number not found!", "Buyer microservice", "GetBoardNumberByIdAsync");

                return NotFound();
            }
            await logger.LogMessage(LogLevel.Information, "Board Number found and successfully returned!", "Buyer microservice", "GetBoardNumberByIdAsync");

            return Ok(mapper.Map<BoardNumberDto>(boardNumber));
        }
        /// <summary>
        /// Kreira novi broj table
        /// </summary>
        /// <param name="BoardNumberCreation"> model broja table</param>
        /// <returns>Potvrda o kreiranom broju table</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog broja table \
        /// POST /api/boardNumber \
        /// { \
        ///  "boardNumberID" : "8d951bd9-497a-47ec-b1a7-c944492f4c8c", \
        ///  "number" : "5", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreirani broj table</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja broja table</response>

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<BoardNumberConfirmationDto>> CreateBoardNumberAsync([FromBody] BoardNumberCreationDto BoardNumberCreation)
        {
            try
            {
                BoardNumber boardNumber = mapper.Map<BoardNumber>(BoardNumberCreation);

                validator.ValidateAndThrow(boardNumber);

                BoardNumberConfirmation boardNumberConfirmation = await boardNumberRepository.CreateBoardNumberAsync(boardNumber);
                await boardNumberRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetBoardNumber", "BoardNumber", new { boardNumberID = boardNumberConfirmation.boardNumberID });

                await logger.LogMessage(LogLevel.Information, "Board number  successfully created!", "Buyer microservice", "CreateBoardNumberAsync");

                return Created(uri, mapper.Map<BoardNumberConfirmationDto>(boardNumberConfirmation));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Board number  object failed!", "Buyer microservice", "CreateBoardNumberAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Board Number object creation failed!", "Buyer microservice", "CreateBoardNumberAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Ažurira jedan broj table
        /// </summary>
        /// <param name="boardNumberUpdate">Model broja table koja se ažurira</param>
        /// <returns>Potvrda o ažuriranom broju table</returns>
        /// <response code="200">Vraća ažurirani broj table</response>
        /// <response code="404">Nije pronađen broj table za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja broja table</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<ActionResult<BoardNumberDto>> UpdateBoardNumberAsync(BoardNumberUpdateDto boardNumberUpdate)
        {
            try
            {
                BoardNumber existingBoardNumber = await boardNumberRepository.GetBoardNumberByIdAsync(boardNumberUpdate.boardNumberID);

                if (existingBoardNumber == null)
                {
                    await logger.LogMessage(LogLevel.Warning, " Board number object not found!", "Buyer microservice", "UpdateBoardNumberAsync");

                    return NotFound();
                }

                BoardNumber boardNumber = mapper.Map<BoardNumber>(boardNumberUpdate);

                //validator.ValidateAndThrow(priority);

                mapper.Map(boardNumber, existingBoardNumber);

                await boardNumberRepository.SaveChangesAsync();
                await logger.LogMessage(LogLevel.Information, "Board number   object updated successfully!", "Buyer microservice", "UpdateBoardNumberAsync");

                return Ok(mapper.Map<BoardNumberDto>(existingBoardNumber));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for Board number   object failed!", "Buyer microservice", "UpdateBoardNumberAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Board number  object updating failed!", "Buyer microservice", "UpdateBoardNumberAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Briše broj table na osnovu ID-ja
        /// </summary>
        /// <param name="boardNumberID">ID broja table</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Broj table uspešno obrisan</response>
        /// <response code="404">Nije pronađen broj table za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja broja table</response>
        [HttpDelete("{boardNumberID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator, Superuser, TehnickiSekretar")]
        public async Task<IActionResult> DeleteBoardNumberAsync(Guid boardNumberID)
        {
            try
            {
                BoardNumber boardNumber = await boardNumberRepository.GetBoardNumberByIdAsync(boardNumberID);

                if (boardNumber == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Board number object not found!", "Buyer microservice", "DeleteBoardNumberAsync");

                    return NotFound();
                }

                await boardNumberRepository.DeleteBoardNumberAsync(boardNumberID);
                await boardNumberRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Board number object deleted successfully!", "Buyer microservice", "DeleteBoardNumberAsync");

                return NoContent();

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Board number object deletion failed!", "Buyer microservice", "DeleteBoardNumberAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve brojeve table
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Menadzer, TehnickiSekretar")]
        public async Task<IActionResult>  GetBoardNumberOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "BoardNumber microservice", "GetBoardNumberOptions");
            return Ok();
        }
    }
}
