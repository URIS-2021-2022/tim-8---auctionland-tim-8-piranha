using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{

    /// <summary>
    /// Buyer confirmation model 
    /// </summary>
    public abstract class BuyerConfirmation
    {
        /// <summary>
        /// Buyer ID - ID kupca
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>     
        public Guid buyerID { get; set; }

        /// <summary>
        /// Priority ID(Foreign key) from priority entity
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid priorityID { get; set; }
        /// <summary>
        /// Priority - entity model
        /// </summary>
        public Priority priority { get; set; }
        /// <summary>
        /// Realized area - realizovana oblast 
        /// Example : 2000
        /// </summary>
        public int realizedArea { get; set; }

        /// <summary>
        /// Authorized person- lista ovlascenih lica
        /// </summary>
        public ICollection<AuthorizedPerson> authorizedPerson { get; set; }
        /// <summary>
        /// Has Ban - Ima ban(true)/ nema ban(false)
        /// Example : true 
        /// </summary>
        public bool hasBan { get; set; }
        /// <summary>
        /// Start date of ban - datum pocetka zabrane
        /// Example : "2019-01-01 00:00:00"
        /// </summary>
        public DateTime? startDateOfBan { get; set; }
        /// <summary>
        /// Duration Of Ban In Year- Trajanje zabrane u godinama
        /// Example : 2
        /// </summary>
        public int durationOfBanInYear { get; set; }
        /// <summary>
        /// End date od ban - datum zavrsetka zabrane
        /// Example : "2020-01-01 00:00:00"
        /// </summary>
        public DateTime? endDateOfBan { get; set; }

        /// <summary>
        /// Name -  kupca
        /// Example : Dimitrije
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Address - adresa stanovanja
        /// Example : Mira Popare
        /// </summary>
        public string addresse { get; set; }
        /// <summary>
        /// Phone 1
        /// Example : 065768576
        /// </summary>
        public string phone1 { get; set; }
        /// <summary>
        /// Phone 2
        /// Example : 065768576
        /// </summary>
        public string phone2 { get; set; }
        /// <summary>
        /// Email
        /// Example : corlija.dimitrije@gmail.com
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Account Number - broj racuna
        /// Example : 0074234876
        /// </summary>
        public string accountNumber { get; set; }

        /// <summary>
        /// Address ID(address entity) from Address microservice.
        /// </summary>
   


    }
}
