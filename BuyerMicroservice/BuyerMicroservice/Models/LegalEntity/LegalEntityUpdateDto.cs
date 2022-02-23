using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.LegalEntity
{
<<<<<<< HEAD
    /// <summary>
    /// Legal Entity update DTO  model for communication with user 
    /// </summary>
    public class LegalEntityUpdateDto : BuyerUpdateDto
    {

        /// <summary>
        /// identification Number - identifikacioni broj 
        /// </summary>
        public string identificationNumber { get; set; }

        /// <summary>
        /// Fax - fax broj pravnog lica 
        /// Example : 212693-2377
        /// </summary>
=======
    public class LegalEntityUpdateDto : BuyerUpdateDto
    {
        public string identificationNumber { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public string fax { get; set; }
    }
}
