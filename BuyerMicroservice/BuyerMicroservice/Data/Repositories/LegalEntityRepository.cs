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
    public class LegalEntityRepository : ILegalEntityRepository
    {
        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;

        public LegalEntityRepository(BuyerContext buyerContext, IMapper mapper)
        {

            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }
       

        public async Task<LegalEntityConfirmation> CreateLegalEntityAsync(LegalEntity legalEntity)
        {
            var createdLegalEntity = await buyerContext.AddAsync(legalEntity);
            return mapper.Map<LegalEntityConfirmation>(createdLegalEntity.Entity);
        }

        
        public async Task DeleteLegalEntityAsync(Guid buyerID)
        {
            var legalEntity = await GetLegalEntityByIdAsync(buyerID);
            buyerContext.Remove(legalEntity);
        }

      

        public async Task<List<LegalEntity>> GetLegalEntityAsync(string identificationNumber = null)
        {
            return await buyerContext.legalEntity.Where(o => (o.identificationNumber == identificationNumber || identificationNumber == null)).ToListAsync();

        }

        public LegalEntity GetLegalEntityById(Guid buyerID)
        {
            return buyerContext.legalEntity.FirstOrDefault(o => o.buyerID == buyerID);
        }

        public async Task<LegalEntity> GetLegalEntityByIdAsync(Guid buyerID)
        {
            return await buyerContext.legalEntity.FirstOrDefaultAsync(o => o.buyerID == buyerID);

        }
        public async Task<bool> SaveChangesAsync()
        {
            return await buyerContext.SaveChangesAsync() > 0;
        }

        

        public async Task UpdateLegalEntityAsync(LegalEntity legalEntity)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
            kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
