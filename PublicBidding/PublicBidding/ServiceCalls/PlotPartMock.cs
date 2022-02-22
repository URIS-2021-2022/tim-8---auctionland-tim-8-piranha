using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class PlotPartMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            var plotPart = new PlotPartDto
            {
                NumberOfPlot = 2345,
                NumberOfPlotPart = 2,
                CadastralMunicipality = "Palic",
                Class = "I",
                Culture = "Njive",
                Workability = "Obradivo",
                Drainage = "I",
                SurfaceArea = 250,
                ProtectedZone = "2"
            };

            return await Task.FromResult((T)Convert.ChangeType(plotPart, typeof(T)));
        }
    }
}
