﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{


    /// <summary>
    /// Contact person confirmation model 
    /// </summary>
    public class ContactPersonConfirmation
    {
        /// <summary>
        /// contactPersonID - ID kontakt osobe 
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid contactPersonID { get; set; }

        /// <summary>
        /// Name - ovlascenog lica
        /// Example : Dimitrije
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Surename - prezime ovlascenog lica
        /// Example : Corlija
        /// </summary>
        public string surname { get; set; }
        /// <summary>
        /// Phone
        /// Example : 065768576
        /// </summary>
        public string phone { get; set; }
    }
}
