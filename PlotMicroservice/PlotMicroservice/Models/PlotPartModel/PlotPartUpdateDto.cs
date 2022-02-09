using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartModel
{
    public class PlotPartUpdateDto
    {
        public Guid PlotPartId { get; set; }

        public string PlotPartNumber { get; set; }

        public Guid PlotId { get; set; }

        public Guid PlotPartClassId { get; set; }

        public Guid PlotPartProtectedZoneId { get; set; }

        public Guid PlotPartFormOfOwnershipId { get; set; }

        public int PlotPartSurfaceArea { get; set; }

        public string PlotPartCurrentClass { get; set; }

        public string PlotPartCurrentProtectedZone { get; set; }
    }
}
