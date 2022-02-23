using Microsoft.Extensions.Logging;

namespace AuthMicroservice.Utils.LoggerService
{
    /// <summary>
    /// Log model representation.
    /// </summary>
    internal class LogModel
    {
        /// <summary>
        /// Log level (ex. Warn, Debug, Error, Info).
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Log message.
        /// </summary>
        public string LogMessage { get; set; }

        /// <summary>
        /// Name of the microservice from which the log is called.
        /// </summary>
        public string MicroserviceName { get; set; }

        /// <summary>
        /// Method from which the logger was called.
        /// </summary>
        public string MicroserviceMethod { get; set; }
    }
}