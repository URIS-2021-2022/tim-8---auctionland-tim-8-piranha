using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotModel
{
    /// <summary>
    /// Confirmation DTO for plot.
    /// </summary>
    public class PlotConfirmationDto
    {
        /// <summary>
        /// Plot number.
        /// </summary>
        public string PlotNumber { get; set; }

        /// <summary>
        /// Plot surface area.
        /// </summary>
        public int PlotSurfaceArea { get; set; }
    }
}
