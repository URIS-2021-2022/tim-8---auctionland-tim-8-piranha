using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotWorkabilityModel;
using PlotMicroservice.Validators;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plot-workabilities")]
    [Produces("application/json", "application/xml")]
    public class PlotWorkabilityController : ControllerBase
    {
        private readonly IPlotWorkabilityRepository PlotWorkabilityRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotWorkabilityValidator Validator;

        public PlotWorkabilityController(IPlotWorkabilityRepository plotWorkabilityRepository, IMapper mapper, LinkGenerator linkGenerator, PlotWorkabilityValidator validator)
        {
            PlotWorkabilityRepository = plotWorkabilityRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotWorkabilityDto>> GetPlotWorkabilities(string workability)
        {
            List<PlotWorkability> plotWorkabilities = PlotWorkabilityRepository.GetPlotWorkabilities(workability);

            if(plotWorkabilities == null || plotWorkabilities.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotWorkabilityDto>>(plotWorkabilities));
        }

        [HttpGet("{plotWorkabilityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotWorkabilityDto> GetPlotWorkabilityId(Guid plotWorkabilityId)
        {
            PlotWorkability plotWorkability = PlotWorkabilityRepository.GetPlotWorkabilityById(plotWorkabilityId);

            if(plotWorkability == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotWorkabilityDto>(plotWorkability));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotWorkabilityConfirmationDto> CreatePlotWorkability([FromBody] PlotWorkabilityCreationDto plotWorkabilityCreation)
        {
            try
            {
                PlotWorkability plotWorkability = Mapper.Map<PlotWorkability>(plotWorkabilityCreation);

                Validator.ValidateAndThrow(plotWorkability);
                
                PlotWorkabilityConfirmation plotWorkabilityConfirmation = PlotWorkabilityRepository.CreatePlotWorkability(plotWorkability);
                PlotWorkabilityRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotWorkabilities", "PlotWorkability", new { plotWorkabilityId = plotWorkabilityConfirmation.PlotWorkabilityId });

                return Created(uri, Mapper.Map<PlotWorkabilityConfirmationDto>(plotWorkabilityConfirmation));

            } catch(ValidationException ve)
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
        public ActionResult<PlotWorkabilityDto> UpdatePlotWorkability(PlotWorkabilityUpdateDto plotWorkabilityUpdate)
        {
            try
            {
                PlotWorkability existingPlotWorkability = PlotWorkabilityRepository.GetPlotWorkabilityById(plotWorkabilityUpdate.PlotWorkabilityId);

                if (existingPlotWorkability == null)
                {
                    return NotFound();
                }

                PlotWorkability plotWorkability = Mapper.Map<PlotWorkability>(plotWorkabilityUpdate);

                Validator.ValidateAndThrow(plotWorkability);
                
                Mapper.Map(plotWorkability, existingPlotWorkability);

                PlotWorkabilityRepository.SaveChanges();

                return Ok(Mapper.Map<PlotWorkabilityDto>(existingPlotWorkability));

            } catch (ValidationException ve)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotWorkabilityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotWorkability(Guid plotWorkabilityId)
        {
            try
            {
                PlotWorkability plotWorkability = PlotWorkabilityRepository.GetPlotWorkabilityById(plotWorkabilityId);

                if (plotWorkability == null)
                {
                    return NotFound();
                }

                PlotWorkabilityRepository.DeletePlotWorkability(plotWorkabilityId);
                PlotWorkabilityRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotWorkabilityOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
