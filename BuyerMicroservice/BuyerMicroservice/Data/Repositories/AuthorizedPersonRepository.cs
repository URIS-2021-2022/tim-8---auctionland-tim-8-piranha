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
    public class AuthorizedPersonRepository : IAuthorizedPersonRepository
    {

        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;
        public AuthorizedPersonRepository(BuyerContext buyerContext, IMapper mapper)
        {
            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }

        public async Task AddBuyerToAuthorizedPerson(Buyer buyer, Guid authorizedPersonId)
        {
            AuthorizedPerson ap = await buyerContext.authorizedPerson.FirstOrDefaultAsync(o => o.authorizedPersonID == authorizedPersonId);

            ap.buyers.Add(buyer);
        }

        public async Task<AuthorizedPersonConfirmation> CreateAuthorizedPersonAsync(AuthorizedPerson authorizedPerson)
        {
            var createdAuthorizedPerson = await buyerContext.AddAsync(authorizedPerson);
            return mapper.Map<AuthorizedPersonConfirmation>(createdAuthorizedPerson.Entity);
        }


        public async Task DeleteAuthorizedPersonAsync(Guid authorizedPersonID)
        {
            var authorizedPerson = await GetAuthorizedPersonByIdAsync(authorizedPersonID);
            buyerContext.Remove(authorizedPerson);
        }

        public async Task<List<AuthorizedPerson>> GetAuthorizedPersonAsync(string personalDocNum = null)
        {
            return await buyerContext.authorizedPerson.Where(o => (o.personalDocNum == personalDocNum || personalDocNum == null)).ToListAsync();
        }

        public async Task<AuthorizedPerson> GetAuthorizedPersonByIdAsync(Guid authorizedPersonID)
        {
            return await buyerContext.authorizedPerson.FirstOrDefaultAsync(o => o.authorizedPersonID == authorizedPersonID);
        }

        public async Task RemoveBuyerFromAuthorizedPerson(Buyer buyer, Guid authorizedPersonId)
        {
            AuthorizedPerson ap = await buyerContext.authorizedPerson.FirstOrDefaultAsync(o => o.authorizedPersonID == authorizedPersonId);

            ap.buyers.Remove(buyer);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await  buyerContext.SaveChangesAsync() > 0;
        }

        public void UpdateAuthorizedPerson(AuthorizedPerson authorizedPerson)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }

        public async Task UpdateAuthorizedPersonAsync(AuthorizedPerson authorizedPerson)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
            kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }

        
    }
}
