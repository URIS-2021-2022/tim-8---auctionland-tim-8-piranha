using AutoMapper;
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

        public Status GetStatusById(Guid statusId)
        {
            return context.Statuses.FirstOrDefault(e => e.StatusId == statusId);
        }

        public List<Status> GetStatuses()
        {
            return context.Statuses.ToList();
        }
    }
}
