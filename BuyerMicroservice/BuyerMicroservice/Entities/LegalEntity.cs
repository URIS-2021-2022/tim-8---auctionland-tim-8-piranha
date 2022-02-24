using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class LegalEntity : Buyer
    {

        public string identificationNumber { get; set; }

        public string fax { get; set; }

        
    }
}
