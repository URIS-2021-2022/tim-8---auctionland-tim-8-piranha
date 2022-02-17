using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Models
{
    public class RegistrationUpdateDto
    {
        public Guid RegistrationId { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public Guid AuctionId { get; set; }


        public Guid BuyerId { get; set; }

    }
}
