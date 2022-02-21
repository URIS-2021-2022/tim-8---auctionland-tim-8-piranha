
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonMicroservice.Data;
using PersonMicroservice.Entities;
using PersonMicroservice.Models;
using PersonMicroservice.ServiceCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Controllers
{
    /// <summary>
    /// Kontroler za komisiju
    /// </summary>
    [ApiController]
    [Route("api/person/board")]
    [Produces("application/json", "application/xml")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository boardRepository;
        private readonly IPersonRepository personRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService logger;

        public BoardController(IBoardRepository boardRepository, IPersonRepository personRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService logger)
        {
            this.boardRepository = boardRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.personRepository = personRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve komisije
        /// </summary>
        /// <returns>Lista komisija</returns>
        /// <response code="200">Vraća listu komisija</response>
        /// <response code="404">Nije pronađena ni jedna komisija</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BoardDto>>> GetAllBoards()
        {
            var boards = await boardRepository.GetAllBoards();

            if (boards == null || boards.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Board list is empty!", "Person microservice", "GetAllBoards");
                return NoContent();
            }

            await logger.LogMessage(LogLevel.Information, "Board list successfully returned!", "Person microservice", "GetAllBoards");
            return Ok(mapper.Map<List<BoardDto>>(boards));
        }

        /// <summary>
        /// Vraća jednu komisiju sa prosleđenim ID-em
        /// </summary>
        /// <param name="boardId">ID komisije</param>
        /// <returns>Komisija</returns>
        /// <response code="200">Vraća traženu komisiju</response>
        /// <response code="404">Nije pronađena komisija sa unetim ID-em</response>
        [HttpGet("{boardId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BoardDto>> GetBoardById(Guid boardId)
        {
            var board = await boardRepository.GetBoardById(boardId);

            if (board == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Board not found!", "Person microservice", "GetBoardById");
                return NotFound();
            }

            await logger.LogMessage(LogLevel.Information, "Board found and successfully returned!", "Person microservice", "GetBoardById");
            return Ok(mapper.Map<BoardDto>(board));
        }


        /// <summary>
        /// Kreira novu komisiju
        /// </summary>
        /// <param name="board">Model komisije</param>
        /// <returns>Potvrda o kreiranju komisije</returns>
        /// <response code="201">Vraća kreiranu komisiju</response>
        /// <response code="500">Desila se greška prilikom kreiranja nove komisije</response>
        /// /// <remarks>
        /// Primer POST zahteva \
        /// POST /api/person/board \
        /// { \
        ///     "president": "2d8607c5-f3cf-4ef5-9323-a9318eee6232", \
        ///     "members": [] \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardConfirmationDto>> CreateBoard([FromBody] BoardCreationDto board)
        {
            try
            {
                var members = new List<Person>();
                if (board.Members is not null)
                {
                    foreach (var member in board.Members)
                    {
                        Person temp = await personRepository.GetPersonById(member);
                        if (temp != null)
                            members.Add(temp);
                    }
                }

                Board mappedBoard = mapper.Map<Board>(board);
                mappedBoard.Members = members;
                mappedBoard.President = await personRepository.GetPersonById(mappedBoard.PresidentId);

                BoardConfirmation newBoard = await boardRepository.CreateBoard(mappedBoard);

                string location = linkGenerator.GetPathByAction("GetBoardById", "Board", new { boardId = newBoard.BoardId });

                await logger.LogMessage(LogLevel.Information, "Board successfully created!", "Person microservice", "CreateBoard");
                return Created(location, mapper.Map<BoardConfirmationDto>(newBoard));
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Board object creation failed!", "Person microservice", "CreateBoard");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja nove komisije." + ex);
            }
        }

        /// <summary>
        /// Modifikacija komisije
        /// </summary>
        /// <param name="board">Model komisije</param>
        /// /// <param name="boardId">Id komisije</param>
        /// <returns>Potvrda o izmeni komisije</returns>
        /// <response code="200">Komisija je uspešno izmenjena</response>
        /// <response code="404">Nije pronađena komisija sa unetim ID-em</response>
        /// <response code="500">Serverska greška tokom izmene komisije</response>
        /// /// /// <remarks>
        /// Primer PUT zahteva \
        /// PUT /api/person \
        /// {   \
        ///     "president": "5283dcc9-7010-459b-87d7-346820a32f31", \
        ///     "members": [] \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardConfirmationDto>> UpdateBoard(Guid boardId, [FromBody] BoardUpdateDto board)
        {
            try
            {
                var oldBoard = await boardRepository.GetBoardById(boardId);

                if (oldBoard == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Board object not found!", "Person microservice", "UpdateBoard");
                    return NotFound();
                }

                Board newBoard = mapper.Map<Board>(board);

                mapper.Map(newBoard, oldBoard);

                var members = new List<Person>();

                if (board.Members is not null)
                {
                    foreach (var member in board.Members)
                    {
                        Person temp = await personRepository.GetPersonById(member);
                        if (temp != null)
                        {
                            members.Add(temp);
                        }
                    }
                }

                oldBoard.Members = members;
                oldBoard.President = await personRepository.GetPersonById(oldBoard.PresidentId);

                await boardRepository.UpdateBoard(newBoard);

                await logger.LogMessage(LogLevel.Information, "Board object updated successfully!", "Person microservice", "UpdateBoard");
                return Ok(mapper.Map<BoardConfirmationDto>(oldBoard));
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Board object updating failed!", "Person microservice", "UpdateBoard");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom modifikacije komisije." + ex);
            }
        }

        /// <summary>
        /// Brisanje komisije sa prosleđenim ID-em
        /// </summary>
        /// <param name="boardId">ID komisije</param>
        /// <response code="204">Komisija je uspešno obrisana</response>
        /// <response code="404">Nije pronađena komisija sa unetim ID-em</response>
        /// <response code="500">Serverska greška tokom brisanja komisije</response>
        [HttpDelete("{boardId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBoard(Guid boardId)
        {
            try
            {
                var board = await boardRepository.GetBoardById(boardId);

                if (board == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Board object not found!", "Person microservice", "DeleteBoard");
                    return NotFound();
                }

                await boardRepository.DeleteBoard(boardId);

                await logger.LogMessage(LogLevel.Information, "Board object deleted successfull!", "Person microservice", "DeleteBoard");
                return Ok();
            }
            catch (Exception)
            {
                await logger.LogMessage(LogLevel.Error, "Board object deletion failed!", "Person microservice", "DeleteBoard");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom brisanja komisije");
            }
        }


        /// <summary>
        /// Zaglavlje odgovora
        /// </summary>
        /// <returns>Zaglavlje odgovora</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetBoardOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Board microservice", "GetBoardOptions");

            return Ok();
        }

    }
}
