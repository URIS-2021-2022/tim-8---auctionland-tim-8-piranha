using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    /// <summary>
    /// Dto javnog nadmetanja
    /// </summary>
    public class PublicBiddingDto
    {
        public Guid PublicBiddingId { get; set; }

        public Entities.Type Type { get; set; }

        public Status Status { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StartPricePerHa { get; set; }

        public bool IsExcepted { get; set; }

        public AddressDto AddressId { get; set; }

        public int Price { get; set; }

        public BuyerDto BestBidder { get; set; }

        public int RentPeriod { get; set; }

        public int NumberOfApplicants { get; set; }

        public int DepositSupplement { get; set; }

        public int Round { get; set; }

        /// <summary>
        /// Lista delova parcela
        /// </summary>
        public List<PlotPartDto> PlotParts { get; set; }

        /// <summary>
        /// Lista ovlascenih lica
        /// </summary>
        public List<AuthorizedPersonDto> AuthorizedPersons { get; set; }
        /// <summary>
        /// Lista kupaca
        /// </summary>
        public List<BuyerDto> Buyers { get; set; }
    }
}
