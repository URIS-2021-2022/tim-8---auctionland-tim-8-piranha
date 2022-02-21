using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Models
{
    /// <summary>
    /// Represents buyer  model
    /// </summary>
    public class BuyerDto
    {
        #region
        /// <summary>
        /// Buyer ID
        /// </summary>
        //public Guid BuyerId { get; set; }
        /// <summary>
        /// Surface bought by buyer
        /// </summary>
        public string BuyerName { get; set; }
        /// <summary>
        /// Restriction start
        /// </summary>
        public string BuyerAddress { get; set; }
        /// <summary>
        /// Restriciton period
        /// </summary>
        public string BuyerPhoneNumber { get; set; }
        /// <summary>
        /// Restriction end
        /// </summary>
        public string BuyerAccountNumber { get; set; }

        public string BuyerEmail { get; set; }

        
        #endregion

    }
}
