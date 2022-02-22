using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.BoardNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        //private readonly PriorityValidator validator;

        public BoardNumberController(IBoardNumberRepository boardNumberRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.boardNumberRepository = boardNumberRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            //this.validator = validator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<BoardNumberDto>>> GetBoardNumberAsync(int number = 0)
        {
            List<BoardNumber> boardNumber = await boardNumberRepository.GetBoardNumberAsync(number);

            if (boardNumber == null || boardNumber.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<BoardNumberDto>>(boardNumber));
        }

        [HttpGet("{boardNumberID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BoardNumberDto>> GetBoardNumberByIdAsync(Guid boardNumberID)
        {
            BoardNumber boardNumber = await boardNumberRepository.GetBoardNumberByIdAsync(boardNumberID);

            if (boardNumber == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BoardNumberDto>(boardNumber));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardNumberConfirmationDto>> CreateBoardNumberAsync([FromBody] BoardNumberCreationDto BoardNumberCreation)
        {
            try
            {
                BoardNumber boardNumber = mapper.Map<BoardNumber>(BoardNumberCreation);

                //validator.ValidateAndThrow(boardNumber);

                BoardNumberConfirmation boardNumberConfirmation = await boardNumberRepository.CreateBoardNumberAsync(boardNumber);
                await boardNumberRepository.SaveChangesAsync();

                string uri = linkGenerator.GetPathByAction("GetBoardNumber", "BoardNumber", new { boardNumberID = boardNumberConfirmation.boardNumberID });

                return Created(uri, mapper.Map<BoardNumberConfirmationDto>(boardNumberConfirmation));

            }
            catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardNumberDto>> UpdateBoardNumber(BoardNumberUpdateDto boardNumberUpdate)
        {
            try
            {
                BoardNumber existingBoardNumber = await boardNumberRepository.GetBoardNumberByIdAsync(boardNumberUpdate.boardNumberID);

                if (existingBoardNumber == null)
                {
                    return NotFound();
                }

                BoardNumber boardNumber = mapper.Map<BoardNumber>(boardNumberUpdate);

                //validator.ValidateAndThrow(priority);

                mapper.Map(boardNumber, existingBoardNumber);

                await boardNumberRepository.SaveChangesAsync();

                return Ok(mapper.Map<BoardNumberDto>(existingBoardNumber));

            }
            catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{boardNumberID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBoardNumberAsync(Guid boardNumberID)
        {
            try
            {
                BoardNumber boardNumber = await boardNumberRepository.GetBoardNumberByIdAsync(boardNumberID);

                if (boardNumber == null)
                {
                    return NotFound();
                }

                await boardNumberRepository.DeleteBoardNumberAsync(boardNumberID);
                await boardNumberRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPriorityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
