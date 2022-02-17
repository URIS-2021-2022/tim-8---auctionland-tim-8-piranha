using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Entities
{
    public class Registration
    {
        #region
        public Guid RegistrationId { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public Guid AuctionId { get; set; }
        public Auction auction { get; set; }
        
        public Guid BuyerId { get; set; }
        public Buyer buyer { get; set; }
        #endregion


    }
}
