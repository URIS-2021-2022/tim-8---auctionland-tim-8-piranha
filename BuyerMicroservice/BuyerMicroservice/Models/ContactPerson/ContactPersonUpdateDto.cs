using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.ContactPerson
{
    public class ContactPersonUpdateDto
    {
        public Guid contactPersonID { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string phone { get; set; }
    }
}
