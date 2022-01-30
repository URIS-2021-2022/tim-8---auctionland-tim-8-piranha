using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotWorkabilityConfirmation
    {
        public Guid PlotWorkabilityId { get; set; }

        public string Workability { get; set; }
    }
}
