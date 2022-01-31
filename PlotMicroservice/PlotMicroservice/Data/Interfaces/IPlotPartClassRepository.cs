using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartClassRepository
    {
        List<PlotPartClass> GetPlotPartClasses(string plotPartClass = null);

        PlotPartClass GetPlotPartClassById(Guid plotPartClassId);

        PlotPartClassConfirmation CreatePlotPartClass(PlotPartClass plotPartClass);

        void UpdatePlotPartClass(PlotPartClass plotPartClass);

        void DeletePlotPartClass(Guid plotPartClassId);

        bool SaveChanges();
    }
}
