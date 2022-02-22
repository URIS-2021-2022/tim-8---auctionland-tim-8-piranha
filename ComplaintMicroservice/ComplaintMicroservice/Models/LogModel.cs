using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// Representation of Log.
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// Log level (ex. Warn, Debug, Error, Info)
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Message to log.
        /// </summary>
        public string LogMessage { get; set; }

        /// <summary>
        /// Name of microservice (ex. Plot microservice)
        /// </summary>
        public string MicroserviceName { get; set; }

        /// <summary>
        /// Microservice method (ex. GetPlotsAsync())
        /// </summary>
        public string MicroserviceMethod { get; set; }
    }
}
