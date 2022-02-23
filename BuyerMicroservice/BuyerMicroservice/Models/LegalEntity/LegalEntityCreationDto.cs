using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.LegalEntity
{
<<<<<<< HEAD
    /// <summary>
    /// Legal Entity creation DTO  model for communication with user 
    /// </summary>
    public class LegalEntityCreationDto : BuyerCreationDto
    {
        /// <summary>
        /// identification Number - identifikacioni broj 
        /// </summary>
        public string identificationNumber { get; set; }

        /// <summary>
        /// Fax - fax broj pravnog lica 
        /// Example : 212693-2377
        /// </summary>
        public string fax { get; set; }
        
=======
    public class LegalEntityCreationDto : BuyerCreationDto
    {
        public string identificationNumber { get; set; }

        public string fax { get; set; }
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    }
}
