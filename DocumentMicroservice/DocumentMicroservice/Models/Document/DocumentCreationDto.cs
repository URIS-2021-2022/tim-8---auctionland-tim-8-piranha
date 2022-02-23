﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
    /// <summary>
    /// Document creation DTO model for commnication with user
    /// </summary>
    public class DocumentCreationDto
    {
        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid documentId { get; set; }


      
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
        /// Document status ID 
        /// This is Foreign Key from Entity "DocumentStatus"
        /// Example : Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid docStatusID { get; set; }

        /// <summary>
        /// Auction ID from Auction microservice.
        /// This is optional attribute
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// Document status - Status dokumenta 
        /// </summary>
        public Guid DocStatusID { get; set; }

        /// <summary>
        /// Auction ID.
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
