using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Models
{
    public class PublicBiddingDto
    {
        /// <summary>
        /// Model javnog nadmetanja za druge servise
        /// </summary>
        /// <summary>
        /// Tip javnog nadmetanja
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Status pocetka javnog nadmetanja
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Vreme pocetka javnog nadmetanja
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Vreme kraja javnog nadmetanja
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Datum odrzavanja javnog nadmetanja
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Pocetna cena parcele
        /// </summary>
        public double StartPricePerHa { get; set; }

        /// <summary>
        /// Adresa odrzavanja javnog nadmetanja
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Period zakupa
        /// </summary>
        public int RentPeriod { get; set; }

        /// <summary>
        /// Doplata depozita
        /// </summary>
        public double DepositSupplement { get; set; }
    }
}
