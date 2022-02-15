using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartProtectedZoneModel
{
    /// <summary>
    /// DTO for plot part protected zone.
    /// </summary>
    public class PlotPartProtectedZoneDto
    {
        /// <summary>
        /// Plot part protected zone. Example: 3
        /// </summary>
        public string ProtectedZone { get; set; }
    }
}
