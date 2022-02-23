using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    /// <summary>
    /// Individual model confirmation (confirmation model fizickog lica) 
    /// </summary>
    public class IndividualConfirmation : BuyerConfirmation
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
