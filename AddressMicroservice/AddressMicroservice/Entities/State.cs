using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Entities
{
    public class State
    {
        [Key]
        public Guid StateID { get; set; }

        public string NameState { get; set; }
    }
}
