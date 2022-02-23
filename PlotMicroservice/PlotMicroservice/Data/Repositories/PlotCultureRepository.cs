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
    public class PlotCultureRepository : IPlotCultureRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotCultureRepository(PlotContext plotContext, IMapper mapper)
        {
            PlotContext = plotContext;
            Mapper = mapper;
        }

        public async Task<PlotCultureConfirmation> CreatePlotCultureAsync(PlotCulture plotCulture)
        {
            var createdEntity = await PlotContext.AddAsync(plotCulture);
            return Mapper.Map<PlotCultureConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotCultureAsync(Guid plotCultureId)
        {
            var plotCulture = await GetPlotCultureByIdAsync(plotCultureId);
            PlotContext.Remove(plotCulture);
        }

        public async Task<PlotCulture> GetPlotCultureByIdAsync(Guid plotCultureId)
        {
            return await PlotContext.PlotCultures.FirstOrDefaultAsync(o => o.PlotCultureId == plotCultureId);
        }

        public async Task<List<PlotCulture>> GetPlotCulturesAsync(string culture = null)
        {
            return await PlotContext.PlotCultures.Where(o => o.Culture == culture || culture == null).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotCultureAsync(PlotCulture plotCulture)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
