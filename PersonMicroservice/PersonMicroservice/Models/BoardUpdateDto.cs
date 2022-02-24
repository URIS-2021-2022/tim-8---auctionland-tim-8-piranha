using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    public class BoardUpdateDto
    {
        public Guid BoardId { get; set; }

        public Guid President { get; set; }

        public List<Guid> Members { get; set; }
    }
}
