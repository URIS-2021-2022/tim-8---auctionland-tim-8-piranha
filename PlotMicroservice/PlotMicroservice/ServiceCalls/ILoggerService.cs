using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.ServiceCalls
{
    public interface ILoggerService
    {
        bool LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod, Exception exception = null);
    }
}
