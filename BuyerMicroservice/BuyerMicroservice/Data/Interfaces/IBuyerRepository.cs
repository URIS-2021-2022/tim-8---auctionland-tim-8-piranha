using BuyerMicroservice.Entities;
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

        Task<BuyerConfirmation> CreateBuyerAsync<T>(Buyer buyer) where T : BuyerConfirmation;

      

        Task DeleteBuyerAsync(Guid buyerID);

        Task<bool> SaveChangesAsync();
    }
}
