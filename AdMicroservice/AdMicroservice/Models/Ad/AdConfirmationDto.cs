using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Potvrda oglasa DTO
    /// </summary>
    public class AdConfirmationDto
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
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid PublicBiddingId { get; set; }
    }
}
