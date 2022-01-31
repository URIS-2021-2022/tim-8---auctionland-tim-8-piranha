using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartClassModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Controllers
{
    [ApiController]
    [Route("api/plotPartClasses")]
    [Produces("application/json", "application/xml")]
    public class PlotPartClassController : ControllerBase
    {
        private readonly IPlotPartClassRepository PlotPartClassRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;

        public PlotPartClassController(IPlotPartClassRepository plotPartClassRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            PlotPartClassRepository = plotPartClassRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlotPartClassDto>> GetPlotPartClasses(string plotPartClass)
        {
            List<PlotPartClass> plotPartClasses = PlotPartClassRepository.GetPlotPartClasses(plotPartClass);

            if (plotPartClasses == null || plotPartClasses.Count == 0)
            {
                return NoContent();
            }

            return Ok(Mapper.Map<List<PlotPartClassDto>>(plotPartClasses));
        }

        [HttpGet("{plotPartClassId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlotPartClassDto> GetPlotPartClassById(Guid plotPartClassId)
        {
            PlotPartClass plotPartClass = PlotPartClassRepository.GetPlotPartClassById(plotPartClassId);

            if (plotPartClass == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PlotPartClassDto>(plotPartClass));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotPartClassConfirmationDto> CreatePlotPartClass([FromBody] PlotPartClassCreationDto plotPartClassCreation)
        {
            try
            {
                PlotPartClass plotPartClass = Mapper.Map<PlotPartClass>(plotPartClassCreation);
                PlotPartClassConfirmation plotPartClassConfirmation = PlotPartClassRepository.CreatePlotPartClass(plotPartClass);
                PlotPartClassRepository.SaveChanges();

                string uri = LinkGenerator.GetPathByAction("GetPlotPartClasses", "PlotPartClass", new { plotPartClassId = plotPartClassConfirmation.PlotPartClassId });

                return Created(uri, Mapper.Map<PlotPartClassConfirmationDto>(plotPartClassConfirmation));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlotPartClassDto> UpdatePlotPartClass(PlotPartClassUpdateDto plotPartClassUpdate)
        {
            try
            {
                PlotPartClass existingPlotPartClass = PlotPartClassRepository.GetPlotPartClassById(plotPartClassUpdate.PlotPartClassId);

                if (existingPlotPartClass == null)
                {
                    return NotFound();
                }

                PlotPartClass plotPartClass = Mapper.Map<PlotPartClass>(plotPartClassUpdate);
                Mapper.Map(plotPartClass, existingPlotPartClass);

                PlotPartClassRepository.SaveChanges();

                return Ok(Mapper.Map<PlotPartClassDto>(existingPlotPartClass));

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{plotPartClassId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePlotPartClass(Guid plotPartClassId)
        {
            try
            {
                PlotPartClass plotPartClass = PlotPartClassRepository.GetPlotPartClassById(plotPartClassId);

                if(plotPartClass == null)
                {
                    return NotFound();
                }

                PlotPartClassRepository.DeletePlotPartClass(plotPartClassId);
                PlotPartClassRepository.SaveChanges();

                return NoContent();

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotPartClassOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
