using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class PublicBiddingAuthorizedPerson
    {
        public Guid PublicBiddingId { get; set; }
        public PublicBidding? PublicBidding { get; set; }
        public Guid AuthorizedPersonId { get; set; }
    }
}
