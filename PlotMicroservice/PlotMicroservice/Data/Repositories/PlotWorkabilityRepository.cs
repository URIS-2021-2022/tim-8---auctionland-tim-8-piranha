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
    public class PlotWorkabilityRepository : IPlotWorkabilityRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotWorkabilityRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public async Task<PlotWorkabilityConfirmation> CreatePlotWorkabilityAsync(PlotWorkability plotWorkability)
        {
            var createdEntity = await PlotContext.AddAsync(plotWorkability);
            return Mapper.Map<PlotWorkabilityConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotWorkabilityAsync(Guid plotWorkabilityId)
        {
            var plotWorkability = await GetPlotWorkabilityByIdAsync(plotWorkabilityId);
            PlotContext.Remove(plotWorkability);
        }

        public async Task<List<PlotWorkability>> GetPlotWorkabilitiesAsync(string workability = null)
        {
            return await PlotContext.PlotWorkabilities.Where(o => o.Workability == workability || workability == null).ToListAsync();
        }

        public async Task<PlotWorkability> GetPlotWorkabilityByIdAsync(Guid plotWorkabilityId)
        {
            return await PlotContext.PlotWorkabilities.FirstOrDefaultAsync(o => o.PlotWorkabilityId == plotWorkabilityId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotWorkabilityAsync(PlotWorkability plotWorkability)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
