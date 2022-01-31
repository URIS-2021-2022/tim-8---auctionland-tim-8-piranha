using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartProtectedZoneModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plotPartProtectedZones")]
    [Produces("application/json", "application/xml")]
    public class PlotPartProtecedZoneController : ControllerBase
    {
        private readonly IPlotPartProtectedZoneRepository PlotPartProtectedZoneRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;

        public PlotPartProtecedZoneController(IPlotPartProtectedZoneRepository plotPartProtectedZoneRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            PlotPartProtectedZoneRepository = plotPartProtectedZoneRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotPartProtectedZoneDto>> GetPlotPartProtectedZones(string protectedZone)
        {
            List<PlotPartProtectedZone> plotPartProtectedZones = PlotPartProtectedZoneRepository.GetPlotPartProtectedZones(protectedZone);

            if(plotPartProtectedZones == null || plotPartProtectedZones.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotPartProtectedZoneDto>>(plotPartProtectedZones));
        }

        [HttpGet("{plotPartProtectedZoneId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotPartProtectedZoneDto> GetPlotPartProtectedZoneById(Guid plotPartProtectedZoneId)
        {
            PlotPartProtectedZone plotPartProtectedZone = PlotPartProtectedZoneRepository.GetPlotPartProtectedZoneById(plotPartProtectedZoneId);

            if(plotPartProtectedZone == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotPartProtectedZoneDto>(plotPartProtectedZone));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotPartProtectedZoneConfirmationDto> CreatePlotPartProtectedZone([FromBody] PlotPartProtectedZoneCreationDto plotPartProtectedZoneCreation)
        {
            try
            {
                PlotPartProtectedZone plotPartProtectedZone = Mapper.Map<PlotPartProtectedZone>(plotPartProtectedZoneCreation);
                PlotPartProtectedZoneConfirmation plotPartProtectedZoneConfirmation = PlotPartProtectedZoneRepository.CreatePlotPartProtectedZone(plotPartProtectedZone);
                PlotPartProtectedZoneRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotPartProtectedZones", "PlotPartProtecedZone", new { plotPartProtectedZoneId = plotPartProtectedZoneConfirmation.PlotPartProtectedZoneId});

                return Created(uri, Mapper.Map<PlotPartProtectedZoneConfirmationDto>(plotPartProtectedZoneConfirmation));

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
        public ActionResult<PlotPartProtectedZoneDto> UpdatePlotPartProtectedZone(PlotPartProtectedZoneUpdateDto plotPartProtectedZoneUpdate)
        {
            try
            {
                PlotPartProtectedZone existingPlotPartProtectedZone = PlotPartProtectedZoneRepository.GetPlotPartProtectedZoneById(plotPartProtectedZoneUpdate.PlotPartProtectedZoneId);

                if(existingPlotPartProtectedZone == null)
                {
                    return NotFound();
                }

                PlotPartProtectedZone plotPartProtectedZone = Mapper.Map<PlotPartProtectedZone>(plotPartProtectedZoneUpdate);
                Mapper.Map(plotPartProtectedZone, existingPlotPartProtectedZone);

                PlotPartProtectedZoneRepository.SaveChanges();

                return Ok(Mapper.Map<PlotPartProtectedZoneDto>(existingPlotPartProtectedZone));

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotPartProtectedZoneId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotPartProtectedZone(Guid plotPartProtectedZoneId)
        {
            try
            {
                PlotPartProtectedZone plotPartProtectedZone = PlotPartProtectedZoneRepository.GetPlotPartProtectedZoneById(plotPartProtectedZoneId);

                if (plotPartProtectedZone == null)
                {
                    return NotFound();
                }

                PlotPartProtectedZoneRepository.DeletePlotPartProtectedZone(plotPartProtectedZoneId);
                PlotPartProtectedZoneRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotPartProtectedZoneOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
