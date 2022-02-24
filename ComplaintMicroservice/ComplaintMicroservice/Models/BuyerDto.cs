using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class BuyerDto
    {
        /// <summary>
        /// Ime kupca
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Adresa kupca
        /// </summary>
        public string addresse { get; set; }
        /// <summary>
        /// Telefon kupca
        /// </summary>
        public string phone1 { get; set; }
        /// <summary>
        /// Mail kupca
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Broj naloga
        /// </summary>
        public string accountNumber { get; set; }
    }
}
