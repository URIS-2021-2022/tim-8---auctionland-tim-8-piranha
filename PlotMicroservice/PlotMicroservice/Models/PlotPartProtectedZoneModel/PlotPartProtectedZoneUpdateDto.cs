using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartProtectedZoneModel
{
    /// <summary>
    /// Update model for plot part protected zone.
    /// </summary>
    public class PlotPartProtectedZoneUpdateDto
    {
        /// <summary>
        /// Plot part protected zone ID.
        /// </summary>
        public Guid PlotPartProtectedZoneId { get; set; }

        /// <summary>
        /// Plot part protected zone.
        /// </summary>
        public string ProtectedZone { get; set; }
    }
}
