using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Oglas DTO za update
    /// </summary>
    public class AdUpdateDto
    {
        /// <summary>
        /// ID oglasa
        /// </summary>
        public Guid AdId { get; set; }
        /// <summary>
        /// Datum objavljivanja
        /// </summary>
        [Required(ErrorMessage = "Must enter a date of publication")]
        public string PublicationDate { get; set; }
        /// <summary>
        /// ID sluzbenog lista
        /// </summary>
        public Guid JournalId { get; set; }
    }
}
