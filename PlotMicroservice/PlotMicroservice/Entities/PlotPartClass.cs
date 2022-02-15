using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents plot part class.
    /// </summary>
    public class PlotPartClass
    {
        /// <summary>
        /// Plot part class ID.
        /// </summary>
        public Guid PlotPartClassId { get; set; }

        /// <summary>
        /// Plot part class. Example: III
        /// </summary>
        public string Class { get; set; }
    }
}
