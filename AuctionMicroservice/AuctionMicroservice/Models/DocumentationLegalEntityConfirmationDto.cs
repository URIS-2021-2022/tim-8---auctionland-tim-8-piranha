using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class DocumentationLegalEntityConfirmationDto
    {
        #region
        public Guid DocumentationLegalEntityId { get; set; }

        public string Name { get; set; }

        public string IdentificationNumber { get; set; }

        public string Address { get; set; }

       
        public Guid AuctionId { get; set; }

        #endregion
    }
}
