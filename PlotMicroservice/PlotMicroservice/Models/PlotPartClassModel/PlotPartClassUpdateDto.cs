using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartClassModel
{
    /// <summary>
    /// Update model for plot part class.
    /// </summary>
    public class PlotPartClassUpdateDto
    {
        /// <summary>
        /// Plot part class ID.
        /// </summary>
        public Guid PlotPartClassId { get; set; }

        /// <summary>
        /// Plot part class.
        /// </summary>
        public string Class { get; set; }
    }
}
