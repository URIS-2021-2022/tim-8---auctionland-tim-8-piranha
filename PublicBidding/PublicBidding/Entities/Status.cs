using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    /// <summary>
    /// Entitet statusa
    /// </summary>
    public class Status
    {   
        /// <summary>
        /// Id statusa
        /// </summary>
        public Guid StatusId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Naziv statusa
        /// </summary>
        public string? StatusName { get; set; }
    }
}
