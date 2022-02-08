using AuctionMicroservice.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionContext context;
        private readonly IMapper mapper;

        public AuctionRepository(AuctionContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public AuctionConfirmation CreateAuction(Auction auction)
        {
            var auctionEntity = context.Add(auction);

            return mapper.Map<AuctionConfirmation>(auctionEntity.Entity);
        }

        public void DeleteAuction(Guid AuctionId)
        {
            var auction = GetAuctionById(AuctionId);

            context.Remove(auction);
        }

        public Auction GetAuctionById(Guid AuctionId)
        {


            return context.auction.FirstOrDefault(e => e.AuctionId == AuctionId);
        }

        public List<Auction> GetAuctions()
        {
            return context.auction.ToList();
        }          


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateAuction(Auction auction)
        {

        }
    }
}
