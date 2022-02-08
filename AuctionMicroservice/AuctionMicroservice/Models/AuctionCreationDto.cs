using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class AuctionCreationDto
    {
        #region
        public Guid AuctionId { get; set; }


        [Required]
        public int AuctionNum { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Restriction { get; set; }

        [Required]
        public int PriceStep { get; set; }


        //public DocumentationIndividualDto DocumentationIndividual { get; set; }

        //public DocumentationLegalEntityDto DocumentationLegalEntity { get; set; }

        //public PublicBiddingDto PublicBiddings { get; set; }

        [Required]
        public DateTime ApplicationDeadline { get; set; }

        #endregion
    }
}
