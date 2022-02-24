using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Models.Address
{
    public class AddressConfirmationDto
    { 
        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public Guid StateID { get; set; }
    }
}
