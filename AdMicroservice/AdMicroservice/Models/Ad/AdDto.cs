using AdMicroservice.Models.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Oglas DTO
    /// </summary>
    public class AdDto
    {
        /// <summary>
        /// Datum objavljivanja
        /// </summary>
        public string PublicationDate { get; set; }
        /// <summary>
        /// Sluzbeni list
        /// </summary>
        public JournalDto Journal { get; set; }
    }
}
