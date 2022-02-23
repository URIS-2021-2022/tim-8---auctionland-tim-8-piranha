﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
<<<<<<< HEAD
    /// <summary>
    /// Document DTO model for commnication with user
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class DocumentDto
    {

        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
<<<<<<< HEAD
        public Guid documentId { get; set; }
=======
        public Guid DocumentId { get; set; }
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a

        /// <summary>
        /// Registration number - broj registracije 
        /// Exaple: 1123232323
        /// </summary>
<<<<<<< HEAD
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
=======
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
        /// Buyer DTO.
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public AuctionDto auction { get; set; }

        /// <summary>
<<<<<<< HEAD
        /// User DTO - User model from User microservice
=======
        /// User DTO
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public UserDto user { get; set; }

    }
}
