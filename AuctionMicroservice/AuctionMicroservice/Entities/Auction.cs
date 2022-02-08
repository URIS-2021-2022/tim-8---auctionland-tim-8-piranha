﻿using AuctionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class Auction
    {
        #region
        
        public Guid AuctionId { get; set; }

        public int AuctionNum { get; set; }

        public int Year { get; set; }

        public DateTime Date { get; set; }

        public int Restriction { get; set; }

        public int PriceStep { get; set; }

        public List<DocumentationIndividual> DocumentationIndividual { get; set; }

        //public Guid DocumentationLegalEntityId { get; set; }

        public List<DocumentationLegalEntity> DocumentationLegalEntity { get; set; }

        //public Guid DocumentationIndividualId { get; set; }

        public List<PublicBiddingDto> PublicBiddings { get; set; }

        //public Guid PublicBiddingDtoId { get; set; }
        public DateTime ApplicationDeadline { get; set; }


        #endregion
    }
}
