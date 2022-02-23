using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.ServiceCalls
{
    public interface ILoggerService
    {
        Task<bool> LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod);
    }
}
