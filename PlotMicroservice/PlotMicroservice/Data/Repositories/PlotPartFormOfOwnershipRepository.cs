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
    public class PlotPartFormOfOwnershipRepository : IPlotPartFormOfOwnershipRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotPartFormOfOwnershipRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public async Task<PlotPartFormOfOwnershipConfirmation> CreatPlotPartFormOfOwnershipAsync(PlotPartFormOfOwnership plotPartFormOfOwnership)
        {
            var createdEntity = await PlotContext.AddAsync(plotPartFormOfOwnership);
            return Mapper.Map<PlotPartFormOfOwnershipConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotPartFormOfOwnershipAsync(Guid plotPartOfOwnershipId)
        {
            var plotPartOfOwnership = await GetPlotPartFormOfOwnershipByIdAsync(plotPartOfOwnershipId);
            PlotContext.Remove(plotPartOfOwnership);
        }

        public async Task<PlotPartFormOfOwnership> GetPlotPartFormOfOwnershipByIdAsync(Guid plotPartOfOwnershipId)
        {
            return await PlotContext.PlotPartFormOfOwnerships.FirstOrDefaultAsync(o => o.PlotPartFormOfOwnershipId == plotPartOfOwnershipId);
        }

        public async Task<List<PlotPartFormOfOwnership>> GetPlotPartFormOfOwnershipsAsync(string formOfOwnership = null)
        {
            return await PlotContext.PlotPartFormOfOwnerships.Where(o => o.FormOfOwnership == formOfOwnership || formOfOwnership == null).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await PlotContext.SaveChangesAsync() > 0;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotPartFormOfOwnershipAsync(PlotPartFormOfOwnership plotPartFormOfOwnership)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
