using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotModel
{
    /// <summary>
    /// DTO for plot.
    /// </summary>
    public class PlotDto
    {
        /// <summary>
        /// Plot number. Example: 112
        /// </summary>
        public string PlotNumber { get; set; }

        /// <summary>
        /// Plot surface area. Example: 2050
        /// </summary>
        public int PlotSurfaceArea { get; set; }

        /// <summary>
        /// Plot real estate number. Example: LN505
        /// </summary>
        public string PlotRealEstateListNumber { get; set; }
    }
}
