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
    /// <summary>
    /// Plot part controller. Gives access to fields and methods of plot part.
    /// Produces JSON and XML objects as response to a request.
    /// </summary>
    [ApiController]
    [Route("api/plot-parts")]
    [Produces("application/json", "application/xml")]
    public class PlotPartController : ControllerBase
    {
        private readonly IPlotPartRepository PlotPartRepository;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;
        private readonly PlotPartValidator Validator;

        /// <summary>
        /// Plot part constructor.
        /// Initializes properties.
        /// </summary>
        /// <param name="plotPartRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="validator"></param>
        public PlotPartController(IPlotPartRepository plotPartRepository, IMapper mapper, LinkGenerator linkGenerator, PlotPartValidator validator)
        {
            PlotPartRepository = plotPartRepository;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
            Validator = validator;
        }

        /// <summary>
        /// Getting all instances of plot parts for given filter/s.
        /// </summary>
        /// <param name="plotPartClass">Plot part class (ex. III)</param>
        /// <param name="plotProtectedZone">Plot part protected zone (ex. 4)</param>
        /// <returns>List of plot parts.</returns>
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

        /// <summary>
        /// Getting plot part by given GUID of plot part as parameter.
        /// </summary>
        /// <param name="plotPartId"></param>
        /// <returns>Single plot part.</returns>
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

        /// <summary>
        /// Getting plot parts by given GUID of plot as parameter.
        /// </summary>
        /// <param name="plotId"></param>
        /// <returns>List of plot parts.</returns>
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

        /// <summary>
        /// Creating new plot part.
        /// </summary>
        /// <param name="plotPartCreation"></param>
        /// <returns>Confirmation of created plot part.</returns>
        /// <remarks>
        /// Example of POST request \
        /// POST /api/plot-parts \
        /// {   \
        ///     "plotPartNumber": "97/1", \
        ///     "plotId": "5f37ba98-ca19-4c9e-8914-708e38bba8bf", \
        ///     "plotPartClassId": "3a3e6366-3a20-4d3b-ae15-be85ba277683", \
        ///     "plotPartProtectedZoneId": "4debaa6a-1a2f-43e0-bb82-1b7ca1824318", \
        ///     "plotPartFormOfOwnershipId": "07af89f2-feee-4680-b489-9d0e31699588", \
        ///     "plotPartSurfaceArea": 100, \
        ///     "plotPartCurrentClass": "", \
        ///     "plotPartCurrentProtectedZone": "" \
        /// }
        /// </remarks>
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

        /// <summary>
        /// Updating existing plot part by given GUID.
        /// </summary>
        /// <param name="plotPartUpdate"></param>
        /// <returns>Updated plot part.</returns>
        /// <remarks> 
        /// Example of PUT request \
        /// PUT /api/plot-parts \
        /// {   \
        ///     "plotPartId": "a11d93e1-5a89-4821-5426-08d9eaeb3bdb", \
        ///     "plotPartNumber": "97/1", \
        ///     "plotId": "5f37ba98-ca19-4c9e-8914-708e38bba8bf", \
        ///     "plotPartClassId": "3a3e6366-3a20-4d3b-ae15-be85ba277683", \
        ///     "plotPartProtectedZoneId": "4debaa6a-1a2f-43e0-bb82-1b7ca1824318", \
        ///     "plotPartFormOfOwnershipId": "07af89f2-feee-4680-b489-9d0e31699588", \
        ///     "plotPartSurfaceArea": 900 , \
        ///     "plotPartCurrentClass": "Updated class", \
        ///     "plotPartCurrentProtectedZone": "Updated protected zone" \
        /// }
        /// </remarks>
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

        /// <summary>
        ///  Deleting plot part by given GUID.
        /// </summary>
        /// <param name="plotPartId"></param>
        /// <returns>Appropriate status code.</returns>
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

        /// <summary>
        /// Gives an overview of response header.
        /// </summary>
        /// <returns>Response header.</returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPlotPartOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
