using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public class StatusRepository : IStatusRepository
    {
        private readonly PublicBiddingContext context;

        public StatusRepository(PublicBiddingContext context)
        {
            this.context = context;
        }

        public async Task<Status> GetStatusById(Guid statusId)
        {
            return await context.Statuses.FirstOrDefaultAsync(s => s.StatusId == statusId);
        }

        public async Task<List<Status>> GetStatuses()
        {
            return await context.Statuses.ToListAsync();
        }
    }
}
