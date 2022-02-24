using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class PublicBiddingBuyer
    {
        public Guid PublicBiddingId { get; set; }
        public PublicBidding PublicBidding { get; set; }
        public Guid BuyerId { get; set; }
    }
}
