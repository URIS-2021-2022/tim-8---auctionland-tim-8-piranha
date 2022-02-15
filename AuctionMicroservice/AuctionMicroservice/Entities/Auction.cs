using AuctionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    /// <summary>
    /// Represents auction
    /// </summary>
    public class Auction
    {
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
        /// List od individual documentations that belong to this auction
        /// </summary>
        public List<DocumentationIndividual> DocumentationIndividual { get; set; }


        /// <summary>
        /// List od legal entity documentations that belong to this auction
        /// </summary>
        public List<DocumentationLegalEntity> DocumentationLegalEntity { get; set; }

        
        /// <summary>
        /// List of public bidding that belong to this auction
        /// </summary>
        public List<PublicBiddingDto> PublicBiddings { get; set; }

        /// <summary>
        /// Date of application deadline
        /// </summary>
        public DateTime ApplicationDeadline { get; set; }


        #endregion
    }
}
