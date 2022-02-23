using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Journal
{
    /// <summary>
    /// Sluzbeni list DTO
    /// </summary>
    public class JournalDto
    {
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
