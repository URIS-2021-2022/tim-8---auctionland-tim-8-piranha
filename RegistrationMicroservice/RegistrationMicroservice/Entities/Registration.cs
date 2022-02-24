using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Entities
{
    /// <summary>
    /// Represents registration  
    /// </summary>
    public class Registration
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
        /// 
        

        public Guid? AuctionId { get; set; }
        /// <summary>
        /// Buyer ID that registration belongs to
        /// </summary>
        public Guid? BuyerId { get; set; }
        #endregion


    }
}
