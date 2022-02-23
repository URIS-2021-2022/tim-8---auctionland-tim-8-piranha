using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Individual
{
    /// <summary>
    /// Individual update DTO model for communication with user 
    /// </summary>
    public class IndividualUpdateDto : BuyerUpdateDto
    {
        /// <summary>
        /// Surename - prezime ovlascenog lica
        /// Example : Corlija
        /// </summary>
        public string surname { get; set; }
        /// <summary>
        /// JMBG -jedinstvena identifikacija fizickog lica
        /// Example : 1187999876656
        /// </summary>
        public string JMBG { get; set; }
    }
}
