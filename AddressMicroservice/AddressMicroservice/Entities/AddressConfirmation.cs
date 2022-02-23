using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Entities
{
    public class AddressConfirmation
    {
        public Guid AddressId { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Place { get; set; }

        public string ZipCode { get; set; }

        public Guid StateID { get; set; }
    }
}
