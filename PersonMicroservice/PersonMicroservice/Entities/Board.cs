using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    public class Board
    {
        [Key]
        public Guid BoardId { get; set; } = Guid.NewGuid();

        public Guid PresidentId { get; set; }

        [NotMapped]
        public Person President { get; set; }

        [NotMapped]
        public List<Person> Members { get; set; }
    }
}
