using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartProtectedZoneRepository
    {
        Task<List<PlotPartProtectedZone>> GetPlotPartProtectedZonesAsync(string protectedZone = null);

        Task<PlotPartProtectedZone> GetPlotPartProtectedZoneByIdAsync(Guid plotPartProtectedZoneId);

        Task<PlotPartProtectedZoneConfirmation> CreatePlotPartProtectedZoneAsync(PlotPartProtectedZone plotPartProtectedZone);

        Task UpdatePlotPartProtectedZoneAsync(PlotPartProtectedZone plotPartProtectedZone);

        Task DeletePlotPartProtectedZoneAsync(Guid plotPartProtectedZoneId);

        Task<bool> SaveChangesAsync();
    }
}
