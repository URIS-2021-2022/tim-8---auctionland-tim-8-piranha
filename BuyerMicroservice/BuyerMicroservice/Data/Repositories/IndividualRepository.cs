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
    public class IndividualRepository : IIndividualRepository
    {
        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;

        public IndividualRepository(BuyerContext buyerContext, IMapper mapper)
        {

            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }
       

        public async  Task<IndividualConfirmation> CreateIndividualAsync(Individual individual)
        {
            var createdIndividual = await buyerContext.AddAsync(individual);
            return mapper.Map<IndividualConfirmation>(createdIndividual.Entity);
        }

       

        public async Task DeleteIndividualAsync(Guid buyerID)
        {
            var individual = await GetIndividualByIdAsync(buyerID);
            buyerContext.Remove(individual);
        }

      

        public async  Task<List<Individual>> GetIndividualAsync(string JMBG = null)
        {
            return await buyerContext.individual.Where(o => (o.JMBG == JMBG || JMBG == null)).ToListAsync();

        }

        public async  Task<Individual> GetIndividualByIdAsync(Guid buyerID)
        {
            return await buyerContext.individual.FirstOrDefaultAsync(o => o.buyerID == buyerID);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await buyerContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateIndividualAsync(Individual individual)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
            kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
