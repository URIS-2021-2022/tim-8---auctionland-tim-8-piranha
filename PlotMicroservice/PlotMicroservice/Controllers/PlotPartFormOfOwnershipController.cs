using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartFormOfOwnershipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plotPartFormOfOwnerships")]
    [Produces("application/json", "application/xml")]
    public class PlotPartFormOfOwnershipController : ControllerBase
    {
        private readonly IPlotPartFormOfOwnershipRepository PlotPartFormOfOwnershipRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;

        public PlotPartFormOfOwnershipController(IPlotPartFormOfOwnershipRepository plotPartFormOfOwnershipRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            PlotPartFormOfOwnershipRepository = plotPartFormOfOwnershipRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotPartFormOfOwnershipDto>> GetPlotPartFormOfOwnerships(string formOfOwnership)
        {
            List<PlotPartFormOfOwnership> plotPartFormOfOwnerships = PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnerships(formOfOwnership);

            if(plotPartFormOfOwnerships == null || plotPartFormOfOwnerships.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotPartFormOfOwnershipDto>>(plotPartFormOfOwnerships));
        }

        [HttpGet("{plotPartFormOfOwnershipId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotPartFormOfOwnershipDto> GetPlotPartFormOfOwnershipById(Guid plotPartFormOfOwnershipId)
        {
            PlotPartFormOfOwnership plotPartFormOfOwnership = PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipById(plotPartFormOfOwnershipId);

            if(plotPartFormOfOwnership == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotPartFormOfOwnershipDto>(plotPartFormOfOwnership));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotPartFormOfOwnershipConfirmationDto> CreatPlotPartFormOfOwnership([FromBody] PlotPartFormOfOwnershipCreationDto plotPartFormOfOwnershipCreation)
        {
            try
            {
                PlotPartFormOfOwnership plotPartFormOfOwnership = Mapper.Map<PlotPartFormOfOwnership>(plotPartFormOfOwnershipCreation);
                PlotPartFormOfOwnershipConfirmation plotPartFormOfOwnershipConfirmation = PlotPartFormOfOwnershipRepository.CreatPlotPartFormOfOwnership(plotPartFormOfOwnership);
                PlotPartFormOfOwnershipRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotPartFormOfOwnerships", "PlotPartFormOfOwnership", new { plotPartFormOfOwnershipId = plotPartFormOfOwnershipConfirmation.PlotPartFormOfOwnershipId });

                return Created(uri, Mapper.Map<PlotPartFormOfOwnershipConfirmationDto>(plotPartFormOfOwnershipConfirmation));

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
        public ActionResult<PlotPartFormOfOwnershipDto> UpdatePlotPartFormOfOwnership(PlotPartFormOfOwnershipUpdateDto plotPartFormOfOwnershipUpdate)
        {
            try
            {
                PlotPartFormOfOwnership existingPlotPartFormOfOwnership = PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipById(plotPartFormOfOwnershipUpdate.PlotPartFormOfOwnershipId);

                if(existingPlotPartFormOfOwnership == null)
                {
                    return NotFound();
                }

                PlotPartFormOfOwnership plotPartFormOfOwnership = Mapper.Map<PlotPartFormOfOwnership>(plotPartFormOfOwnershipUpdate);
                Mapper.Map(plotPartFormOfOwnership, existingPlotPartFormOfOwnership);

                PlotPartFormOfOwnershipRepository.SaveChanges();

                return Ok(Mapper.Map<PlotPartFormOfOwnershipDto>(existingPlotPartFormOfOwnership));

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotPartFormOfOwnershipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotPartFormOfOwnership(Guid plotPartFormOfOwnershipId)
        {
            try
            {
                PlotPartFormOfOwnership plotPartFormOfOwnership = PlotPartFormOfOwnershipRepository.GetPlotPartFormOfOwnershipById(plotPartFormOfOwnershipId);

                if(plotPartFormOfOwnership == null)
                {
                    return NotFound();
                }

                PlotPartFormOfOwnershipRepository.DeletePlotPartFormOfOwnership(plotPartFormOfOwnershipId);
                PlotPartFormOfOwnershipRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotPartFormOfOwnershipOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
