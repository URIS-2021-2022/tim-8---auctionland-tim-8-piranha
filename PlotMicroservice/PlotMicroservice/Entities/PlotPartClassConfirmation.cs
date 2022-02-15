using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot part class.
    /// </summary>
    public class PlotPartClassConfirmation
    {
        /// <summary>
        /// Plot part class ID.
        /// </summary>
        public Guid PlotPartClassId { get; set; }

        /// <summary>
        /// Plot part class. Example: IV
        /// </summary>
        public string Class { get; set; }
    }
}
