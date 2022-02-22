using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
    public class DocumentUpdateDto
    {
        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Registration number - broj registracije 
        /// Exaple: 1123232323
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Document creation date - Datum krairanja dokumenta
        /// </summary>
        public DateTime? DocumentCreationDate { get; set; }

        /// <summary>
        /// Document date - Datum u dokumenta 
        /// </summary>
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Document template - Šablon dokumenta
        /// </summary>
        public string DocumentTemplate { get; set; }

        /// <summary>
        /// Document status - Status dokumenta 
        /// </summary>
        public Guid DocStatusID { get; set; }

        /// <summary>
        /// Buyer ID.
        /// </summary>
        public Guid? auctionId { get; set; }

        /// <summary>
        /// User ID.
        /// </summary>
        public Guid? userId { get; set; }


    }
}
