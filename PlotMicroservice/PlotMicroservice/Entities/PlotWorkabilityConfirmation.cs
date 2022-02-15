using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot workability.
    /// </summary>
    public class PlotWorkabilityConfirmation
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
