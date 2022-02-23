using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;
        public PriorityRepository(BuyerContext buyerContext, IMapper mapper)
        {
            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }
        

        public async Task<PriorityConfirmation> CreatePriorityAsync(Priority priority)
        {
            var createdPriority =await buyerContext.AddAsync(priority);
            return mapper.Map<PriorityConfirmation>(createdPriority.Entity);
        }

      
        public async Task DeletePriorityAsync(Guid priorityID)
        {
            var priority =await GetPriorityByIdAsync(priorityID);
            buyerContext.Remove(priority);
        }

      
        public async Task<List<Priority>> GetPriorityAsync(string priorityType = null)
        {
            return await buyerContext.priority.Where(o => (o.priorityType == priorityType || priorityType == null)).ToListAsync();

        }

        public Priority GetPriorityById(Guid priorityID)
        {
            return buyerContext.priority.FirstOrDefault(o => o.priorityID == priorityID);
        }

        public async  Task<Priority> GetPriorityByIdAsync(Guid priorityID)
        {
            return await buyerContext.priority.FirstOrDefaultAsync(o => o.priorityID == priorityID);
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await buyerContext.SaveChangesAsync() > 0;
        }

      
        public async  Task UpdatePriorityAsync(Priority priority)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
