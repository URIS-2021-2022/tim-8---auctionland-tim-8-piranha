using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyerMicroservice.Entities;

namespace BuyerMicroservice.Models.AuthorizedPerson
{
<<<<<<< HEAD
    /// <summary>
    /// Authorized person  confirmation DTO model for communication with user
    /// </summary>
    public class AuthorizedPersonConfirmationDto
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
        /// board Number ID from boardNumber entity - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
=======
    public class AuthorizedPersonConfirmationDto
    {
       

        public string name { get; set; }

        public string surname { get; set; }

        public string personalDocNum { get; set; }

        public string address { get; set; }

      
        public string country { get; set; }


>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public Guid boardNumbID { get; set; }
    }
}
