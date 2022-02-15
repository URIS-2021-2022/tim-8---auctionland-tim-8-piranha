using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotWorkabilityModel
{
    /// <summary>
    /// Update model for plot workability.
    /// </summary>
    public class PlotWorkabilityUpdateDto
    {
        /// <summary>
        /// Plot workability ID.
        /// </summary>
        public Guid PlotWorkabilityId { get; set; }

        /// <summary>
        /// Plot workability.
        /// </summary>
        public string Workability { get; set; }
    }
}
