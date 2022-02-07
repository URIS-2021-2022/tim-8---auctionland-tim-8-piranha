using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotWorkabilityRepository : IPlotWorkabilityRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotWorkabilityRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public PlotWorkabilityConfirmation CreatePlotWorkability(PlotWorkability plotWorkability)
        {
            var createdEntity = PlotContext.Add(plotWorkability);
            return Mapper.Map<PlotWorkabilityConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotWorkability(Guid plotWorkabilityId)
        {
            var plotWorkability = GetPlotWorkabilityById(plotWorkabilityId);
            PlotContext.Remove(plotWorkability);
        }

        public List<PlotWorkability> GetPlotWorkabilities(string workability = null)
        {
            return PlotContext.PlotWorkabilities.Where(o => o.Workability == workability || workability == null).ToList();
        }

        public PlotWorkability GetPlotWorkabilityById(Guid plotWorkabilityId)
        {
            return PlotContext.PlotWorkabilities.FirstOrDefault(o => o.PlotWorkabilityId == plotWorkabilityId);
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlotWorkability(PlotWorkability plotWorkability)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
