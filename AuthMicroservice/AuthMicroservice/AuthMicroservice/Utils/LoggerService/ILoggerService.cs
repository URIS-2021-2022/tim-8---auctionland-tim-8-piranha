namespace AuthMicroservice.Utils.LoggerService
{
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public interface ILoggerService
    {
        Task<bool> LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod);
    }
}
