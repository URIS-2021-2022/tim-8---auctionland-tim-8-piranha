using AuctionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class AuctionConfirmation
    {
        #region

        public Guid AuctionId { get; set; }

        public int AuctionNum { get; set; }

        public int Year { get; set; }

        public DateTime Date { get; set; }

        public int Restriction { get; set; }

        public int PriceStep { get; set; }

        //public DocumentationIndividual DocumentationIndividual { get; set; }

        //public DocumentationLegalEntity DocumentationLegalEntity { get; set; }

        //public PublicBiddingDto PublicBiddings { get; set; }

        public DateTime ApplicationDeadline { get; set; }
        #endregion
    }
}
