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
using PlotMicroservice.ServiceCalls;
using Microsoft.Extensions.Logging;
using PlotMicroservice.Models;

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
        private readonly ILoggerService Logger;
        private readonly IServiceCall<BuyerDto> BuyerService;

        /// <summary>
        /// Plot constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        /// <param name="logger"></param>
        /// <param name="buyerService"></param>
        public PlotController(IPlotRepository plotRepository, IMapper mapper, LinkGenerator linkGenerator, PlotValidator validator, ILoggerService logger, IServiceCall<BuyerDto> buyerService)
        {
            PlotRepository = plotRepository;
            LinkGenerator = linkGenerator;
            Mapper = mapper;
            Validator = validator;
            Logger = logger;
            BuyerService = buyerService;
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
                await Logger.LogMessage(LogLevel.Warning, "Plot list is empty!", "Plot microservice", "GetPlotsAsync");
                return NoContent();
            }

            List<PlotDto> plotsDto = new List<PlotDto>();

            foreach(var plot in plots)
            {
                PlotDto plotDto = Mapper.Map<PlotDto>(plot);

                if(plot.BuyerId is not null)
                {
                    var buyerDto = await BuyerService.SendGetRequestAsync("");

                    if(buyerDto is not null)
                    {
                        plotDto.Buyer = buyerDto;
                    }
                }
                plotsDto.Add(plotDto);
            }

            await Logger.LogMessage(LogLevel.Information, "Plot list successfully returned!", "Plot microservice", "GetPlotsAsync");
            return Ok(plotsDto);
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
                await Logger.LogMessage(LogLevel.Warning, "Plot not found!", "Plot microservice", "GetPlotByIdAsync");
                return NotFound();
            }

            PlotDto plotDto = Mapper.Map<PlotDto>(plot);

            if(plot.BuyerId is not null)
            {
                var buyerDto = await BuyerService.SendGetRequestAsync("");

                if(buyerDto is not null)
                {
                    plotDto.Buyer = buyerDto;
                }
            }

            await Logger.LogMessage(LogLevel.Information, "Plot found and successfully returned!", "Plot microservice", "GetPlotByIdAsync");
            return Ok(plotDto);
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

                await Logger.LogMessage(LogLevel.Information, "Plot successfully created!", "Plot microservice", "CreatePlotAsync");
                return Created(uri, Mapper.Map<PlotConfirmationDto>(plotConfirmation));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot object failed!", "Plot microservice", "CreatePlotAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot object creation failed!", "Plot microservice", "CreatePlotAsync");
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
                    await Logger.LogMessage(LogLevel.Warning, "Plot object not found!", "Plot microservice", "UpdatePlotAsync");
                    return NotFound();
                }

                Plot plot = Mapper.Map<Plot>(plotUpdate);

                Validator.ValidateAndThrow(plot);

                Mapper.Map(plot, existingPlot);

                await PlotRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot object updated successfully!", "Plot microservice", "UpdatePlotAsync");
                return Ok(Mapper.Map<PlotDto>(existingPlot));

            } catch (ValidationException ve)
            {
                await Logger.LogMessage(LogLevel.Error, "Validation for plot object failed!", "Plot microservice", "UpdatePlotAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot object updating failed!", "Plot microservice", "UpdatePlotAsync");
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
                    await Logger.LogMessage(LogLevel.Warning, "Plot object not found!", "Plot microservice", "DeletePlotAsync");
                    return NotFound();
                }

                await PlotRepository.DeletePlotAsync(plotId);
                await PlotRepository.SaveChangesAsync();

                await Logger.LogMessage(LogLevel.Information, "Plot object deleted successfully!", "Plot microservice", "DeletePlotAsync");
                return NoContent();

            } catch(Exception ex)
            {
                await Logger.LogMessage(LogLevel.Error, "Plot object deletion failed!", "Plot microservice", "DeletePlotAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlotOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            await Logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Plot microservice", "GetPlotOptions");

            return Ok();
        }
    }
}
