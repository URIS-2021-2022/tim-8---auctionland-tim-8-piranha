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
        List<Entities.PublicBidding> GetPublicBiddings(int numberOfApplicants = 0, Type type = null, Status status = null);

        Entities.PublicBidding GetPublicBiddingById(Guid publicBiddingId);

        PublicBiddingConfirmation CreatePublicBidding(Entities.PublicBidding publicBidding);

        void UpdatePublicBidding(Entities.PublicBidding publicBidding);

        void DeletePublicBidding(Guid publicBiddingId);
    }
}
