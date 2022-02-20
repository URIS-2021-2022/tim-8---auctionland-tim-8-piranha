﻿using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
   public interface IBuyerRepository
    {
        Task<List<Buyer>> GetBuyerAsync(int realizedArea = 0);

        Task<Buyer> GetBuyerByIdAsync(Guid buyerID);

        Task<BuyerConfirmation> CreateBuyerAsync(Buyer buyer);

        Task UpdateBuyerAsync(Buyer buyer);

        Task DeleteBuyerAsync(Guid buyerID);

        Task<bool> SaveChangesAsync();
    }
}
