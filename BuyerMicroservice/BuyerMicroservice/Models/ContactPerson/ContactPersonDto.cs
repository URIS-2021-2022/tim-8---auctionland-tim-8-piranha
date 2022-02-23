using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.ContactPerson
{
    /// <summary>
    /// Contact person DTO for communication with user 
    /// </summary>
    public class ContactPersonDto
    {
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
