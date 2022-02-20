using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    public class BoardDto
    {
        public Guid BoardId { get; set; }

        public PersonDto President { get; set; }

        public List<PersonDto> Members { get; set; }
    }
}
