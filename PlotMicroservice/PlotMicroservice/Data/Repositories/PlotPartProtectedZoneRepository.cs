using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotPartProtectedZoneRepository : IPlotPartProtectedZoneRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotPartProtectedZoneRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public PlotPartProtectedZoneConfirmation CreatePlotPartProtectedZone(PlotPartProtectedZone plotPartProtectedZone)
        {
            var createdEntity = PlotContext.Add(plotPartProtectedZone);
            return Mapper.Map<PlotPartProtectedZoneConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotPartProtectedZone(Guid plotPartProtectedZoneId)
        {
            var plotPartProtectedZone = GetPlotPartProtectedZoneById(plotPartProtectedZoneId);
            PlotContext.Remove(plotPartProtectedZone);
        }

        public PlotPartProtectedZone GetPlotPartProtectedZoneById(Guid plotPartProtectedZoneId)
        {
            return PlotContext.PlotPartProtectedZones.FirstOrDefault(o => o.PlotPartProtectedZoneId == plotPartProtectedZoneId);
        }

        public List<PlotPartProtectedZone> GetPlotPartProtectedZones(string protectedZone = null)
        {
            return PlotContext.PlotPartProtectedZones.Where(o => o.ProtectedZone == protectedZone || protectedZone == null).ToList();
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlotPartProtectedZone(PlotPartProtectedZone plotPartProtectedZone)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
