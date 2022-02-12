using PublicBidding.Entities;
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

        Task<List<Entities.PublicBidding>> GetPublicBiddings(int numberOfApplicants = 0, Type type = null, Status status = null);

        Task<Entities.PublicBidding> GetPublicBiddingById(Guid publicBiddingId);

        Task<PublicBiddingConfirmation> CreatePublicBidding(Entities.PublicBidding publicBidding);

        Task UpdatePublicBidding(Entities.PublicBidding publicBidding);

        Task DeletePublicBidding(Guid publicBiddingId);
    }
}
