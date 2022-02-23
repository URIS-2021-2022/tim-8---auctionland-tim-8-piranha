using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Services
{
     public  interface ILoggerService
    {
        
           Task <bool> LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod, Exception exception = null);
        
    }
}
