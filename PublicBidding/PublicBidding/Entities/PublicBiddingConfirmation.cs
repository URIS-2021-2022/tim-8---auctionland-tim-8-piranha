using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class PublicBiddingConfirmation
    {
        public Guid PublicBiddingId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime Date { get; set; }

        public double StartPricePerHa { get; set; }
    }
}
