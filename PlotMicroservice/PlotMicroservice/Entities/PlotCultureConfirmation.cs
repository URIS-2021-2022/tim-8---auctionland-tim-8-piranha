using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation for plot culture.
    /// </summary>
    public class PlotCultureConfirmation
    {
        /// <summary>
        /// Plot culture ID.
        /// </summary>
        public Guid PlotCultureId { get; set; }

        /// <summary>
        /// Plot culture. Example: Pašnjaci
        /// </summary>
        public string Culture { get; set; }
    }
}
