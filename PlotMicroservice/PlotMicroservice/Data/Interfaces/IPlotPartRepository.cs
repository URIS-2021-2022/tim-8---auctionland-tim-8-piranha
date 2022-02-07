using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartRepository
    {
        List<PlotPart> GetPlotParts(string plotPartCurrentClass = null, string plotPartCurrentProtectedZone = null);

        List<PlotPart> GetPlotPartsByPlotId(Guid plotId);

        PlotPart GetPlotPartById(Guid plotPartId);

        PlotPartConfirmation CreatePlotPart(PlotPart plotPart);

        void UpdatePlotPart(PlotPart plotPart);

        void DeletePlotPart(Guid plotPartId);

        bool SaveChanges();
    }
}
