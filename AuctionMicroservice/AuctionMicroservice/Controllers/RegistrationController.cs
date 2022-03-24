using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Controllers
{
    [ApiController]
    [Route("api/registration")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    public class RegistrationController : ControllerBase
    {

        private readonly IUserRegistrationRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;


        public RegistrationController(IMapper mapper, IUserRegistrationRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{Email}")]
        public ActionResult<RegistrationDto> GetRegistrationByEmail(string email)
        {
            var registration = repository.GetRegistrationByEmail(email);

            if(registration == null)
            {
                return NotFound();
            }

            RegistrationDto registrationDto = mapper.Map<RegistrationDto>(registration);

            return Ok(registrationDto);

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateUser(RegistrationDto registration)
        {
            try
            {
                Registration registrationEntity = mapper.Map<Registration>(registration);

                repository.CreateUser(registrationEntity);
                repository.SaveChanges();


                return Ok();
            }
            catch (Exception e)
            {
                //await logger.LogMessage(LogLevel.Information, "Internal server error", "Auction microservice", "PostAuctionAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                
            }
        }

    }
}
