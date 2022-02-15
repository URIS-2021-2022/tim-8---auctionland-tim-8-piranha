using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents Plot. 
    /// </summary>
    public class Plot
    {
        /// <summary>
        /// ID Plot-a.
        /// </summary>
        public Guid PlotId { get; set; }

        // KorisnikParceleId ???
        /// <summary>
        /// Collection of plot parts within current plot.
        /// </summary>
        public ICollection<PlotPart> PlotParts { get; set; }

        /// <summary>
        /// Plot culture ID.
        /// </summary>
        [ForeignKey("PlotCulture")]
        public Guid PlotCultureId { get; set; }
        /// <summary>
        /// Plot culture.
        /// </summary>
        public PlotCulture PlotCulture { get; set; }

        /// <summary>
        /// Plot cadastral municipality ID.
        /// </summary>
        [ForeignKey("PlotCadastralMunicipality")]
        public Guid PlotCadastralMunicipalityId { get; set; }
        /// <summary>
        /// Plot cadastral municipality.
        /// </summary>
        public PlotCadastralMunicipality PlotCadastralMunicipality { get; set; }

        /// <summary>
        /// Plot workability ID.
        /// </summary>
        [ForeignKey("PlotWorkability")]
        public Guid PlotWorkabilityId { get; set; }
        /// <summary>
        /// Plot workability.
        /// </summary>
        public PlotWorkability PlotWorkability { get; set; }

        /// <summary>
        /// Plot surface area. Example: 3200
        /// </summary>
        public int PlotSurfaceArea { get; set; }

        /// <summary>
        /// Plot number. Example: 55
        /// </summary>
        public string PlotNumber { get; set; }

        /// <summary>
        /// Plot real estate number. Example: LN505
        /// </summary>
        public string PlotRealEstateListNumber { get; set; }

        /// <summary>
        /// Current plot culture
        /// </summary>
        public string PlotCurrentCulture { get; set; }

        /// <summary>
        /// Current plot workability.
        /// </summary>
        public string PlotCurrentWorkability { get; set; }
    }
}
