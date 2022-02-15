using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot part protected zone.
    /// </summary>
    public class PlotPartProtectedZoneConfirmation
    {
        /// <summary>
        /// Plot part protected zone ID.
        /// </summary>
        public Guid PlotPartProtectedZoneId { get; set; }

        /// <summary>
        /// Plot part protected zone. Example: 1
        /// </summary>
        public string ProtectedZone { get; set; }
    }
}
