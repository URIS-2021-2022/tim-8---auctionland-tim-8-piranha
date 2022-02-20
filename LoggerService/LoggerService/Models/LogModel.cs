using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Models
{
    public class LogModel
    {
        public LogLevel LogLevel { get; set; }

        public string LogMessage { get; set; }

        public string MicroserviceName { get; set; }

        public string MicroserviceMethod { get; set; }
    }
}
