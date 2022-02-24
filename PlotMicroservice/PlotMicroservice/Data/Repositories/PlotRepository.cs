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
    public class PlotRepository : IPlotRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotRepository(PlotContext plotContext, IMapper mapper)
        {
            PlotContext = plotContext;
            Mapper = mapper;
        }

        public async Task<PlotConfirmation> CreatePlotAsync(Plot plot)
        {
            var createdEntity = await PlotContext.AddAsync(plot);
            return Mapper.Map<PlotConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotAsync(Guid plotId)
        {
            var plot = await GetPlotByIdAsync(plotId);
            PlotContext.Remove(plot);
        }

        public async Task<Plot> GetPlotByIdAsync(Guid plotId)
        {
            return await PlotContext.Plots.FirstOrDefaultAsync(o => o.PlotId == plotId);
        }

        public async Task<List<Plot>> GetPlotsAsync(string culture = null, string workability = null)
        {
            return await PlotContext.Plots.Where(o => (o.PlotCurrentCulture == culture || culture == null) && (o.PlotCurrentWorkability == workability || workability == null)).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotAsync(Plot plot)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
