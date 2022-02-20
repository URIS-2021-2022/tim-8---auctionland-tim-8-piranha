using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities.Ad
{
    /// <summary>
    /// Potvrda oglasa
    /// </summary>
    public class AdConfirmation
    {
        /// <summary>
        /// ID oglasa
        /// </summary>
        public Guid AdId { get; set; }
        /// <summary>
        /// Datum objavljivanja
        /// </summary>
        public string PublicationDate { get; set; }
        /// <summary>
        /// ID sluzbenog lista
        /// </summary>
        public Guid JournalId { get; set; }
    }
}
