using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    public class Board
    {
        [Key]
        public Guid BoardId { get; set; } = Guid.NewGuid();

        public Guid PresidentId { get; set; }

        public Person President { get; set; }

        public List<Person> Members { get; set; }
    }
}
