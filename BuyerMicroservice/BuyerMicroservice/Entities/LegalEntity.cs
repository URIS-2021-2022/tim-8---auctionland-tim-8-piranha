using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    /// <summary>
    /// Legal Entity model (model pravnog lica) 
    /// </summary>
    public class LegalEntity : Buyer
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
