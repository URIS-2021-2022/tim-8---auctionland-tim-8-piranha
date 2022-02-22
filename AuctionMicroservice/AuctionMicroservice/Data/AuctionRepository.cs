using AuctionMicroservice.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<AuctionConfirmation> CreateAuctionAsync(Auction auction)
        {
            var auctionEntity =  await context.AddAsync(auction);

            return  mapper.Map<AuctionConfirmation>(auctionEntity.Entity);
        }

        public async Task DeleteAuctionAsync(Guid AuctionId)
        {
            var auction =  await GetAuctionByIdAsync(AuctionId);

            context.Remove(auction);
        }

        public async Task<Auction> GetAuctionByIdAsync(Guid AuctionId)
        {


            return await context.auction.FirstOrDefaultAsync(e => e.AuctionId == AuctionId);
        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {
            return await context.auction.ToListAsync();
        }          


        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAuctionAsync(Auction auction)
        {

        }
    }
}
