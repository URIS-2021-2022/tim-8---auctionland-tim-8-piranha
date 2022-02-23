using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    /// <summary>
    /// Entitet tipa
    /// </summary>
    public class Type
    {
        /// <summary>
        /// Id tipa
        /// </summary>
        public Guid TypeId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Naziv statusa
        /// </summary>
        public string TypeName { get; set; }
    }
}
