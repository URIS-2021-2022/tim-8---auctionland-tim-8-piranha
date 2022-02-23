using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PlotPartClassConfirmation> CreatePlotPartClassAsync(PlotPartClass plotPartClass)
        {
            var createdEntity = await PlotContext.AddAsync(plotPartClass);
            return Mapper.Map<PlotPartClassConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotPartClassAsync(Guid plotPartClassId)
        {
            var plotPartClass = await GetPlotPartClassByIdAsync(plotPartClassId);
            PlotContext.Remove(plotPartClass);
        }

        public async Task<PlotPartClass> GetPlotPartClassByIdAsync(Guid plotPartClassId)
        {
            return await PlotContext.PlotPartClasses.FirstOrDefaultAsync(o => o.PlotPartClassId == plotPartClassId);
        }

        public async Task<List<PlotPartClass>> GetPlotPartClassesAsync(string plotPartClass = null)
        {
            return await PlotContext.PlotPartClasses.Where(o => o.Class == plotPartClass || plotPartClass == null).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotPartClassAsync(PlotPartClass plotPartClass)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
