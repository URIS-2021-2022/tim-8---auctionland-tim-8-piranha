using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartModel
{
    public class PlotPartDto
    {
        public string PlotPartNumber { get; set; }

        public int PlotPartSurfaceArea { get; set; }

        public Guid PlotId { get; set; }
    }
}
