using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    /// <summary>
    /// DTO za deo parcele
    /// </summary>
    public class PlotPartDto
    {
        /// <summary>
        /// Broj dela parcele
        /// </summary>
        public string? PlotPartNumber { get; set; }

        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int PlotPartSurfaceArea { get; set; }

        /// <summary>
        /// Id parcele
        /// </summary>
        public Guid PlotId { get; set; }
    }
}
