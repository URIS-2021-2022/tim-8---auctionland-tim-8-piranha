using AutoMapper;
using AddressMicroservice.Data.Interfaces;
using AddressMicroservice.Entities;
using AddressMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressMicroservice.Models.State;
using FluentValidation;
using CustomValidationException = AddressMicroservice.Models.Exceptions.ValidationException;
using DocumentMicroservice.ServiceCalls;

namespace AddressMicroservice.Controllers
{
    [ApiController]
    [Route("api/State")]
    [Produces("application/json", "application/xml")]
    //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
    public class StateController : ControllerBase
    {
        private readonly IStateRepository stateRepository;
        private readonly LinkGenerator linkGeneration;
        private readonly IMapper mapper;
        private readonly IValidator<State> Validator;
        private readonly ILoggerService logger;

        public StateController(IStateRepository stateRepository, LinkGenerator linkGeneration, IMapper mapper, IValidator<State> Validator, ILoggerService logger)
        {
            this.stateRepository = stateRepository;
            this.linkGeneration = linkGeneration;
            this.mapper = mapper;
            this.Validator = Validator;
            this.logger = logger;
        }

        /// <summary>
        /// Vraca sve države na osnovu prosledjenih filtera
        /// </summary>
        /// <param name="state">Država</param>
        /// <returns>Lista svih država</returns>

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StateDto>> GetState(string state)
        {
            List<State> StateList = stateRepository.GetState(state);

            if (StateList == null || StateList.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<StateDto>>(StateList));
        }
        /// <summary>
        /// Vraća traženu državu po ID-ju
        /// </summary>
        /// <param name="StateId">ID države</param>
        /// <returns>Tražena adresa</returns>
        /// <response code = "200">Vraća traženu državu</response>
        /// <response code = "404">Nije pronađena tražena država</response>

        [HttpGet("{stateId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StateDto> GetStateById(Guid stateId)
        {
            State State = stateRepository.GetStateById(stateId);

            return Ok(mapper.Map<StateDto>(State));
        }
        /// <summary>
        /// Kreira novu državu
        /// </summary>
        /// <param name="state"> model države</param>
        /// <returns>Potvrda o kreiranoj državi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove države \
        /// POST /api/State \
        /// { \
        ///"StateID" : "84ff030b-7067-45b7-8bb2-10719534f91e",\
        ///"NameState" : "Makedonija",\
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu državu</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja države</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StateConfirmationDto>> CreateStateAsync([FromBody] StateCreationDto StateDto)
        {
            State state = mapper.Map<State>(StateDto);

            var result = await Validator.ValidateAsync(state);
            if (!result.IsValid)
            {
                throw new CustomValidationException(result.Errors);
            }

            StateConfirmation confirmation = stateRepository.CreateState(state);
            stateRepository.SaveChanges();


            string uri = linkGeneration.GetPathByAction("GetStateById", "State", new { stateId = confirmation.StateID });
            //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
            return Created(uri, mapper.Map<StateConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Ažurira jednu državu
        /// </summary>
        /// <param name="state">Model države koji se ažurira</param>
        /// <returns>Potvrda o ažuriranoj državi</returns>
        /// <response code="200">Vraća ažuriranu državu</response>
        /// <response code="404">Nije pronađena država za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja države</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StateDto>> UpdateStateAsync(StateUpdateDto stateDto)
        {
            State existingState = stateRepository.GetStateById(stateDto.StateId);

            State State = mapper.Map<State>(stateDto);

            var result = await Validator.ValidateAsync(State);
            if (!result.IsValid)
            {
                throw new CustomValidationException(result.Errors);
            }

            mapper.Map(State, existingState);
            stateRepository.SaveChanges();

            StateConfirmation confirmation = mapper.Map<StateConfirmation>(existingState);

            return Ok(mapper.Map<StateConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Briše državu na osnovu ID-ja
        /// </summary>
        /// <param name="stateId">ID države</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Država uspešno obrisana</response>
        /// <response code="404">Nije pronađena država za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja države</response>
        [HttpDelete("stateId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteState(Guid stateId)
        {
            State State = stateRepository.GetStateById(stateId);

            stateRepository.DeleteState(State);
            stateRepository.SaveChanges();

            return NoContent(); // Successful deletion -- sve je okej proslo ali ne vraca nikakav sadrzaj--> iz familije je 200
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetStateOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}