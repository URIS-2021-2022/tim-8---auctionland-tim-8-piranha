using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class Plot
    {
        private Guid PlotId { get; set; }
        
        // KorinsikParceleId ???
        private string PlotName { get; set; }

        private List<PlotPart> PlotParts { get; set; }

        private PlotCulture PlotCulture { get; set; }

        private PlotCadastralMunicipality PlotCadastralMunicipality { get; set; }

        private PlotWorkability PlotWorkability { get; set; }

        private string PlotSurfaceArea { get; set; }

        private string PlotNumber { get; set; }

        private string PlotRealEstateListNumber { get; set; }

        private string PlotCurrentCulture { get; set; }

        private string PlotCurrentWorkability { get; set; }
    }
}
