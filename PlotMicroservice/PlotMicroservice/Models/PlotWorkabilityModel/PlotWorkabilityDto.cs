using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotWorkabilityModel
{
    /// <summary>
    /// DTO for plot workability.
    /// </summary>
    public class PlotWorkabilityDto
    {
        /// <summary>
        /// Plot workability. Example: Obradivo
        /// </summary>
        public string Workability { get; set; }
    }
}
