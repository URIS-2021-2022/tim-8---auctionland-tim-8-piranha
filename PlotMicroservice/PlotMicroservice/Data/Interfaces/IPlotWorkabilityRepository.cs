using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotWorkabilityRepository
    {
        Task<List<PlotWorkability>> GetPlotWorkabilitiesAsync(string workability = null);

        Task<PlotWorkability> GetPlotWorkabilityByIdAsync(Guid plotWorkabilityId);

        Task<PlotWorkabilityConfirmation> CreatePlotWorkabilityAsync(PlotWorkability plotWorkability);

        Task UpdatePlotWorkabilityAsync(PlotWorkability plotWorkability);

        Task DeletePlotWorkabilityAsync(Guid plotWorkabilityId);

        Task<bool> SaveChangesAsync();
    }
}
