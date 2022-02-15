using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotCultureModel
{
    /// <summary>
    /// Update model for plot culture.
    /// </summary>
    public class PlotCultureUpdateDto
    {
        /// <summary>
        /// Plot culture ID.
        /// </summary>
        public Guid PlotCultureId { get; set; }

        /// <summary>
        /// Plot culture.
        /// </summary>
        public string Culture { get; set; }
    }
}
