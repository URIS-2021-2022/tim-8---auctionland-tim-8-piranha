using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotRepository
    {
        List<Plot> GetPlots(string culture = null, string workability = null);

        Plot GetPlotById(Guid plotId);

        PlotConfirmation CreatePlot(Plot plot);

        void UpdatePlot(Plot plot);

        void DeletePlot(Guid plotId);

        bool SaveChanges();
    }
}
