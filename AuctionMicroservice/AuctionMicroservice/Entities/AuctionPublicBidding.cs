using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class AuctionPublicBidding
    {
        #region
       [Key]
        public Guid PublicBiddingId { get; set; }
        /// <summary>
        /// Public bidding date
        /// </summary>
        /// <summary>
        /// ID of auction that public bidding belongs to
        /// </summary>
        public Guid AuctionId { get; set; }

        public Auction auction { get; set; }

        #endregion
    }
}
