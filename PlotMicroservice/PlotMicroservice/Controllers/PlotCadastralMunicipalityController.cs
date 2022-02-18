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
    /// <summary>
    /// Plot cadastral municipality controller. Gives access to fields and methods of cadastral municipality.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-cadastral-municipalities")]
    [Produces("application/json", "application/xml")]
    public class PlotCadastralMunicipalityController : ControllerBase
    {
        private readonly IPlotCadastralMunicipalityRepository PlotCadastralMunicipalityRepository;
        private readonly LinkGenerator LinkGenerator;
        private readonly IMapper Mapper;
        private readonly PlotCadastralMunicipalityValidator Validator;

        /// <summary>
        /// Plot cadastral municipality constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotCadastralMunicipalityRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="validator"></param>
        public PlotCadastralMunicipalityController(IPlotCadastralMunicipalityRepository plotCadastralMunicipalityRepository, LinkGenerator linkGenerator, IMapper mapper, PlotCadastralMunicipalityValidator validator)
        {
            PlotCadastralMunicipalityRepository = plotCadastralMunicipalityRepository;
            LinkGenerator = linkGenerator;
            Mapper = mapper;
            Validator = validator;
        }

        /// <summary>
        /// Getting all instances of cadastral municipality for given filter.
        /// </summary>
        /// <param name="cadastrialMunicipality">Plot cadastral municipality (ex. Stari Grad).</param>
        /// <returns>List of cadastral municipalities.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotCadastralMunicipalityDto>>> GetPlotCadastralMunicipalitiesAsync(string cadastrialMunicipality)
        {
            List<PlotCadastralMunicipality> municipalities = await PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalitiesAsync(cadastrialMunicipality);

            if (municipalities == null || municipalities.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotCadastralMunicipalityDto>>(municipalities));      
        }

        /// <summary>
        /// Getting plot cadastral municipality by given GUID of cadastral municipality as parameter.
        /// </summary>
        /// <param name="plotCadastralMunicipalityId"></param>
        /// <returns>Single plot cadastral municipality.</returns>
        [HttpGet("{plotCadastralMunicipalityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotCadastralMunicipalityDto>> GetPlotCadastralMunicipalityByIdAsync(Guid plotCadastralMunicipalityId)
        {
           
            PlotCadastralMunicipality municipality = await PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalityByIdAsync(plotCadastralMunicipalityId);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotCadastralMunicipalityDto>(municipality));
        }

        /// <summary>
        /// Creating new plot cadastral municipality.
        /// </summary>
        /// <param name="municipality"></param>
        /// <returns>Confirmation about created cadastral municipality.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-cadastral-municipalities \
        /// {   \
        ///     "cadastralMunicipality": "New cadastral municipality" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotCadastralMunicipalityConfirmationDto>> CreatePlotCadastralMunicipalityAsync([FromBody] PlotCadastralMunicipalityCreationDto municipality)
        {
            try
            {

                PlotCadastralMunicipality cadastralMunicipality = Mapper.Map<PlotCadastralMunicipality>(municipality);

                Validator.ValidateAndThrow(cadastralMunicipality);

                PlotCadastralMunicipalityConfirmation confirmation = await PlotCadastralMunicipalityRepository.CreatePlotCadastralMunicipalityAsync(cadastralMunicipality);
                await PlotCadastralMunicipalityRepository.SaveChangesAsync();

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

        /// <summary>
        /// Updating existing plot cadastral municipality by given GUID. 
        /// </summary>
        /// <param name="plotCadastralMunicipality"></param>
        /// <returns>Updated plot cadastral municipality.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT api/plot-cadastral-municipalities \
        /// {   \
        ///     "plotCadastralMunicipalityId": "93a08cc2-1d17-46e6-bd95-4fa70bb11226", \
        ///      "cadastralMunicipality": "Subotica" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotCadastralMunicipalityDto>> UpdatePlotCadastralMunicipalityAsync(PlotCadastralMunicipalityUpdateDto plotCadastralMunicipality)
        {
            try
            {
                PlotCadastralMunicipality existingCadastralMunicipality = await PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalityByIdAsync(plotCadastralMunicipality.PlotCadastralMunicipalityId);
                
                if(existingCadastralMunicipality == null)
                {
                    return NotFound();
                }

                PlotCadastralMunicipality cadastralMunicipality = Mapper.Map<PlotCadastralMunicipality>(plotCadastralMunicipality);

                Validator.ValidateAndThrow(cadastralMunicipality);

                Mapper.Map(cadastralMunicipality, existingCadastralMunicipality);
                await PlotCadastralMunicipalityRepository.SaveChangesAsync();

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

        /// <summary>
        /// Deleting plot cadastral municipality by given GUID.
        /// </summary>
        /// <param name="plotCadastrialMunicipalityId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotCadastrialMunicipalityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotCadastralMunicipalityAsync(Guid plotCadastrialMunicipalityId)
        {
            try
            {
                PlotCadastralMunicipality cadastralMunicipality = await PlotCadastralMunicipalityRepository.GetPlotCadastralMunicipalityByIdAsync(plotCadastrialMunicipalityId);
                
                if (cadastralMunicipality == null)
                {
                    return NotFound();
                }

                await PlotCadastralMunicipalityRepository.DeletePlotCadastralMunicipalityAsync(plotCadastrialMunicipalityId);
                await PlotCadastralMunicipalityRepository.SaveChangesAsync();
                return NoContent(); // Successful deletion

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotCadastralMunicipalityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
