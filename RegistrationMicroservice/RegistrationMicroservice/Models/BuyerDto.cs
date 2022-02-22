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
        /// Buyer name
        /// </summary>
        public string BuyerName { get; set; }
        /// <summary>
        /// Buyer address
        /// </summary>
        public string BuyerAddress { get; set; }
        /// <summary>
        /// Buyer phone number
        /// </summary>
        public string BuyerPhoneNumber { get; set; }
        /// <summary>
        /// Buyer account number
        /// </summary>
        public string BuyerAccountNumber { get; set; }
        /// <summary>
        /// Buyer email
        /// </summary>
        public string BuyerEmail { get; set; }

        
        #endregion

    }
}
