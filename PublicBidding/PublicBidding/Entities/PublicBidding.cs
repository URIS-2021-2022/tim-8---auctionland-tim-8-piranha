using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class PublicBidding
    {
        [Key]
        public Guid PublicBiddingId { get; set; }

        public Guid TypeId { get; set; }

        public Type Type { get; set; }

        public Guid StatusId { get; set; }

        public Status Status { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StartPricePerHa { get; set; }

        public bool IsExcepted{ get; set; }

        public Guid AddressId { get; set; }

        public List<Guid> Bidders { get; set; }

        public List<Guid> Plots { get; set; }

        public int Price { get; set; }

        public Guid? BestBidder { get; set; }

        public int RentPeriod { get; set; }

        public int NumberOfApplicants { get; set; }

        public int DepositSupplement { get; set; }

        public int Round { get; set; }

    }
}
