using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class Status
    {   
        public Guid StatusId { get; set; } = Guid.NewGuid();

        public string StatusName { get; set; }
    }
}
