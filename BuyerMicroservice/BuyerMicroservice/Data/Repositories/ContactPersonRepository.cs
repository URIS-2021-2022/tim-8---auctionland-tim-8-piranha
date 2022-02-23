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
    public class ContactPersonRepository : IContactPersonRepository
    {
        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;
        public ContactPersonRepository(BuyerContext buyerContext, IMapper mapper)
        {
            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }

        

        public async Task<ContactPersonConfirmation> CreateContactPersonAsync(ContactPerson contactPerson)
        {
            var createdContactPerson = await buyerContext.AddAsync(contactPerson);
            return mapper.Map<ContactPersonConfirmation>(createdContactPerson.Entity);
        }



        public async Task DeleteContactPersonAsync(Guid contactPersonID)
        {
            var contactPerson = await GetContactPersonByIdAsync(contactPersonID);
            buyerContext.Remove(contactPerson);
        }

        public async Task<List<ContactPerson>> GetContactPersonAsync(string name = null)
        {
            return await buyerContext.contactPerson.Where(o => (o.name == name || name == null)).ToListAsync();

        }

        public async Task<ContactPerson> GetContactPersonByIdAsync(Guid contactPersonID)
        {
            return await buyerContext.contactPerson.FirstOrDefaultAsync(o => o.contactPersonID == contactPersonID);
        }



        public async Task<bool> SaveChangesAsync()
        {
            return await buyerContext.SaveChangesAsync() > 0;
        }



        public async Task UpdateContactPersonAsync(ContactPerson contactPerson)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
             kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }

        
    }
}
