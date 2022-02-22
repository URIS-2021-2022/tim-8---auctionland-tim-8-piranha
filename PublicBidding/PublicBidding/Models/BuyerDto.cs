using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    /// <summary>
    /// Dto za kupca
    /// </summary>
    public class BuyerDto
    {
        /// <summary>
        /// Naziv kupca
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Broj telefona kupca
        /// </summary>
        public string phone1 { get; set; }
        /// <summary>
        /// Emali kupca
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Broj računa kupca
        /// </summary>
        public string accountNumber { get; set; }
        /// <summary>
        /// Adresa kupca
        /// </summary>
        public AddressDto address { get; set; }
    }
}
