using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    public class BoardConfirmation
    {
        public Guid BoardId { get; set; }

        public Guid PresidentId { get; set; }

        public Person President { get; set; }
    }
}
