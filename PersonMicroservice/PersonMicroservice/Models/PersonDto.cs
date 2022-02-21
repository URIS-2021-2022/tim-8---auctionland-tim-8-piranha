using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    /// <summary>
    /// Model za ličnost
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Id ličnosti
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Funkcija ličnosti
        /// </summary>
        public string Function { get; set; }
    }
}
