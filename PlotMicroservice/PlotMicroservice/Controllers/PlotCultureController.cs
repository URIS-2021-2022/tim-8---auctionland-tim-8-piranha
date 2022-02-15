using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotCultureModel;
using PlotMicroservice.Validators;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Controllers
{
    /// <summary>
    /// Plot culutre controller. Gives access to fields and methods of plot culuture.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-cultures")]
    [Produces("application/json", "application/xml")]
    public class PlotCultureController : ControllerBase
    {
        private readonly IPlotCultureRepository PlotCultureRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotCultureValidator Validator;

        /// <summary>
        /// Plot culture constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotCultureRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        public PlotCultureController(IPlotCultureRepository plotCultureRepository, IMapper mapper, LinkGenerator linkGenerator, PlotCultureValidator validator)
        {
            PlotCultureRepository = plotCultureRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
        }

        /// <summary>
        /// Getting all instances of plot cultures for given filter.
        /// </summary>
        /// <param name="plotCulture">Plot culture (ex. Voćnjaci)</param>
        /// <returns>List of plot cultures.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotCultureDto>> GetPlotCultures(string plotCulture)
        {
            List<PlotCulture> plotCultures = PlotCultureRepository.GetPlotCultures(plotCulture);
            
            if(plotCultures == null || plotCultures.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotCultureDto>>(plotCultures));
        }

        /// <summary>
        /// Getting plot culture by given GUID of plot culture as parameter.
        /// </summary>
        /// <param name="plotCultureId"></param>
        /// <returns>Single plot culture.</returns>
        [HttpGet("{plotCultureId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotCultureDto> GetPlotCultureById(Guid plotCultureId)
        {
            PlotCulture plotCulture = PlotCultureRepository.GetPlotCultureById(plotCultureId);

            if(plotCulture == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotCultureDto>(plotCulture));
        }

        /// <summary>
        /// Creating new plot culture.
        /// </summary>
        /// <param name="plotCultureCreation"></param>
        /// <returns>Confirmation about created plot culture.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-cultures \
        /// {   \
        ///     "culture": "New plot culture" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotCultureConfirmationDto> CreatePlotCulture([FromBody] PlotCultureCreationDto plotCultureCreation)
        {
            try
            {

                PlotCulture plotCulture = Mapper.Map<PlotCulture>(plotCultureCreation);

                Validator.ValidateAndThrow(plotCulture);

                PlotCultureConfirmation plotCultureConfirmation = PlotCultureRepository.CreatePlotCulture(plotCulture);
                
                PlotCultureRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotCultures", "PlotCulture", new { plotCultureId = plotCultureConfirmation.PlotCultureId });

                return Created(uri, Mapper.Map<PlotCultureConfirmationDto>(plotCultureConfirmation));

            } catch(ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updating existing plot culuture by given GUID.
        /// </summary>
        /// <param name="plotCultureUpdate"></param>
        /// <returns>Updated plot culture.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT /api/plot-cultures \
        /// {   \
        ///     "plotCultureId": "260c190d-07b9-426b-e58b-08d9e36dc042",
        ///     "culture": "Updated culture"
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotCultureDto> UpdatePlotCulture(PlotCultureUpdateDto plotCultureUpdate)
        {
            try
            {

                PlotCulture existingPlotCulture = PlotCultureRepository.GetPlotCultureById(plotCultureUpdate.PlotCultureId);

                if (existingPlotCulture == null)
                {
                    return NotFound();
                }

                PlotCulture plotCulture = Mapper.Map<PlotCulture>(plotCultureUpdate);

                Validator.ValidateAndThrow(plotCulture);

                Mapper.Map(plotCulture, existingPlotCulture);

                PlotCultureRepository.SaveChanges();

                return Ok(Mapper.Map<PlotCultureDto>(existingPlotCulture));

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
        /// Deleting plot culture by given GUID.
        /// </summary>
        /// <param name="plotCultureId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotCultureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotCulture(Guid plotCultureId)
        {
            try
            {
                PlotCulture plotCulture = PlotCultureRepository.GetPlotCultureById(plotCultureId);

                if (plotCulture == null)
                {
                    return NotFound();
                }

                PlotCultureRepository.DeletePlotCulture(plotCultureId);
                PlotCultureRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
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
        public IActionResult GetPlotCultureOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
