using AdMicroservice.Entities.Journal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities.Ad
{
    /// <summary>
    /// Predstavlja oglas
    /// </summary>
    public class AdModel
    {
        /// <summary>
        /// ID oglasa
        /// </summary>
        [Key]
        public Guid AdId { get; set; }
        /// <summary>
        /// Datum objavljivanja
        /// </summary>
        public string PublicationDate { get; set; }
        /// <summary>
        /// ID sluzbenog lista
        /// </summary>
        public Guid JournalId { get; set; }
        /// <summary>
        /// Sluzbeni list
        /// </summary>
        public JournalModel Journal { get; set; }
    }
}
