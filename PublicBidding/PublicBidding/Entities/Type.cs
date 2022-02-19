using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class Type
    {
        public Guid TypeId { get; set; } = Guid.NewGuid();

        public string TypeName { get; set; }
    }
}
