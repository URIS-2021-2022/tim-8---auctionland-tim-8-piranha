using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    /// <summary>
    /// Represents legal entity documentation confirmation
    /// </summary>
    public class DocumentationLegalEntityConfirmation
    {
        #region
        /// <summary>
        /// legal entity documentation ID
        /// </summary>
        public Guid DocumentationLegalEntityId { get; set; }
        /// <summary>
        /// legal entity name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// legal entity identification number
        /// </summary>
        public string IdentificationNumber { get; set; }
        /// <summary>
        /// legal entity address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ID of auction that this documentation belongs to
        /// </summary>
        public Guid AuctionId { get; set; }

        public Auction Auction { get; set; }

        #endregion
    }
}
