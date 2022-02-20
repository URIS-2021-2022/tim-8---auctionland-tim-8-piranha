using LoggerService.Data;
using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggerService.Controllers
{
    [ApiController]
    [Route("api/logger-service")]
    public class LoggerManagerController : ControllerBase
    {
        private readonly ILoggerManagerRepository Logger;

        public LoggerManagerController(ILoggerManagerRepository logger)
        {
            Logger = logger;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LogMessage([FromBody] LogModel logModel)
        {
            try
            {
                string logMessage = "Log level = " + logModel.LogLevel 
                                    + " / Log message = " + logModel.LogMessage 
                                    + " / Microservice name = " + logModel.MicroserviceName 
                                    + " / Microservice method name = " + logModel.MicroserviceMethod + "\n\n";

                if (logModel.LogLevel == LogLevel.Information)
                {
                    Logger.LogInfo(logMessage);
                }
                else if (logModel.LogLevel == LogLevel.Warning)
                {
                    Logger.LogWarn(logMessage);
                } else if (logModel.LogLevel == LogLevel.Error)
                {
                    Logger.LogError(logMessage);
                } else if (logModel.LogLevel == LogLevel.Debug)
                {
                    Logger.LogDebug(logMessage);
                }


                return Ok("Logger service logged message successfully!");

            } catch(Exception)
            {
                Logger.LogError(new System.Diagnostics.StackTrace().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Some errors occured while trying to log message (internal server error)!");
            }
        }
    }
}
