using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    /// <summary>
    /// Dto kojim se potvrdjuje da je javno nadmetanje kreirano
    /// </summary>
    public class PublicBiddingConfirmationDto
    {
        /// <summary>
        /// Pocetna cena po hektaru
        /// </summary>
        public double StartPricePerHa { get; set; }
        /// <summary>
        /// Vremensi period zakupa
        /// </summary>
        public int RentPeriod { get; set; }
        /// <summary>
        /// Broj kruga
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// Visina dopune depozita
        /// </summary>
        public int DepositSupplement { get; set; }
    }
}
