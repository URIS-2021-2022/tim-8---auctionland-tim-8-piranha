using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartRepository
    {
        Task<List<PlotPart>> GetPlotPartsAsync(string plotPartCurrentClass = null, string plotPartCurrentProtectedZone = null);

        Task<List<PlotPart>> GetPlotPartsByPlotIdAsync(Guid plotId);

        Task<PlotPart> GetPlotPartByIdAsync(Guid plotPartId);

        Task<PlotPartConfirmation> CreatePlotPartAsync(PlotPart plotPart);

        Task UpdatePlotPartAsync(PlotPart plotPart);

        Task DeletePlotPartAsync(Guid plotPartId);

        Task<bool> SaveChangesAsync();
    }
}
