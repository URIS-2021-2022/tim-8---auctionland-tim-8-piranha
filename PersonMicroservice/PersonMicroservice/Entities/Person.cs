using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Obavezno je uneti ime ličnosti.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti prezime ličnosti.")]
        public string Surname { get; set; }

        public string Function { get; set; }

        public List<Board> Boards { get; set; }
    }
}
