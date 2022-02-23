using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Models.Address
{
    public class AddressDto
    {
        public Guid AddressId { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Place { get; set; }

        public string ZipCode { get; set; }

        public Entities.State State { get; set; }
    }
}
