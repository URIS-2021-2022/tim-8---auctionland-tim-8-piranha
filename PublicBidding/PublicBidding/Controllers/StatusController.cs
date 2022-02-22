using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PublicBidding.Data;
using PublicBidding.Models;
using PublicBidding.ServiceCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Controllers
{
    /// <summary>
    /// Kontroler za status javnog nadmetanja
    /// </summary>
    [Route("api/publicBidding/status")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository statusRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService logger;

        public StatusController(IStatusRepository statusRepository, IMapper mapper, ILoggerService logger)
        {
            this.statusRepository = statusRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve statuse javnog nadmetanja
        /// </summary>
        /// <returns>Lista mogućih statusa javnog nadmetanja</returns>
        /// <response code="200">Vraća listu mogućih statusa javnog nadmetanja</response>
        /// <response code="404">Nije pronađen ni jedan status javnog nadmetanja</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator, Superuser, Manager, OperaterNadmetanja")]
        public async Task<ActionResult<List<StatusDto>>> GetAllStatuses()
        {
            var statuses = await statusRepository.GetStatuses();

            if (statuses == null || statuses.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Status list is empty!", "PublicBidding microservice", "GetAllStatuses");
                return NoContent();
            }

            await logger.LogMessage(LogLevel.Information, "Status list successfully returned!", "PublicBidding microservice", "GetAllStatuses");
            return Ok(mapper.Map<List<StatusDto>>(statuses));
        }

        /// <summary>
        /// Vraća jedan status javnog nadmetanja na osnovu prosleđenog ID-a
        /// </summary>
        /// <param name="statusId">ID statusa</param>
        /// <returns>Status javnog nadmetanja</returns>
        /// <response code="200">Vraća traženi status javnog nadmetanja</response>
        /// <response code="404">Nije pronađen status sa unetim ID-em</response>
        [HttpGet("{statusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Manager, OperaterNadmetanja")]
        public async Task<ActionResult<StatusDto>> GetStatusById(Guid statusId)
        {
            var status = await statusRepository.GetStatusById(statusId);

            if (status == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Status not found!", "PublicBidding microservice", "GetStatusById");
                return NotFound();
            }

            await logger.LogMessage(LogLevel.Information, "Status found and successfully returned!", "PublicBidding microservice", "GetStatusById");
            return Ok(mapper.Map<StatusDto>(status));
        }

        /// <summary>
        /// Pregled zaglavlja odgovora
        /// </summary>
        /// <returns>Zaglavlje odgovora</returns>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Manager, OperaterNadmetanja")]
        public async Task<IActionResult> GetStatusOptions()
        {
            Response.Headers.Add("Allow", "GET");

            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "PublicBidding microservice", "GetPublicBiddingOptions");

            return Ok();
        }
    }
}
