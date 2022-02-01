using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class AuctionDto
    {
        #region

        public Guid AuctionId { get; set; }

        public int AuctionNum { get; set; }

        public int Year { get; set; }

        public DateTime Date { get; set; }

        public int Restriction { get; set; }

        public int PriceStep { get; set; }

        public List<DocumentationIndividualDto> DocumentationIndividual { get; set; }

        public List<DocumentationLegalEntityDto> DocumentationLegalEntity { get; set; }

        public List<PublicBiddingDto> PublicBiddings { get; set; }


        #endregion
    }
}
