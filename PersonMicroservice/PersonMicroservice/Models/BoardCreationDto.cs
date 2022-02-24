using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje komisije
    /// </summary>
    public class BoardCreationDto
    {
        /// <summary>
        /// Id komisije
        /// </summary>
        public Guid BoardId { get; set; }

        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public Guid President { get; set; }

        /// <summary>
        /// Članovi komisije
        /// </summary>
        public List<Guid> Members { get; set; }
    }
}
