using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    /// <summary>
    /// Entitet za ličnost
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Id ličnosti
        /// </summary>
        [Key]
        public Guid PersonId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Ime ličnosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime ličnosti.")]
        public string Name { get; set; }
        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime ličnosti.")]
        public string Surname { get; set; }
        /// <summary>
        /// funkcija ličnosti
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// Lista komisija u kojima se nalazi ličnost
        /// </summary>
        [NotMapped]
        public List<Board> Boards { get; set; }
    }
}
