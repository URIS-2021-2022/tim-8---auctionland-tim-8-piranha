
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    /// <summary>
    /// Reprezentacija loga
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// Log level (ex. Warn, Debug, Error, Info)
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Poruka za log
        /// </summary>
        public string? LogMessage { get; set; }

        /// <summary>
        /// Ime mikroservisa
        /// </summary>
        public string? MicroserviceName { get; set; }

        /// <summary>
        /// Metoda mikroservisa
        /// </summary>
        public string? MicroserviceMethod { get; set; }
    }
}
