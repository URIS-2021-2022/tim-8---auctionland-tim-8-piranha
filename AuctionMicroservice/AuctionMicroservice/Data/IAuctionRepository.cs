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
        AuctionConfirmation CreateAuction(Auction auction);

        List<Auction> GetAuctions();

        Auction GetAuctionById(Guid AuctionId);

        void UpdateAuction(Auction auction);

        void DeleteAuction(Guid AuctionId);

        bool SaveChanges();


    }
}
