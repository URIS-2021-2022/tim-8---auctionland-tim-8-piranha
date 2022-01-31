using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotPartClassRepository : IPlotPartClassRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;
        
        public PlotPartClassRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public PlotPartClassConfirmation CreatePlotPartClass(PlotPartClass plotPartClass)
        {
            var createdEntity = PlotContext.Add(plotPartClass);
            return Mapper.Map<PlotPartClassConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotPartClass(Guid plotPartClassId)
        {
            var plotPartClass = GetPlotPartClassById(plotPartClassId);
            PlotContext.Remove(plotPartClass);
        }

        public PlotPartClass GetPlotPartClassById(Guid plotPartClassId)
        {
            return PlotContext.PlotPartClasses.FirstOrDefault(o => o.PlotPartClassId == plotPartClassId);
        }

        public List<PlotPartClass> GetPlotPartClasses(string plotPartClass = null)
        {
            return PlotContext.PlotPartClasses.Where(o => o.Class == plotPartClass || plotPartClass == null).ToList();
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlotPartClass(PlotPartClass plotPartClass)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
