using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    /// <summary>
    /// Buyer DTO - Model za kupca sistema
    /// </summary>

    public class BuyerDto
    {
        /// <summary>
        /// Buyer name.
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// Buyer address (ex. Pap pavla 15)
        /// </summary>
        public string BuyerAddress { get; set; }

        /// <summary>
        /// Buyer phone number (ex. 0603802918)
        /// </summary>
        public string BuyerPhoneNumber { get; set; }

        /// <summary>
        /// Buyer account number (ex. 1234567890)
        /// </summary>
        public string BuyerAccountNumber { get; set; }

        /// <summary>
        /// Buyer email (ex. test@gmail.com)
        /// </summary>
        public string BuyerEmail { get; set; }
    }
}
