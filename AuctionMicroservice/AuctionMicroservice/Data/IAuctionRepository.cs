using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IAuctionRepository
    {
        Task<AuctionConfirmation> CreateAuctionAsync(Auction auction);

        Task<List<Auction>> GetAuctionsAsync();

        Task<Auction> GetAuctionByIdAsync(Guid AuctionId);

        void UpdateAuction(Auction auction);

        void DeleteAuctionAsync(Guid AuctionId);

        Task<bool> SaveChangesAsync();


    }
}
