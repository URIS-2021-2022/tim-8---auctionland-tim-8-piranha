﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot.
    /// </summary>
    public class PlotConfirmation
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
        /// Plot cadastral municipality ID.
        /// </summary>
        public Guid PlotCadastralMunicipalityId { get; set; }

        /// <summary>
        /// Plot workability ID.
        /// </summary>
        public Guid PlotWorkabilityId { get; set; }
        
        /// <summary>
        /// Plot surface area. Example: 5000
        /// </summary>
        public int PlotSurfaceArea { get; set; }

        /// <summary>
        /// Plot number. Example: 112
        /// </summary>
        public string PlotNumber { get; set; }

        /// <summary>
        /// Plot real estate number. Example: LN30
        /// </summary>
        public string PlotRealEstateListNumber { get; set; }

        /// <summary>
        /// Current plot culture.
        /// </summary>
        public string PlotCurrentCulture { get; set; }

        /// <summary>
        /// Current plot workability
        /// </summary>
        public string PlotCurrentWorkability { get; set; }
    }
}
