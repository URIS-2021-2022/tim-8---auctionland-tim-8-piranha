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
                PlotPartNumber = "2",
                PlotPartSurfaceArea = 45,
                PlotId = Guid.Parse("53576442-a7c2-474e-9620-523b0707b36c")
            };

            return await Task.FromResult((T)Convert.ChangeType(plotPart, typeof(T)));
        }
    }
}
