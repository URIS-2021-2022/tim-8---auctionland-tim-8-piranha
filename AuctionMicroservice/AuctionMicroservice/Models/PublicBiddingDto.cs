using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    /// <summary>
    /// Represents public bidding model
    /// </summary>
    public class PublicBiddingDto
    {
        /// <summary>
        /// public bidding ID
        /// </summary>
        #region
       
        /// <summary>
        /// Public bidding date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Public bidding start time
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Public bidding end time
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Public bidding beggining price
        /// </summary>
        public double StartPricePerHa { get; set; }
        /// <summary>
        /// Skipped item
        /// </summary>
        public bool IsExcepted { get; set; }
        /// <summary>
        /// Auctioned price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Lease period for plot
        /// </summary>
        public int RentPeriod { get; set; }
        /// <summary>
        /// Number of contestants
        /// </summary>
        public int NumberOfApplicants { get; set; }
        /// <summary>
        /// Price od addition to deposit
        /// </summary>
        public double DepositSupplement { get; set; }
        /// <summary>
        /// Round number
        /// </summary>
        public int Round { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
        /// <summary>
        /// ID of auction that public bidding belongs to
        /// </summary>
        

        #endregion

    }
}
