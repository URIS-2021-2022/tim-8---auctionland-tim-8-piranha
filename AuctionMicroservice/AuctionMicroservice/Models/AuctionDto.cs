using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    /// <summary>
    /// Represents auction model
    /// </summary>
    public class AuctionDto
    {

        public AuctionDto()
        {
            publicBiddings = new List<PublicBiddingDto>();
        }
        #region
        /// <summary>
        /// Auction ID
        /// </summary>
        public Guid AuctionId { get; set; }
        /// <summary>
        /// Auction number
        /// </summary>
        public int AuctionNum { get; set; }
        /// <summary>
        /// Year when auction is happening
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Date od auction
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Auction restriction number
        /// </summary>
        public int Restriction { get; set; }
        /// <summary>
        /// Auction price step
        /// </summary>
        public int PriceStep { get; set; }
       
        /// <summary>
        /// List of public biddings
        /// </summary>
        public List<PublicBiddingDto> publicBiddings { get; set; }

        /// <summary>
        /// Date of application deadline
        /// </summary>
        /// 
        public DateTime ApplicationDeadline { get; set; }
        #endregion
    }
}
