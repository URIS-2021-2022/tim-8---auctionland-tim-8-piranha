using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class AuctionConformationDto
    {
        #region

        public Guid AuctionId { get; set; }

        public int AuctionNum { get; set; }

        public DateTime Date { get; set; }

        #endregion
    }
}
