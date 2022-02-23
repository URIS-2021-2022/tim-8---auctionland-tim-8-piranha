using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    /// <summary>
    /// Entitet za komisiju
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Id komisije
        /// </summary>
        [Key]
        public Guid BoardId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Id predsednika komisije
        /// </summary>
        public Guid PresidentId { get; set; }
        /// <summary>
        /// Predsednik komisije
        /// </summary>
        [NotMapped]
        public Person President { get; set; }
        /// <summary>
        /// List članova komisije
        /// </summary>
        [NotMapped]
        public List<Person> Members { get; set; }
    }
}
