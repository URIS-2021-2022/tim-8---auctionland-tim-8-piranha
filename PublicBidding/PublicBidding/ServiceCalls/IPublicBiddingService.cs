using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public interface IPublicBiddingService
    {
        Task<PublicBiddingDto> GetInfoForListsInPublicBidding(Entities.PublicBidding publicBidding);
    }
}
