using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotModel
{
    /// <summary>
    /// Creation DTO for plot.
    /// </summary>
    public class PlotCreationDto
    {
        /// <summary>
        /// Plot culture ID.
        /// </summary>
        public Guid PlotCultureId { get; set; }

        /// <summary>
        /// Plot cadastral municipality ID.
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
        /// Plot real estate number.
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

        /// <summary>
        /// Buyer ID.
        /// </summary>
        public Guid? BuyerId { get; set; }
    }
}
