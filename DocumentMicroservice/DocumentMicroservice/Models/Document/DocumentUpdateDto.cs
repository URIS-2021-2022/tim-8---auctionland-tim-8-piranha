using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{

    /// <summary>
    /// Document update DTO model for commnication with user
    /// </summary>
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
        /// Example : "2021-02-01 00:00:00"
        /// </summary>
        public DateTime? DocumentCreationDate { get; set; }

        /// <summary>
        /// Document date - Datum u dokumenta 
        /// Example : "2021-02-01 00:00:00"
        /// </summary>
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Document template - Šablon dokumenta
        /// Example : "2021-02-01 00:00:00"
        /// </summary>
        public string DocumentTemplate { get; set; }

        /// <summary>
        /// Document status - Status dokumenta 
        /// This is ForeignKey from Entity "DocumentStatus"
        /// </summary>
        public Guid DocStatusID { get; set; }

        /// <summary>
        /// Auction ID from Auction microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        public Guid? auctionId { get; set; }

        /// <summary>
        /// User ID from User microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid? userId { get; set; }


    }
}
