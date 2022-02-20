using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Models
{
    public class PersonCreationDto
    {
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
