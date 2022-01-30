using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotWorkabilityModel
{
    public class PlotWorkabilityUpdateDto
    {
        public Guid PlotWorkabilityId { get; set; }

        public string Workability { get; set; }
    }
}
