using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.LegalEntity
{
    public class LegalEntityUpdateDto : BuyerUpdateDto
    {
        public string identificationNumber { get; set; }

        public string fax { get; set; }
    }
}
