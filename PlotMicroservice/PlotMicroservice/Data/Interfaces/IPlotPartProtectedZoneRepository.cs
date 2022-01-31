using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartProtectedZoneRepository
    {
        List<PlotPartProtectedZone> GetPlotPartProtectedZones(string protectedZone = null);

        PlotPartProtectedZone GetPlotPartProtectedZoneById(Guid plotPartProtectedZoneId);

        PlotPartProtectedZoneConfirmation CreatePlotPartProtectedZone(PlotPartProtectedZone plotPartProtectedZone);

        void UpdatePlotPartProtectedZone(PlotPartProtectedZone plotPartProtectedZone);

        void DeletePlotPartProtectedZone(Guid plotPartProtectedZoneId);

        bool SaveChanges();
    }
}
