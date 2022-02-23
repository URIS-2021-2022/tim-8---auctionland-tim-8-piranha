using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    /// <summary>
    /// Model za potvrdu kreiranja komisije
    /// </summary>
    public class BoardConfirmationDto
    {
        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public string President { get; set; }
    }
}
