using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPart
    {
        private Guid PlotPartId { get; set; }

        private string PlotPartName { get; set; }

        private PlotPartClass PlotPartClass { get; set; }

        private PlotPartProtectedZone PlotPartProtectedZone { get; set; }

        private PlotPartFormOfOwnership PlotPartFormOfOwnership { get; set; }

        private string PlotPartSurfaceArea { get; set; }

        private string PlotPartCurrentClass { get; set; }

        private string PlotPartCurrentProtectedZone { get; set; }
    }
}
