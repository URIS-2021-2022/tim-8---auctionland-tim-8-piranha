using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.ServiceCalls.Mocks
{
    public class PlotServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            PlotDto plot = new PlotDto
            {
                PlotNumber = "112",
                PlotSurfaceArea = 2050,
                PlotRealEstateListNumber = "LN505",
                
            };

            return await Task.FromResult((T)Convert.ChangeType(plot, typeof(T)));
        }
    }
}
