using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents plot workability.
    /// </summary>
    public class PlotWorkability
    {
        /// <summary>
        /// Plot workability ID.
        /// </summary>
        public Guid PlotWorkabilityId { get; set; }

        /// <summary>
        /// Plot workability. Example: Obradivo
        /// </summary>
        public string Workability { get; set; }
    }
}
