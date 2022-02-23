using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartClassRepository
    {
        Task<List<PlotPartClass>> GetPlotPartClassesAsync(string plotPartClass = null);

        Task<PlotPartClass> GetPlotPartClassByIdAsync(Guid plotPartClassId);

        Task<PlotPartClassConfirmation> CreatePlotPartClassAsync(PlotPartClass plotPartClass);

        Task UpdatePlotPartClassAsync(PlotPartClass plotPartClass);

        Task DeletePlotPartClassAsync(Guid plotPartClassId);

        Task<bool> SaveChangesAsync();
    }
}
