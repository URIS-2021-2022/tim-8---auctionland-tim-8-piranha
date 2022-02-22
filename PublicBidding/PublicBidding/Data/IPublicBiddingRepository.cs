using PublicBidding.Entities;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = PublicBidding.Entities.Type;

namespace PublicBidding.Data
{
    public interface IPublicBiddingRepository
    {
        Task SaveChanges();

        Task<List<Entities.PublicBidding>> GetPublicBiddings();

        Task<Entities.PublicBidding> GetPublicBiddingById(Guid publicBiddingId);

        Task<PublicBiddingForOtherServices> GetPublicBiddingsById(Guid publicBiddingId);

        Task<PublicBiddingConfirmation> CreatePublicBidding(Entities.PublicBidding publicBidding);

        Task UpdatePublicBidding(Entities.PublicBidding publicBidding);

        Task DeletePublicBidding(Guid publicBiddingId);
    }
}
