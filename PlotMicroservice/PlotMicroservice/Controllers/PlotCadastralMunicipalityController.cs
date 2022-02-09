using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models;
using PlotMicroservice.Validators;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plot-cadastral-municipalities")]
    [Produces("application/json", "application/xml")]
    //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
    public class PlotCadastralMunicipalityController : ControllerBase
    {
        private readonly IPlotCadastralMunicipalityRepository PlotCadastralMunicipalityRepository;
        private readonly LinkGenerator LinkGenerator;
        private readonly IMapper Mapper;
        private readonly PlotCadastralMunicipalityValidator Validator;

        public PlotCadastralMunicipalityController(IPlotCadastralMunicipalityRepository plotCadastralMunicipalityRepository, LinkGenerator linkGenerator, IMapper mapper, PlotCadastralMunicipalityValidator validator)
        {
            PlotCadastralMunicipalityRepository = plotCadastralMunicipalityRepository;
            LinkGenerator = linkGenerator;
            Mapper = mapper;
            Validator = validator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotCadastralMunicipalityDto>> GetPlotCadastralMunicipalities(string cadastrialMunicipality)
        {
            List<PlotCadastralMunicipality> municipalities = PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalities(cadastrialMunicipality);

            if (municipalities == null || municipalities.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotCadastralMunicipalityDto>>(municipalities));      
        }

        [HttpGet("{plotCadastralMunicipalityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotCadastralMunicipalityDto> GetPlotCadastralMunicipalityById(Guid plotCadastralMunicipalityId)
        {
           
            PlotCadastralMunicipality municipality = PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalityById(plotCadastralMunicipalityId);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotCadastralMunicipalityDto>(municipality));
        }
        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotCadastralMunicipalityConfirmationDto> CreatePlotCadastralMunicipality([FromBody] PlotCadastralMunicipalityCreationDto municipality)
        {
            try
            {

                PlotCadastralMunicipality cadastralMunicipality = Mapper.Map<PlotCadastralMunicipality>(municipality);

                Validator.ValidateAndThrow(cadastralMunicipality);

                PlotCadastralMunicipalityConfirmation confirmation = PlotCadastralMunicipalityRepository.CreatePlotCadastralMunicipality(cadastralMunicipality);
                PlotCadastralMunicipalityRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotCadastralMunicipalities", "PlotCadastralMunicipality", new { cadastralMunicipalityId = confirmation.PlotCadastralMunicipalityId });

                return Created(uri, Mapper.Map<PlotCadastralMunicipalityConfirmationDto>(confirmation));
               
            } catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch(Exception ex)
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
        public ActionResult<PlotCadastralMunicipalityDto> UpdatePlotCadastralMunicipality(PlotCadastralMunicipalityUpdateDto plotCadastralMunicipality)
        {
            try
            {
                PlotCadastralMunicipality existingCadastralMunicipality = PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalityById(plotCadastralMunicipality.PlotCadastralMunicipalityId);
                
                if(existingCadastralMunicipality == null)
                {
                    return NotFound();
                }

                PlotCadastralMunicipality cadastralMunicipality = Mapper.Map<PlotCadastralMunicipality>(plotCadastralMunicipality);

                Validator.ValidateAndThrow(cadastralMunicipality);

                Mapper.Map(cadastralMunicipality, existingCadastralMunicipality);
                PlotCadastralMunicipalityRepository.SaveChanges();

                return Ok(Mapper.Map<PlotCadastralMunicipalityDto>(existingCadastralMunicipality));

            } catch(ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotCadastrialMunicipalityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotCadastralMunicipality(Guid plotCadastrialMunicipalityId)
        {
            try
            {
                PlotCadastralMunicipality cadastralMunicipality = PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalityById(plotCadastrialMunicipalityId);
                if (cadastralMunicipality == null)
                {
                    return NotFound();
                }

                PlotCadastralMunicipalityRepository.DeletePlotCadastralMunicipality(plotCadastrialMunicipalityId);
                PlotCadastralMunicipalityRepository.SaveChanges();
                return NoContent(); // Successful deletion

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotCadastralMunicipalityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
