using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
<<<<<<< HEAD
    /// <summary>
    /// Document update DTO model for commnication with user
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
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
<<<<<<< HEAD
        /// Example : "2021-02-01 00:00:00"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public DateTime? DocumentCreationDate { get; set; }

        /// <summary>
        /// Document date - Datum u dokumenta 
<<<<<<< HEAD
        /// Example : "2021-02-01 00:00:00"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Document template - Šablon dokumenta
<<<<<<< HEAD
        /// Example : "2021-02-01 00:00:00"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public string DocumentTemplate { get; set; }

        /// <summary>
        /// Document status - Status dokumenta 
<<<<<<< HEAD
        /// This is ForeignKey from Entity "DocumentStatus"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public Guid DocStatusID { get; set; }

        /// <summary>
<<<<<<< HEAD
        /// Auction ID from Auction microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
=======
        /// Buyer ID.
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public Guid? auctionId { get; set; }

        /// <summary>
<<<<<<< HEAD
        /// User ID from User microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
=======
        /// User ID.
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public Guid? userId { get; set; }


    }
}
