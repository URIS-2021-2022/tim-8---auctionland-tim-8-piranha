using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Models.State
{
    public class StateUpdateDto
    {
        public Guid StateId { get; set; }

        public string NameState { get; set; }
    }
}
