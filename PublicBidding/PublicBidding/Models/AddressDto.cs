using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    public class AddressDto
    {
        public string? Street { get; set; }
        public string? AddressNumber { get; set; }
        public string? City { get; set; }
        public long PostalCode { get; set; }
        public string? Country { get; set; }
    }
}
