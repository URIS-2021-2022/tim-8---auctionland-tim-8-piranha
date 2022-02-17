using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Entities
{
    public class RegistrationConfirmation
    {
        #region
        public Guid RegistrationId { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public Guid AuctionId { get; set; }

        public Guid BuyerId { get; set; }

        

        #endregion
    }
}
