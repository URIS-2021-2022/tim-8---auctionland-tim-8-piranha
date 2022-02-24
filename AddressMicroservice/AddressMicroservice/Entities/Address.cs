using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Entities
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Place { get; set; }

        public string ZipCode { get; set; }

        [ForeignKey("State")]
        public Guid StateID { get; set; }

        public State State { get; set; }
    }
}
