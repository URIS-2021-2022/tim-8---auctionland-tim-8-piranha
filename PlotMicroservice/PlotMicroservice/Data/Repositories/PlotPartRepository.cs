using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotPartRepository : IPlotPartRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotPartRepository(PlotContext plotContext, IMapper mapper)
        {
            PlotContext = plotContext;
            Mapper = mapper;
        }

        public PlotPartConfirmation CreatePlotPart(PlotPart plotPart)
        {
            var createdEntity = PlotContext.Add(plotPart);
            return Mapper.Map<PlotPartConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotPart(Guid plotPartId)
        {
            var plotPart = GetPlotPartById(plotPartId);
            PlotContext.Remove(plotPart);
        }

        public PlotPart GetPlotPartById(Guid plotPartId)
        {
            return PlotContext.PlotParts.FirstOrDefault(o => o.PlotPartId == plotPartId);
        }

        public List<PlotPart> GetPlotParts(string plotPartCurrentClass = null, string plotPartCurrentProtectedZone = null)
        {
            return PlotContext.PlotParts.Where(o => (o.PlotPartCurrentClass == plotPartCurrentClass || plotPartCurrentClass == null) && (o.PlotPartCurrentProtectedZone == plotPartCurrentProtectedZone || plotPartCurrentProtectedZone == null)).ToList();
        }

        public List<PlotPart> GetPlotPartsByPlotId(Guid plotId)
        {
            return PlotContext.PlotParts.Where(o => o.PlotId == plotId).ToList();
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlotPart(PlotPart plotPart)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}

