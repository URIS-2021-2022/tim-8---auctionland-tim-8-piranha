using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotRepository : IPlotRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotRepository(PlotContext plotContext, IMapper mapper)
        {
            PlotContext = plotContext;
            Mapper = mapper;
        }

        public PlotConfirmation CreatePlot(Plot plot)
        {
            var createdEntity = PlotContext.Add(plot);
            return Mapper.Map<PlotConfirmation>(createdEntity.Entity);
        }

        public void DeletePlot(Guid plotId)
        {
            var plot = GetPlotById(plotId);
            PlotContext.Remove(plot);
        }

        public Plot GetPlotById(Guid plotId)
        {
            return PlotContext.Plots.FirstOrDefault(o => o.PlotId == plotId);
        }

        public List<Plot> GetPlots(string culture = null, string workability = null)
        {
            return PlotContext.Plots.Where(o => (o.PlotCurrentCulture == culture || culture == null) && (o.PlotCurrentWorkability == workability || workability == null)).ToList();
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlot(Plot plot)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
