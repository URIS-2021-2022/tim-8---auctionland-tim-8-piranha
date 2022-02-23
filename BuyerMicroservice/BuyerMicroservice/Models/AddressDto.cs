using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models
{
    /// <summary>
    /// DTO for address
    /// </summary>
    public class AddressDto
    {

       
        /// <summary>
        /// Buyer name.
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// Street number
        /// </summary>
        public string numberStreet { get; set; }

        /// <summary>
        /// A place of residence
        /// </summary>
        public string place { get; set; }

        /// <summary>
        /// Zip code for place
        /// </summary>
        public string zipCode { get; set; }


    }
}
