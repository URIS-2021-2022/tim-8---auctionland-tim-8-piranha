using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
    /// <summary>
    /// Document DTO model for commnication with user
    /// </summary>
    public class DocumentDto
    {

        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid documentId { get; set; }
        public Guid DocumentId { get; set; }

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
        /// Example :07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid docStatusID { get; set; }

        /// <summary>
        /// Auction DTO - Auction model from Auction microservice
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
        /// Auction
        /// </summary>
        public AuctionDto auction { get; set; }

        /// <summary>
        /// User DTO - User model from User microservice
        /// </summary>
        public UserDto user { get; set; }

    }
}
