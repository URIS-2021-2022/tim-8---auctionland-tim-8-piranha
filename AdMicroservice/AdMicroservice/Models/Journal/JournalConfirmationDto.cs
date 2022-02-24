using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Journal
{
    /// <summary>
    /// Potvrda sluzbenog lista DTO
    /// </summary>
    public class JournalConfirmationDto
    {
        /// <summary>
        /// ID sluzbenog lista
        /// </summary>
        public Guid JournalId { get; set; }
        /// <summary>
        /// Broj sluzbenog lista
        /// </summary>
        public string JournalNumber { get; set; }
        /// <summary>
        /// Opstina
        /// </summary>
        public string Municipality { get; set; }
        /// <summary>
        /// Datum izdavanja
        /// </summary>
        public string DateOfIssue { get; set; }
    }
}
