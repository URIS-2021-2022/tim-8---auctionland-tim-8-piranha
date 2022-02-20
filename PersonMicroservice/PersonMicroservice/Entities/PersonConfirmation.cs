using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    public class PersonConfirmation
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
