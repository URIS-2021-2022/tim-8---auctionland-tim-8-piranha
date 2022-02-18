using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotCultureRepository
    {
        Task<List<PlotCulture>> GetPlotCulturesAsync(string culture = null);

        Task<PlotCulture> GetPlotCultureByIdAsync(Guid plotCultureId);

        Task<PlotCultureConfirmation> CreatePlotCultureAsync(PlotCulture plotCulture);

        Task UpdatePlotCultureAsync(PlotCulture plotCulture);

        Task DeletePlotCultureAsync(Guid plotCultureId);

        Task<bool> SaveChangesAsync();
    }
}
