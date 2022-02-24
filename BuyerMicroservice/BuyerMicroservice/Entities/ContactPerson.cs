using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class ContactPerson
    {
        [Key]
        public Guid contactPersonID { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string phone { get; set; }
    }
}
