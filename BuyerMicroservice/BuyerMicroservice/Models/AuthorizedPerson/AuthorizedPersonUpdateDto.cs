using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.AuthorizedPerson
{

    /// <summary>
    /// Authorized person update DTO model for communication with user
    /// </summary>
    public class AuthorizedPersonUpdateDto
    {
        /// <summary>
        /// Authorized person ID ID - ID OVLASCENOG LICA
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid authorizedPersonID { get; set; }

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
        /// personalDocNum - Broj licnog dokumenta
        /// Example : 8767834637274
        /// </summary>
        public string personalDocNum { get; set; }
        /// <summary>
        /// Address- adresa ovlascenog lica
        /// Example : Partizanski put 10
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Country - zemlja stanovanja ovlascenog lica 
        /// Example : Srbija
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// board Number ID(Foreign key) from boardNumber entity - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid boardNumbID { get; set; }
    }
}
