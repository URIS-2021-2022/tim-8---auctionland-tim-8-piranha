using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.LegalEntity
{
    public class LegalEntityCreationDto : BuyerCreationDto
    {
        public string identificationNumber { get; set; }

        public string fax { get; set; }
    }
}
