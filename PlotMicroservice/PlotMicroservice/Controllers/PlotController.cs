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
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plots")]
    [Produces("application/json", "application/xml")]
    public class PlotController : ControllerBase
    {
        private readonly IPlotRepository PlotRepository;
        private readonly LinkGenerator LinkGenerator;
        private readonly IMapper Mapper;

        public PlotController(IPlotRepository plotRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            PlotRepository = plotRepository;
            LinkGenerator = linkGenerator;
            Mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotDto>> GetPlots(string culture, string workability)
        {
            List<Plot> plots = PlotRepository.GetPlots(culture, workability);

            if(plots == null || plots.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotDto>>(plots));
        }

        [HttpGet("{plotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotDto> GetPlotById(Guid plotId)
        {
            Plot plot = PlotRepository.GetPlotById(plotId);

            if(plot == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotDto>(plot));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotConfirmationDto> CreatePlot([FromBody] PlotCreationDto plotCreation)
        {
            try
            {
                Plot plot = Mapper.Map<Plot>(plotCreation);
                PlotConfirmation plotConfirmation = PlotRepository.CreatePlot(plot);
                PlotRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlots", "Plot", new { plotId = plotConfirmation.PlotId });

                return Created(uri, Mapper.Map<PlotConfirmationDto>(plotConfirmation));

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotDto> UpdatePlot(PlotUpdateDto plotUpdate)
        {
            try
            {
                Plot existingPlot = PlotRepository.GetPlotById(plotUpdate.PlotId);

                if(existingPlot == null)
                {
                    return NotFound();
                }

                Plot plot = Mapper.Map<Plot>(plotUpdate);
                Mapper.Map(plot, existingPlot);

                PlotRepository.SaveChanges();

                return Ok(Mapper.Map<PlotDto>(existingPlot));

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlot(Guid plotId)
        {
            try
            {
                Plot plot = PlotRepository.GetPlotById(plotId);

                if(plot == null)
                {
                    return NotFound();
                }

                PlotRepository.DeletePlot(plotId);
                PlotRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
