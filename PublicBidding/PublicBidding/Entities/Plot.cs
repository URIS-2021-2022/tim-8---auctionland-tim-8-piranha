using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class Plot
    {
        public Guid PlotId { get; set; }

        public string PlotUser { get; set; }

        public string PlotName { get; set; }

        public string PlotCulture { get; set; }

        public string PlotCadastralMunicipality { get; set; }

        public string PlotWorkability { get; set; }

        public string PlotSurfaceArea { get; set; }

        public string PlotNumber { get; set; }

        public string PlotRealEstateListNumber { get; set; }

        public string PlotCurrentCulture { get; set; }

        public string PlotCurrentWorkability { get; set; }
    }
}
