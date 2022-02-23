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
    public class PlotPartRepository : IPlotPartRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotPartRepository(PlotContext plotContext, IMapper mapper)
        {
            PlotContext = plotContext;
            Mapper = mapper;
        }

        public async Task<PlotPartConfirmation> CreatePlotPartAsync(PlotPart plotPart)
        {
            var createdEntity = await PlotContext.AddAsync(plotPart);
            return Mapper.Map<PlotPartConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotPartAsync(Guid plotPartId)
        {
            var plotPart = await GetPlotPartByIdAsync(plotPartId);
            PlotContext.Remove(plotPart);
        }

        public async Task<PlotPart> GetPlotPartByIdAsync(Guid plotPartId)
        {
            return await PlotContext.PlotParts.FirstOrDefaultAsync(o => o.PlotPartId == plotPartId);
        }

        public async Task<List<PlotPart>> GetPlotPartsAsync(string plotPartCurrentClass = null, string plotPartCurrentProtectedZone = null)
        {
            return await PlotContext.PlotParts.Where(o => (o.PlotPartCurrentClass == plotPartCurrentClass || plotPartCurrentClass == null) && (o.PlotPartCurrentProtectedZone == plotPartCurrentProtectedZone || plotPartCurrentProtectedZone == null)).ToListAsync();
        }

        public async Task<List<PlotPart>> GetPlotPartsByPlotIdAsync(Guid plotId)
        {
            return await PlotContext.PlotParts.Where(o => o.PlotId == plotId).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotPartAsync(PlotPart plotPart)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}

