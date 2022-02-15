using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartModel
{
    /// <summary>
    /// DTO for plot part.
    /// </summary>
    public class PlotPartDto
    {
        /// <summary>
        /// Plot part number. Example: 112/2
        /// </summary>
        public string PlotPartNumber { get; set; }

        /// <summary>
        /// Plot part surface area. Example: 2500
        /// </summary>
        public int PlotPartSurfaceArea { get; set; }

        /// <summary>
        /// Plot ID.
        /// </summary>
        public Guid PlotId { get; set; }
    }
}
