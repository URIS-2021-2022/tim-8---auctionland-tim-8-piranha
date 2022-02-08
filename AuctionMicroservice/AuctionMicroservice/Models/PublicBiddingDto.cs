using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class PublicBiddingDto
    {
        #region
        [Key]
        public Guid PublicBiddingId { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int BegginingPriceByHectare { get; set; }

        public bool Skipped { get; set; }

        public int AuctionedPrice { get; set; }

        public int LeasePeriod { get; set; }

        public int ContestantsNumber { get; set; }

        public int DepositAdditionPrice { get; set; }

        public int Round { get; set; }

        
        public Guid AuctionId { get; set; }

        public Auction auction { get; set; }

        #endregion

    }
}
