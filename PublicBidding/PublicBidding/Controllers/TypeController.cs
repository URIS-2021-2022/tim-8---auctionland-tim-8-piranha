using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicBidding.Data;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Controllers
{
    /// <summary>
    /// Kontroler za tip javnog nadmetanja
    /// </summary>
    [Route("api/publicBiddingType")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository typeRepository;
        private readonly IMapper mapper;

        public TypeController(ITypeRepository typeRepository, IMapper mapper)
        {
            typeRepository = typeRepository;
            mapper = mapper;
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
        public async Task<ActionResult<List<TypeDto>>> GetAllTypes()
        {
            var types = await typeRepository.GetAllTypes();

            if (types == null || types.Count == 0)
            {
                return NoContent();
            }

            await _loggerService.Log(LogLevel.Information, "GetAllStatus", "Lista statusa javnog nadmetanja je uspešno vraćena.");

            return Ok(_mapper.Map<List<StatusDto>>(statusi));
        }
    }
}
