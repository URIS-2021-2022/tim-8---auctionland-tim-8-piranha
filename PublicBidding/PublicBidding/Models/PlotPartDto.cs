using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    public class PlotPartDto
    {
        public string NumberOfPlot { get; set; }
        public string NumberOfPlotPart { get; set; }
        public string SurfaceArea { get; set; }
        public string Culture { get; set; }
        public string Class { get; set; }
        public string Workability { get; set; }
        public string ProtectedZone { get; set; }
        public string Drainage { get; set; }
        public string CadastralMunicipality { get; set; }
    }
}
