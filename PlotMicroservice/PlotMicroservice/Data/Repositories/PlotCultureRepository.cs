using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotCultureRepository : IPlotCultureRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotCultureRepository(PlotContext plotContext, IMapper mapper)
        {
            PlotContext = plotContext;
            Mapper = mapper;
        }

        public PlotCultureConfirmation CreatePlotCulture(PlotCulture plotCulture)
        {
            var createdEntity = PlotContext.Add(plotCulture);
            return Mapper.Map<PlotCultureConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotCulture(Guid plotCultureId)
        {
            var plotCulture = GetPlotCultureById(plotCultureId);
            PlotContext.Remove(plotCulture);
        }

        public PlotCulture GetPlotCultureById(Guid plotCultureId)
        {
            return PlotContext.PlotCultures.FirstOrDefault(o => o.PlotCultureId == plotCultureId);
        }

        public List<PlotCulture> GetPlotCultures(string culture = null)
        {
            return PlotContext.PlotCultures.Where(o => o.Culture == culture || culture == null).ToList();
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlotCulture(PlotCulture plotCulture)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
