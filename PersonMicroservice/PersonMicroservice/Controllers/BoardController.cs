
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using PersonMicroservice.Data;
using PersonMicroservice.Entities;
using PersonMicroservice.Models;
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

        public BoardController(IBoardRepository boardRepository, IPersonRepository personRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.boardRepository = boardRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.personRepository = personRepository;
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
                return NoContent();
            }

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
                return NotFound();
            }

            return Ok(mapper.Map<BoardDto>(board));
        }


        /// <summary>
        /// Kreira novu komisiju
        /// </summary>
        /// <param name="board">Model komisije</param>
        /// <returns>Potvrda o kreiranju komisije</returns>
        /// <response code="201">Vraća kreiranu komisiju</response>
        /// <response code="500">Desila se greška prilikom kreiranja nove komisije</response>
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

                BoardConfirmation newBoard = await boardRepository.CreateBoard(mappedBoard);

                string location = linkGenerator.GetPathByAction("GetBoardById", "Board", new { boardId = newBoard.BoardId });

                return Created(location, mapper.Map<BoardConfirmationDto>(newBoard));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom kreiranja nove komisije.");
            }
        }

        /// <summary>
        /// Modifikacija komisije
        /// </summary>
        /// <param name="board">Model komisije</param>
        /// <returns>Potvrda o izmeni komisije</returns>
        /// <response code="200">Komisija je uspešno izmenjena</response>
        /// <response code="404">Nije pronađena komisija sa unetim ID-em</response>
        /// <response code="500">Serverska greška tokom izmene komisije</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardConfirmationDto>> UpdateBoard(BoardUpdateDto board)
        {
            try
            {
                var oldBoard = await boardRepository.GetBoardById(board.BoardId);

                if (oldBoard == null)
                {
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
                
                return Ok(mapper.Map<BoardConfirmationDto>(oldBoard));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom modifikacije komisije.");
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
                    return NotFound();
                }

                await boardRepository.DeleteBoard(boardId);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom brisanja komisije");
            }
        }

    }
}
