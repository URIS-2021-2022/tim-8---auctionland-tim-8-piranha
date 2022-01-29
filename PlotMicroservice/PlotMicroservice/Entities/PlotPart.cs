using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPart
    {
        public Guid PlotPartId { get; set; }

        public string PlotPartName { get; set; }

        public PlotPartClass PlotPartClass { get; set; }

        public PlotPartProtectedZone PlotPartProtectedZone { get; set; }

        public PlotPartFormOfOwnership PlotPartFormOfOwnership { get; set; }

        public string PlotPartSurfaceArea { get; set; }

        public string PlotPartCurrentClass { get; set; }

        public string PlotPartCurrentProtectedZone { get; set; }
    }
}
