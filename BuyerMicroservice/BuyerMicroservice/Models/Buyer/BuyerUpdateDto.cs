using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Buyer
{
    /// <summary>
    /// Buyer update Dto model for communication with user 
    /// </summary>
    public class BuyerUpdateDto
    {
        /// <summary>
        /// Buyer ID - ID kupca
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid buyerID { get; set; }
        /// <summary>
        /// Priority ID from priority entity
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid priorityID { get; set; }
        /// <summary>
        /// Realized area - realizovana oblast 
        /// Example : 2000
        /// </summary>
        public int realizedArea { get; set; }
        /// <summary>
        /// Authorized Person ID (ID ovlascenog lica)
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid authorizedPersonID { get; set; }
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
        /// Name
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
        /// Address ID(Address entity) from address microservice
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid? addressId { get; set; }
        /// <summary>
        /// Payment ID(Payment entity) from payment microservice
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid? paymentId { get; set; }
    }
}
