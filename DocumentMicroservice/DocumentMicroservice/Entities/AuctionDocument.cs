using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class AuctionDocument
    {
        [Key]
        public int AuctionNumber { get; set; }

        public DateTime AuctionDate { get; set; }


    }
}
