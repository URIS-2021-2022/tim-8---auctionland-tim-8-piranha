using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartModel
{
    /// <summary>
    /// Confirmation DTO for plot part.
    /// </summary>
    public class PlotPartConfirmationDto
    {
        /// <summary>
        /// Plot part number.
        /// </summary>
        public string PlotPartNumber { get; set; }

        /// <summary>
        /// Plot part surface area.
        /// </summary>
        public int PlotPartSurfaceArea { get; set; }
    }
}
