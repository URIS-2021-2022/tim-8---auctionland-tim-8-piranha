using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    /// <summary>
    /// Document model
    /// </summary>

    public class Document
    {
        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        [Key]
        public Guid documentId { get; set; }

        /// <summary>
        /// Registration number - broj registracije 
        /// Exaple: 1123232323
        /// </summary>
        public string registrationNumber { get; set; }

        /// <summary>
        /// Document creation date - Datum krairanja dokumenta
        /// Example : "2021-02-01 00:00:00"
        /// </summary>
        public DateTime? documentCreationDate { get; set; }

        /// <summary>
        /// Document date - Datum u dokumenta 
        /// Example : "2021-02-01 00:00:00"
        /// </summary>
        public DateTime? documentDate { get; set; }

        /// <summary>
        /// Document template - Šablon dokumenta
        /// Example : Kreiranje predloga plana 
        /// </summary>
        public string documentTemplate { get; set; }

        /// <summary>
        /// Document status ID 
        /// This is Foreign Key from Entity "DocumentStatus"
        /// </summary>
        [ForeignKey("DocumentStatus")]
        public Guid docStatusID { get; set; }
        /// <summary>
        /// documentStatus - Entity
        /// </summary>
        public DocumentStatus documentStatus { get; set; }

        /// <summary>
        /// Auction ID from Auction microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid? auctionId { get; set; }

        /// <summary>
        /// User ID from User microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid? userId { get; set; }
    }
}
