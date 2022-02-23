using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    /// <summary>
    /// Model za modifikaciju obelezja
    /// </summary>
    public class PersonUpdateDto
    {
        /// <summary>
        /// Id ličnosti
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        [Required(ErrorMessage = "Ime licnosti je obavezno obelezje.")]
        public string Name { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        [Required(ErrorMessage = "Prezime licnosti je obavezno obelezje.")]
        public string Surname { get; set; }

        /// <summary>
        /// Funkcija ličnosti
        /// </summary>
        [Required(ErrorMessage = "Funkcija licnosti je obavezno polje.")]
        public string Function { get; set; }
    }
}
