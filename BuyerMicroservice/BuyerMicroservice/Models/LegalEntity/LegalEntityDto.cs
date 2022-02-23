using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.LegalEntity
{
    /// <summary>
    /// Legal Entity DTO  model for communication with user 
    /// </summary>
    public class LegalEntityDto : BuyerDto
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
    }
}
