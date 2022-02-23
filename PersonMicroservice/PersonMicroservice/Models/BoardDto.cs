using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    /// <summary>
    /// Ime ličnosti
    /// </summary>
    public class BoardDto
    {
        /// <summary>
        /// Id komisije
        /// </summary>
        public Guid BoardId { get; set; }

        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public PersonDto President { get; set; }

        /// <summary>
        /// Članovi komisije
        /// </summary>
        public List<PersonDto> Members { get; set; }
    }
}
