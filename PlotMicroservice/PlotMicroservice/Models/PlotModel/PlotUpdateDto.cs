using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotModel
{
    /// <summary>
    /// Update model for plot.
    /// </summary>
    public class PlotUpdateDto
    {
        /// <summary>
        /// Plot ID.
        /// </summary>
        public Guid PlotId { get; set; }

        /// <summary>
        /// Plot culture ID.
        /// </summary>
        public Guid PlotCultureId { get; set; }

        /// <summary>
        /// Plot cadastral municiplaity ID.
        /// </summary>
        public Guid PlotCadastralMunicipalityId { get; set; }

        /// <summary>
        /// Plot workability ID.
        /// </summary>
        public Guid PlotWorkabilityId { get; set; }

        /// <summary>
        /// Plot surface area.
        /// </summary>
        public int PlotSurfaceArea { get; set; }

        /// <summary>
        /// Plot number.
        /// </summary>
        public string PlotNumber { get; set; }

        /// <summary>
        /// Plot real estate list number.
        /// </summary>
        public string PlotRealEstateListNumber { get; set; }

        /// <summary>
        /// Current plot culture.
        /// </summary>
        public string PlotCurrentCulture { get; set; }

        /// <summary>
        /// Current plot workability.
        /// </summary>
        public string PlotCurrentWorkability { get; set; }
    }
}
