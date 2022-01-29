using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class Plot
    {
        public Guid PlotId { get; set; }

        // KorinsikParceleId ???
        public string PlotName { get; set; }

        public List<PlotPart> PlotParts { get; set; }

        public PlotCulture PlotCulture { get; set; }

        public PlotCadastralMunicipality PlotCadastralMunicipality { get; set; }

        public PlotWorkability PlotWorkability { get; set; }

        public string PlotSurfaceArea { get; set; }

        public string PlotNumber { get; set; }

        public string PlotRealEstateListNumber { get; set; }

        public string PlotCurrentCulture { get; set; }

        public string PlotCurrentWorkability { get; set; }
    }
}
