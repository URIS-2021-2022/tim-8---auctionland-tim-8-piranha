using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class DocumentationIndividualConformation
    {
        #region
        
        public Guid DocumentationIndividualId { get; set; }
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string IdentificationNumber { get; set; }

        
        public Guid AuctionId { get; set; }

        public Auction Auction { get; set; }

        #endregion
    }
}
