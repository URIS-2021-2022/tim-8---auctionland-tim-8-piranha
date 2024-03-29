﻿using AutoMapper;
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
    public class BuyerRepository : IBuyerRepository
    {
        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;
        public BuyerRepository(BuyerContext buyerContext, IMapper mapper)
        {
            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }

        
        public async Task AddAuthorizedPersonToBuyer(AuthorizedPerson authorizedPerson, Guid buyerId)
        {
             Buyer buyer= await buyerContext.buyer.FirstOrDefaultAsync(o => o.buyerID == buyerId);

            buyer.authorizedPerson.Add(authorizedPerson);
        }

        public async Task<BuyerConfirmation> CreateBuyerAsync<T>(Buyer buyer) where T : BuyerConfirmation
        {
            var createdBuyer =await buyerContext.AddAsync(buyer);
          
            return mapper.Map<T>(createdBuyer.Entity);
        }

       

        public async Task DeleteBuyerAsync(Guid buyerID)
        {
            var buyer = await GetBuyerByIdAsync(buyerID);
            buyerContext.Remove(buyer);
        }

        

        public async Task<List<Buyer>> GetBuyerAsync(int realizedArea = 0)
        {
            return await buyerContext.buyer.Where(o => o.realizedArea == realizedArea || realizedArea == 0).ToListAsync();
        }

       

        public async  Task<Buyer> GetBuyerByIdAsync(Guid buyerID)
        {
            return await buyerContext.buyer.FirstOrDefaultAsync(o => o.buyerID == buyerID);
        }

      
        public async Task RemoveAuthorizedPersonFromBuyer(AuthorizedPerson authorizedPerson, Guid buyerId)
        {
            Buyer buyer = await buyerContext.buyer.FirstOrDefaultAsync(o => o.buyerID == buyerId);

            buyer.authorizedPerson.Remove(authorizedPerson);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await buyerContext.SaveChangesAsync() > 0;
        }

        

       
    }
}
