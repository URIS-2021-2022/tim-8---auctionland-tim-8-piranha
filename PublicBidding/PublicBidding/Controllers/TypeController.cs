using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    /// Kontroler za tip javnog nadmetanja
    /// </summary>
    [Route("api/publicBidding/type")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository typeRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService logger;

        public TypeController(ITypeRepository typeRepository, IMapper mapper, ILoggerService logger)
        {
            this.typeRepository = typeRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Vraća sve tipove javnog nadmetanja
        /// </summary>
        /// <param name="typeName">Naziv tipa javnog nadmetanja</param>
        /// <returns>Lista tipova javnog nadmetanja</returns>
        /// <response code="200">Vraća listu tipova javnog nadmetanja</response>
        /// <response code="404">Nije pronađen ni jedan tip javnog nadmetanja</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Administrator, Superuser, Manager, OperaterNadmetanja")]
        public async Task<ActionResult<List<TypeDto>>> GetAllTypes()
        {
            var types = await typeRepository.GetAllTypes();

            if (types == null || types.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Type list is empty!", "PublicBidding microservice", "GetAllTypes");
                return NoContent();
            }

            await logger.LogMessage(LogLevel.Information, "Type list successfully returned!", "PublicBidding microservice", "GetAllTypes");
            return Ok(mapper.Map<List<TypeDto>>(types));
        }

        /// <summary>
        /// Vraća jedan tip javnog nadmetanja na osnovu prosleđenog ID-a
        /// </summary>
        /// <param name="typeId">ID tipa javnog nadmetanja</param>
        /// <returns>Tip javnog nadmetanja</returns>
        /// <response code="200">Vraća traženi tip javnog nadmetanja</response>
        /// <response code="404">Nije pronađen tip sa unetim ID-em</response>
        [HttpGet("{typeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator, Superuser, Manager, OperaterNadmetanja")]
        public async Task<ActionResult<TypeDto>> GetTypeById(Guid typeId)
        {
            var type = await typeRepository.GetTypeById(typeId);

            if (type == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Type not found!", "PublicBidding microservice", "GetTypeById");
                return NotFound();
            }

            await logger.LogMessage(LogLevel.Information, "Type found and successfully returned!", "PublicBidding microservice", "GetTypeById");
            return Ok(mapper.Map<TypeDto>(type));
        }

        /// <summary>
        /// Pregled zaglavlja odgovora
        /// </summary>
        /// <returns>Zaglavlje odgovora</returns>
        [HttpOptions]
        [AllowAnonymous]
        [Authorize(Roles = "Administrator, Superuser, Manager, OperaterNadmetanja")]
        public async Task<IActionResult> GetTypeOptions()
        {
            Response.Headers.Add("Allow", "GET");

            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "PublicBidding microservice", "GetPublicBiddingOptions");

            return Ok();
        }
    }
}
