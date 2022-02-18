using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotModel;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using PlotMicroservice.Validators;

namespace PlotMicroservice.Controllers
{
    /// <summary>
    /// Plot controller. Gives access to fields and methods of plot.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plots")]
    [Produces("application/json", "application/xml")]
    public class PlotController : ControllerBase
    {
        private readonly IPlotRepository PlotRepository;
        private readonly LinkGenerator LinkGenerator;
        private readonly IMapper Mapper;
        private readonly PlotValidator Validator;

        /// <summary>
        /// Plot constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        public PlotController(IPlotRepository plotRepository, IMapper mapper, LinkGenerator linkGenerator, PlotValidator validator)
        {
            PlotRepository = plotRepository;
            LinkGenerator = linkGenerator;
            Mapper = mapper;
            Validator = validator;
        }

        /// <summary>
        /// Getting all instances of plot for given filter/s.
        /// </summary>
        /// <param name="culture">Culture (ex. Njive)</param>
        /// <param name="workability">Workability (ex. Obradivo)</param>
        /// <returns>List of plots.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<PlotDto>>> GetPlotsAsync(string culture, string workability)
        {
            List<Plot> plots = await PlotRepository.GetPlotsAsync(culture, workability);

            if(plots == null || plots.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotDto>>(plots));
        }

        /// <summary>
        /// Getting plot by given GUID of plot as parameter.
        /// </summary>
        /// <param name="plotId"></param>
        /// <returns>Single plot.</returns>
        [HttpGet("{plotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlotDto>> GetPlotByIdAsync(Guid plotId)
        {
            Plot plot = await PlotRepository.GetPlotByIdAsync(plotId);

            if(plot == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotDto>(plot));
        }

        /// <summary>
        /// Creating new plot.
        /// </summary>
        /// <param name="plotCreation"></param>
        /// <returns>Confirmation about created plot.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plots \
        /// {   \
        ///     "plotCultureId": "cc506ecd-fb9e-48d8-af90-26ecc5d9feba", \
        ///     "plotCadastralMunicipalityId": "f305096b-52fd-4c43-8699-05bc3ee664b7", \
        ///     "plotWorkabilityId": "c0615a4d-faa4-4e17-8f2f-93ec25383f9b", \
        ///     "plotSurfaceArea": 3200, \
        ///     "plotNumber": "55", \
        ///     "plotRealEstateListNumber": "LN505", \
        ///     "plotCurrentCulture": "", \
        ///     "plotCurrentWorkability": "" \
        /// }
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotConfirmationDto>> CreatePlotAsync([FromBody] PlotCreationDto plotCreation)
        {
            try
            {
                Plot plot = Mapper.Map<Plot>(plotCreation);

                Validator.ValidateAndThrow(plot);
                
                PlotConfirmation plotConfirmation = await PlotRepository.CreatePlotAsync(plot);
                await PlotRepository.SaveChangesAsync();

                string uri = LinkGenerator.GetPathByAction("GetPlots", "Plot", new { plotId = plotConfirmation.PlotId });

                return Created(uri, Mapper.Map<PlotConfirmationDto>(plotConfirmation));

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
        /// Updating existing plot by given GUID.
        /// </summary>
        /// <param name="plotUpdate"></param>
        /// <returns>Updated plot.</returns>
        /// <remarks>
        /// Example of PUT request \
        /// PUT /api/plots \
        /// {   \
        ///     "plotId": "49178d69-2ada-4d14-d731-08d9eaeac343", \
        ///     "plotCultureId": "cc506ecd-fb9e-48d8-af90-26ecc5d9feba", \
        ///     "plotCadastralMunicipalityId": "f305096b-52fd-4c43-8699-05bc3ee664b7", \
        ///     "plotWorkabilityId": "c0615a4d-faa4-4e17-8f2f-93ec25383f9b", \
        ///     "plotSurfaceArea": 2900, \
        ///     "plotNumber": "482", \
        ///     "plotRealEstateListNumber": "LN482", \
        ///     "plotCurrentCulture": "updatedCulture", \
        ///     "plotCurrentWorkability": "updatedWorkability" \
        /// }
        /// </remarks>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlotDto>> UpdatePlotAsync(PlotUpdateDto plotUpdate)
        {
            try
            {
                Plot existingPlot = await PlotRepository.GetPlotByIdAsync(plotUpdate.PlotId);

                if(existingPlot == null)
                {
                    return NotFound();
                }

                Plot plot = Mapper.Map<Plot>(plotUpdate);

                Validator.ValidateAndThrow(plot);

                Mapper.Map(plot, existingPlot);

                await PlotRepository.SaveChangesAsync();

                return Ok(Mapper.Map<PlotDto>(existingPlot));

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
        /// Deleting plot by given GUID.
        /// </summary>
        /// <param name="plotId"></param>
        /// <returns>Appropriate status code.</returns>
        [HttpDelete("{plotId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlotAsync(Guid plotId)
        {
            try
            {
                Plot plot = await PlotRepository.GetPlotByIdAsync(plotId);

                if(plot == null)
                {
                    return NotFound();
                }

                await PlotRepository.DeletePlotAsync(plotId);
                await PlotRepository.SaveChangesAsync();

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
        public IActionResult GetPlotOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
