using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Buyer
{
<<<<<<< HEAD
    /// <summary>
    /// Buyer Dto model for communication with user 
    /// </summary>
    public class BuyerDto
    {
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
        /// Account Number - broj racuna
        /// Example : 0074234876
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Address ID(Address entity) from address microservice
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public string accountNumber { get; set; }
        /// <summary>
        ///Address( AddressDto) from address microservice
        /// </summary>
        public AddressDto address { get; set; }
        /// <summary>
        /// Payment(PaymentDto) from payment microservice 
        /// </summary>
=======
    public class BuyerDto
    {
        public string name { get; set; }

        public string addresse { get; set; }

        public string phone1 { get; set; }

        public string email { get; set; }

        public string accountNumber { get; set; }

        public AddressDto address { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public PaymentDto payment { get; set; }
    }
}
