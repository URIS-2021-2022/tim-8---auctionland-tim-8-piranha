using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
<<<<<<< HEAD
    /// <summary>
    /// Document model
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
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
<<<<<<< HEAD
        /// Example : "2021-02-01 00:00:00"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public DateTime? documentCreationDate { get; set; }

        /// <summary>
        /// Document date - Datum u dokumenta 
<<<<<<< HEAD
        /// Example : "2021-02-01 00:00:00"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public DateTime? documentDate { get; set; }

        /// <summary>
        /// Document template - Šablon dokumenta
<<<<<<< HEAD
        /// Example : Kreiranje predloga plana 
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public string documentTemplate { get; set; }

        /// <summary>
<<<<<<< HEAD
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
=======
        /// Document status - Status dokumenta 
        /// </summary>
        [ForeignKey("DocumentStatus")]
        public Guid docStatusID { get; set; }
        public DocumentStatus documentStatus { get; set; }

        /// <summary>
        /// Buyer ID. Buyer ID from Buyer microservice.
        /// </summary>
        public Guid? auctionId { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public Guid? userId { get; set; }
    }
}
