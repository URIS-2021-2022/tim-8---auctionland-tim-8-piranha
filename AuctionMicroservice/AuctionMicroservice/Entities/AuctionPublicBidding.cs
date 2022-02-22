using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    /// <summary>
    /// Represent AuctionPublicBidding entity
    /// </summary>
    public class AuctionPublicBidding
    {
        #region
        /// <summary>
        /// Public bidding ID
        /// </summary>
       [Key]
        public Guid PublicBiddingId { get; set; }
       
        /// <summary>
        /// ID of auction that public bidding belongs to
        /// </summary>
        public Guid AuctionId { get; set; }

        public Auction auction { get; set; }

        #endregion
    }
}
