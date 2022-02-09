using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartModel;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using PlotMicroservice.Validators;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plotParts")]
    [Produces("application/json", "application/xml")]
    public class PlotPartController : ControllerBase
    {
        private readonly IPlotPartRepository PlotPartRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotPartValidator Validator;

        public PlotPartController(IPlotPartRepository plotPartRepository, IMapper mapper, LinkGenerator linkGenerator, PlotPartValidator validator)
        {
            PlotPartRepository = plotPartRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotPartDto>> GetPlotParts(string plotPartClass, string plotProtectedZone)
        {
            List<PlotPart> plotParts = PlotPartRepository.GetPlotParts(plotPartClass, plotProtectedZone);
            
            if(plotParts == null || plotParts.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotPartDto>>(plotParts));
        }

        [HttpGet("{plotPartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotPartDto> GetPlotPartById(Guid plotPartId)
        {
            PlotPart plotPart = PlotPartRepository.GetPlotPartById(plotPartId);

            if(plotPart == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotPartDto>(plotPart));
        }

        [HttpGet("plot/{plotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotPartDto>> GetPlotPartsByPlotId(Guid plotId)
        {
            List<PlotPart> plotParts = PlotPartRepository.GetPlotPartsByPlotId(plotId);

            if(plotParts == null)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotPartDto>>(plotParts));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotPartConfirmationDto> CreatePlotPart([FromBody] PlotPartCreationDto plotPartCreation)
        {
            try
            {
                PlotPart plotPart = Mapper.Map<PlotPart>(plotPartCreation);

                Validator.ValidateAndThrow(plotPart);
                
                PlotPartConfirmation plotPartConfirmation = PlotPartRepository.CreatePlotPart(plotPart);

                PlotPartRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotParts", "PlotPart", new { plotPartId = plotPartConfirmation.PlotPartId });

                return Created(uri, Mapper.Map<PlotPartConfirmationDto>(plotPartConfirmation));

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
        public ActionResult<PlotPartDto> UpdatePlotPart(PlotPartUpdateDto plotPartUpdate)
        {
            try
            {
                PlotPart existingPlotPart = PlotPartRepository.GetPlotPartById(plotPartUpdate.PlotPartId);

                if(existingPlotPart == null)
                {
                    return NotFound();
                }

                PlotPart plotPart = Mapper.Map<PlotPart>(plotPartUpdate);

                Validator.ValidateAndThrow(plotPart);
                
                Mapper.Map(plotPart, existingPlotPart);

                PlotPartRepository.SaveChanges();

                return Ok(Mapper.Map<PlotPartDto>(existingPlotPart));

            } catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            } 
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotPartId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotPart(Guid plotPartId)
        {
            try
            {
                PlotPart plotPart = PlotPartRepository.GetPlotPartById(plotPartId);

                if (plotPart == null)
                {
                    return NotFound();
                }

                PlotPartRepository.DeletePlotPart(plotPartId);
                PlotPartRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotPartOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
