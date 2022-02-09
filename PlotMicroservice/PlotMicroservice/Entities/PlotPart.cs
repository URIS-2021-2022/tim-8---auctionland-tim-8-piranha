using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPart
    {
        public Guid PlotPartId { get; set; }

        public string PlotPartNumber { get; set; }

        [ForeignKey("Plot")]
        public Guid PlotId { get; set; }

        public Plot Plot { get; set; }

        [ForeignKey("PlotPartClass")]
        public Guid PlotPartClassId { get; set; }
        public PlotPartClass PlotPartClass { get; set; }

        [ForeignKey("PlotPartProtectedZone")]
        public Guid PlotPartProtectedZoneId { get; set; }
        public PlotPartProtectedZone PlotPartProtectedZone { get; set; }

        [ForeignKey("PlotPartFormOfOwnership")]
        public Guid PlotPartFormOfOwnershipId { get; set; }
        public PlotPartFormOfOwnership PlotPartFormOfOwnership { get; set; }

        public int PlotPartSurfaceArea { get; set; }

        public string PlotPartCurrentClass { get; set; }

        public string PlotPartCurrentProtectedZone { get; set; }
    }
}
