using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public record BoardNumber 
    {
        public Guid authorizedPersonID { get; set; }
        public int number { get; set; }

        
    }
}
