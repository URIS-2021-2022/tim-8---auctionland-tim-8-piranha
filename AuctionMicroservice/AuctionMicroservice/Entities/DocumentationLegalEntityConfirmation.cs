using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class DocumentationLegalEntityConfirmation
    {
        #region
        public Guid DocumentationLegalEntityId { get; set; }

        public string Name { get; set; }

        public string IdentificationNumber { get; set; }

        public string Address { get; set; }

        public Guid AuctionId { get; set; }

        public Auction Auction { get; set; }

        #endregion
    }
}
