using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Individual
{
<<<<<<< HEAD
    /// <summary>
    /// Individual confirmation DTO model for communication with user 
    /// </summary>
    public class IndividualConfirmationDto : BuyerConfirmationDto
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
    public class IndividualConfirmationDto : BuyerConfirmationDto
    {
        public string surname { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public string JMBG { get; set; }
    }
}
