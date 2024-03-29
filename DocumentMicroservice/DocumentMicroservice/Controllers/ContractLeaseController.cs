﻿using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using DocumentMicroservice.Models.ContractLease;
using DocumentMicroservice.ServiceCalls;
using DocumentMicroservice.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Controllers
{
    [ApiController]
    [Route("api/contractLease")]
    [Produces("application/json", "application/xml")]
    public class ContractLeaseController : ControllerBase
    {
        private readonly IContractLeaseRepository contractLeaseRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ContractLeaseValidators validator;
        private readonly ILoggerService logger;
        private readonly IServiceCall<BuyerDto> buyerService;
        private readonly IServiceCall<PersonDto> personService;
        private readonly IServiceCall<PlotDto> plotService;
        private readonly IConfiguration configuration;

        public ContractLeaseController(IContractLeaseRepository contractLeaseRepository, LinkGenerator linkGenerator, IMapper mapper, ContractLeaseValidators validator, ILoggerService logger, IServiceCall<BuyerDto> buyerService, IServiceCall<PersonDto> personService, IServiceCall<PlotDto> plotService, IConfiguration configuration)
        {
            this.contractLeaseRepository = contractLeaseRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.validator = validator;
            this.logger = logger;
            this.buyerService = buyerService;
            this.personService = personService;
            this.plotService = plotService;
            this.configuration = configuration;
        }

     
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ContractLeaseDto>>> GetContractLeaseAsync(string serialNumber)
        {
            List<ContractLease> contractLeaseList = await contractLeaseRepository.GetContractLeaseAsync(serialNumber);

            if (contractLeaseList == null || contractLeaseList.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "ContractLease list is empty!", "Document microservice", "GetContractLeaseAsync");
                return NoContent();
            }

            List<ContractLeaseDto> contractLeasesDto = new List<ContractLeaseDto>();

            foreach (var cl in contractLeaseList)
            {
                ContractLeaseDto contractLeasestDto = mapper.Map<ContractLeaseDto>(cl);

                if (cl.buyerId is not null && cl.personId is not null && cl.plotId is not null)
                {
                    var buyerDto = await buyerService.SendGetRequestAsync(configuration["Services:BuyerServiceCallMock"]);
                    var personDto = await personService.SendGetRequestAsync(configuration["Services:PersonServiceCallMock"]);
                    var plotDto = await plotService.SendGetRequestAsync(configuration["Services:PlotServiceCallMock"]);

                    if (buyerDto is not null && personDto is not null && plotDto is not null)
                    {
                        contractLeasestDto.buyer = buyerDto;
                        contractLeasestDto.person = personDto;
                        contractLeasestDto.plot = plotDto;
                    }
                }
                contractLeasesDto.Add(contractLeasestDto);
            }

            await logger.LogMessage(LogLevel.Information, "Contract lease list successfully returned!", "Document microservice", "GetContractLeaseAsync");
            return Ok(contractLeasesDto);
        }


        [HttpGet("{contractLeaseID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContractLeaseDto>> GetContractLeaseByIdAsync(Guid contractLeaseID)
        {
            ContractLease contractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);

            if (contractLease == null)
            {
                await logger.LogMessage(LogLevel.Warning, "Contract lease not found!", "Document  microservice", "GetContractLeaseByIdAsync");
                return NotFound();
            }
            ContractLeaseDto contractLeaseDto = mapper.Map<ContractLeaseDto>(contractLease);

            if (contractLease.buyerId is not null && contractLease.personId is not null && contractLease.plotId is not null)
            {
                var buyerDto = await buyerService.SendGetRequestAsync(configuration["Services:BuyerServiceCallMock"]);
                var personDto = await personService.SendGetRequestAsync(configuration["Services:PersonServiceCallMock"]);
                var plotDto = await plotService.SendGetRequestAsync(configuration["Services:PlotServiceCallMock"]);

                if (buyerDto is not null && personDto is not null && plotDto is not null)
                {
                    contractLeaseDto.buyer = buyerDto;
                    contractLeaseDto.person = personDto;
                    contractLeaseDto.plot = plotDto;
                }
            }

            await logger.LogMessage(LogLevel.Information, "Contract lease found and successfully returned!", "Document microservice", "GetContractLeaseByIdAsync");
            return Ok(contractLeaseDto);
        }


      
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContractLeaseConfirmationDto>> CreateContractLeaseAsync([FromBody] ContractLeaseCreationDto ContractLeaseCreation)
        {
            try
            {
                ContractLease contractLease = mapper.Map<ContractLease>(ContractLeaseCreation);
                validator.ValidateAndThrow(contractLease);
                ContractLeaseConfirmation confirmation = await contractLeaseRepository.CreateContractLeaseAsync(contractLease);
                await contractLeaseRepository.SaveChangesAsync();


                string uri = linkGenerator.GetPathByAction("GetContractLeaseById", "ContractLease", new { contractLeaseID = confirmation.contractLeaseID });
                await logger.LogMessage(LogLevel.Information, "Contract Lease  protected zone successfully created!", "Document microservice", "CreateContractLeaseAsync");

                return Created(uri, mapper.Map<ContractLeaseConfirmationDto>(confirmation));
            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for contract lease object failed!", "Document microservice", "CreateContractLeaseAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contract lease object creation failed!", "Document microservice", "CreateContractLeaseAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContractLeaseDto>> UpdateContractLeaseAsync(ContractLeaseUpdateDto contractLease)
        {
            try
            {
                ContractLease existingContractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLease.contractLeaseID);

                if (existingContractLease == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contract Lease object not found!", "Document microservice", "UpdateContractLeaseAsync");
                    return NotFound();
                }

                ContractLease cl = mapper.Map<ContractLease>(contractLease);

                validator.ValidateAndThrow(cl);

                mapper.Map(cl, existingContractLease);
                await contractLeaseRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Contract Lease object updated successfully!", "Document microservice", "UpdateContractLeaseAsync");

                return Ok(mapper.Map<ContractLeaseDto>(existingContractLease));

            }
            catch (ValidationException ve)
            {
                await logger.LogMessage(LogLevel.Error, "Validation for contract lease object failed!", "Document microservice", "UpdateContractLeaseAsync");
                return StatusCode(StatusCodes.Status400BadRequest, ve.Errors);
            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contract lease object updating failed!", "Document microservice", "UpdateContractLeaseAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [HttpDelete("{contractLeaseID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContractLeaseAsync(Guid contractLeaseID)
        {
            try
            {
                ContractLease contractLease = await contractLeaseRepository.GetContractLeaseByIdAsync(contractLeaseID);
                if (contractLease == null)
                {
                    await logger.LogMessage(LogLevel.Warning, "Contract lease object not found!", "Document microservice", "DeleteContractLeaseAsync");
                    return NotFound();
                }

                await contractLeaseRepository.DeleteContractLeaseAsync(contractLeaseID);
                await contractLeaseRepository.SaveChangesAsync();

                await logger.LogMessage(LogLevel.Information, "Contract lease object deleted successfully!", "Document microservice", "DeleteContractLeaseAsync");

                return NoContent(); // Successful deletion

            }
            catch (Exception ex)
            {
                await logger.LogMessage(LogLevel.Error, "Contract lease object deletion failed!", "Document microservice", "DeleteContractLeaseAsync");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

      
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetContractLeaseOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            await logger.LogMessage(LogLevel.Information, "Options request returned successfully!", "Document microservice", "GetDocumentOptions");

            return Ok();
        }
    }
}