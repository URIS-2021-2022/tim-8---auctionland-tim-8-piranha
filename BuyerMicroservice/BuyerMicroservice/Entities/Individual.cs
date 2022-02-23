using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
<<<<<<< HEAD
    /// <summary>
    /// Individual model (model fizickog lica) 
    /// </summary>
    public class Individual : Buyer
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
=======
    public class Individual : Buyer
    {
        
        public string surname { get; set; }
        
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public string JMBG { get; set; }

       
    }
}
