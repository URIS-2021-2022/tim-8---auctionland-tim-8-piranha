using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents Plot part.
    /// </summary>
    public class PlotPart
    {
        /// <summary>
        /// Plot part ID.
        /// </summary>
        public Guid PlotPartId { get; set; }

        /// <summary>
        /// Plot part number. Example: 97/1
        /// </summary>
        public string PlotPartNumber { get; set; }

        /// <summary>
        /// Plot ID.
        /// </summary>
        [ForeignKey("Plot")]
        public Guid PlotId { get; set; }

        /// <summary>
        /// Plot
        /// </summary>
        public Plot Plot { get; set; }

        /// <summary>
        /// Plot part class ID.
        /// </summary>
        [ForeignKey("PlotPartClass")]
        public Guid PlotPartClassId { get; set; }
        /// <summary>
        /// Plot part class.
        /// </summary>
        public PlotPartClass PlotPartClass { get; set; }

        /// <summary>
        /// Plot part protected zone ID.
        /// </summary>
        [ForeignKey("PlotPartProtectedZone")]
        public Guid PlotPartProtectedZoneId { get; set; }
        
        /// <summary>
        /// Plot part protected zone.
        /// </summary>
        public PlotPartProtectedZone PlotPartProtectedZone { get; set; }

        /// <summary>
        /// Plot part form of ownership ID.
        /// </summary>
        [ForeignKey("PlotPartFormOfOwnership")]
        public Guid PlotPartFormOfOwnershipId { get; set; }
        
        /// <summary>
        /// Plot part form of ownership.
        /// </summary>
        public PlotPartFormOfOwnership PlotPartFormOfOwnership { get; set; }

        /// <summary>
        /// Plot part surface area. Example: 2000
        /// </summary>
        public int PlotPartSurfaceArea { get; set; }

        /// <summary>
        /// Current plot part class.
        /// </summary>
        public string PlotPartCurrentClass { get; set; }

        /// <summary>
        /// Current plot part protected zone.
        /// </summary>
        public string PlotPartCurrentProtectedZone { get; set; }
    }
}
