using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotCultureRepository
    {
        List<PlotCulture> GetPlotCultures(string culture = null);

        PlotCulture GetPlotCultureById(Guid plotCultureId);

        PlotCultureConfirmation CreatePlotCulture(PlotCulture plotCulture);

        void UpdatePlotCulture(PlotCulture plotCulture);

        void DeletePlotCulture(Guid plotCultureId);

        bool SaveChanges();
    }
}
