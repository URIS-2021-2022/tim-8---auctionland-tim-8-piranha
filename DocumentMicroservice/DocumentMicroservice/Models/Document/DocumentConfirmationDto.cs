using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
    /// <summary>
    /// Document confirmation DTO model for commnication with user
    /// </summary>
    public class DocumentConfirmationDto
    {
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

    }
}
