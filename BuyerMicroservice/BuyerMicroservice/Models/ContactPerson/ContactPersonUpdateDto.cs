﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.ContactPerson
{
<<<<<<< HEAD
    /// <summary>
    /// Contact person update DTO for communication with user 
    /// </summary>
    public class ContactPersonUpdateDto
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
=======
    public class ContactPersonUpdateDto
    {
        public Guid contactPersonID { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public string phone { get; set; }
    }
}
