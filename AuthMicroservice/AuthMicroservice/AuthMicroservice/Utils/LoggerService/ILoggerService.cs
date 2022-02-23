namespace AuthMicroservice.Utils.LoggerService
{
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    /// <summary>
    /// Logger service interface.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Abstract method for logging a message.
        /// </summary>
        /// <param name="logLevel">Log level.</param>
        /// <param name="logMessage">Log message.</param>
        /// <param name="microserviceName">Microservice name.</param>
        /// <param name="microserviceMethod">Method from which is called.</param>
        /// <returns></returns>
        Task<bool> LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod);
    }
}
