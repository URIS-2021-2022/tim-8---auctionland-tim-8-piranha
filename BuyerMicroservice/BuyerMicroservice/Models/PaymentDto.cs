using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models
{
    /// <summary>
    /// Paymen DTO
    /// </summary>
    public class PaymentDto
    {

      
        /// <summary>
        /// Account number
        /// </summary>
        public string accountNumber { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        public string referenceNumber { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// Purpose of Payment
        /// </summary>
        public string purposeOfPayment { get; set; }

    }
}
