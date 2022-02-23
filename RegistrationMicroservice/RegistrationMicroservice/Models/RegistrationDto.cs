using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Models
{
    /// <summary>
    /// Represents registration model
    /// </summary>
    public class RegistrationDto
    {
        #region
        /// <summary>
        /// Registration ID
        /// </summary>
        public Guid RegistrationId { get; set; }
        /// <summary>
        /// Registration Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Registration location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Auction ID that registration belongs to
        /// </summary>
        public AuctionDto AuctionDto { get; set; }

        /// <summary>
        /// Buyer ID that registration belongs to
        /// </summary>
        public BuyerDto BuyerDto { get; set; }  

        

        #endregion
    }
}
