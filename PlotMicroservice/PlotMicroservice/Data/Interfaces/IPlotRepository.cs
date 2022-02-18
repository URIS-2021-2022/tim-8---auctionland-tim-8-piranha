using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotRepository
    {
        Task<List<Plot>> GetPlotsAsync(string culture = null, string workability = null);

        Task<Plot> GetPlotByIdAsync(Guid plotId);

        Task<PlotConfirmation> CreatePlotAsync(Plot plot);

        Task UpdatePlotAsync(Plot plot);

        Task DeletePlotAsync(Guid plotId);

        Task<bool> SaveChangesAsync();
    }
}
