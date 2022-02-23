using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Journal
{
    /// <summary>
    /// Sluzbeni list DTO za update
    /// </summary>
    public class JournalUpdateDto
    {
        /// <summary>
        /// ID sluzbenog lista
        /// </summary>
        public Guid JournalId { get; set; }
        /// <summary>
        /// Broj sluzbenog lista
        /// </summary>
        [Required(ErrorMessage = "Must enter a journal number")]
        public string JournalNumber { get; set; }
        /// <summary>
        /// Opstina
        /// </summary>
        [Required(ErrorMessage = "Must enter a municipality")]
        public string Municipality { get; set; }
        /// <summary>
        /// Datum izdavanja
        /// </summary>
        [Required(ErrorMessage = "Must enter a date of issue")]
        public string DateOfIssue { get; set; }
    }
}
