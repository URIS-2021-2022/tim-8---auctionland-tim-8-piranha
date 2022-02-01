using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class DocumentationLegalEntityDto
    {
        #region
        public Guid DocumentationLegalEntityId { get; set; }

        public string Name { get; set; }

        public string IdentificationNumber { get; set; }

        public string Address { get; set; }

        #endregion
    }
}
