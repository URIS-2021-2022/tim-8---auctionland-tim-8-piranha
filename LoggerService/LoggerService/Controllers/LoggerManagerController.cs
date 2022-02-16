using LoggerService.Data;
using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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
                string logMessage = logModel.LogLevel + " / " + logModel.LogMessage + " / "
                                    + logModel.MicroserviceName + " / "
                                    + logModel.MicroserviceMethod;

                if (logModel.LogLevel == LogLevel.Info)
                {
                    Logger.LogInfo(logMessage);
                }
                else if (logModel.LogLevel == LogLevel.Warn)
                {
                    Logger.LogWarn(logMessage);
                } else if (logModel.LogLevel == LogLevel.Error)
                {
                    Logger.LogError(logMessage);
                } else if (logModel.LogLevel == LogLevel.Debug)
                {
                    Logger.LogDebug(logMessage);
                }

                return Ok(logMessage);

            } catch(Exception)
            {
                Logger.LogError(logModel.Exception.ToString() + "\n" + new System.Diagnostics.StackTrace().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Some errors occured while trying to log message (internal server error)!");
            }
        }

    }
}
