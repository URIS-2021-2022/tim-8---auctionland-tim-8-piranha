using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    public class BuyerDto
    {
        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid BuyerId { get; set; }
        /// <summary>
        /// Naziv kupca
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Broj telefona kupca
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Emali kupca
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Broj računa kupca
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
