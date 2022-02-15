using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot part.
    /// </summary>
    public class PlotPartConfirmation
    {
        /// <summary>
        /// Plot part ID.
        /// </summary>
        public Guid PlotPartId { get; set; }

        /// <summary>
        /// Plot part number. Example: 112/3
        /// </summary>
        public string PlotPartNumber { get; set; }

        /// <summary>
        /// Plot ID.
        /// </summary>
        public Guid PlotId { get; set; }

        /// <summary>
        /// Plot part class ID.
        /// </summary>
        public Guid PlotPartClassId { get; set; }

        /// <summary>
        /// Plot part protected zone ID.
        /// </summary>
        public Guid PlotPartProtectedZoneId { get; set; }
        
        /// <summary>
        /// Plot part form of ownership ID.
        /// </summary>
        public Guid PlotPartFormOfOwnershipId { get; set; }
        
        /// <summary>
        /// Plot part surface area. Example: 3500
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
