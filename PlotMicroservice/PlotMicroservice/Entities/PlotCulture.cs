using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents plot culture.
    /// </summary>
    public class PlotCulture
    {
        /// <summary>
        /// Plot culture ID.
        /// </summary>
        public Guid PlotCultureId { get; set; }

        /// <summary>
        /// Plot culture. Example: Vinogradi
        /// </summary>
        public string Culture { get; set; }
    }
}
