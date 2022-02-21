using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    /// <summary>
    /// Model za potvrdu kreiranja ličnosti
    /// </summary>
    public class PersonConfirmationDto
    {
        /// <summary>
        /// Ime ličnosti
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        public string Surname { get; set; }
    }
}
