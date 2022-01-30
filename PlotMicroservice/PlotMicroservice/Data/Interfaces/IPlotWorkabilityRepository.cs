using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotWorkabilityRepository
    {
        List<PlotWorkability> GetPlotWorkabilities(string workability = null);

        PlotWorkability GetPlotWorkabilityById(Guid plotWorkabilityId);

        PlotWorkabilityConfirmation CreatePlotWorkability(PlotWorkability plotWorkability);

        void UpdatePlotWorkability(PlotWorkability plotWorkability);

        void DeletePlotWorkability(Guid plotWorkabilityId);

        bool SaveChanges();
    }
}
