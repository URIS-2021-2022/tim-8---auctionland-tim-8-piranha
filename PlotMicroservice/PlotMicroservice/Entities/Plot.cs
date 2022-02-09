using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class Plot
    {
        public Guid PlotId { get; set; }

        // KorisnikParceleId ???

        public ICollection<PlotPart> PlotParts { get; set; }

        [ForeignKey("PlotCulture")]
        public Guid PlotCultureId { get; set; }
        public PlotCulture PlotCulture { get; set; }

        [ForeignKey("PlotCadastralMunicipality")]
        public Guid PlotCadastralMunicipalityId { get; set; }
        public PlotCadastralMunicipality PlotCadastralMunicipality { get; set; }

        [ForeignKey("PlotWorkability")]
        public Guid PlotWorkabilityId { get; set; }
        public PlotWorkability PlotWorkability { get; set; }

        public int PlotSurfaceArea { get; set; }

        public string PlotNumber { get; set; }

        public string PlotRealEstateListNumber { get; set; }

        public string PlotCurrentCulture { get; set; }

        public string PlotCurrentWorkability { get; set; }
    }
}
