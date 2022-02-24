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
    public class PlotPartProtectedZoneRepository : IPlotPartProtectedZoneRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotPartProtectedZoneRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public async Task<PlotPartProtectedZoneConfirmation> CreatePlotPartProtectedZoneAsync(PlotPartProtectedZone plotPartProtectedZone)
        {
            var createdEntity = await PlotContext.AddAsync(plotPartProtectedZone);
            return Mapper.Map<PlotPartProtectedZoneConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotPartProtectedZoneAsync(Guid plotPartProtectedZoneId)
        {
            var plotPartProtectedZone = await GetPlotPartProtectedZoneByIdAsync(plotPartProtectedZoneId);
            PlotContext.Remove(plotPartProtectedZone);
        }

        public async Task<PlotPartProtectedZone> GetPlotPartProtectedZoneByIdAsync(Guid plotPartProtectedZoneId)
        {
            return await PlotContext.PlotPartProtectedZones.FirstOrDefaultAsync(o => o.PlotPartProtectedZoneId == plotPartProtectedZoneId);
        }

        public async Task<List<PlotPartProtectedZone>> GetPlotPartProtectedZonesAsync(string protectedZone = null)
        {
            return await PlotContext.PlotPartProtectedZones.Where(o => o.ProtectedZone == protectedZone || protectedZone == null).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotPartProtectedZoneAsync(PlotPartProtectedZone plotPartProtectedZone)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
