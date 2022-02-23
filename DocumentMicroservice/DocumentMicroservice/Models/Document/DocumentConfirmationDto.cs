using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.Document
{
<<<<<<< HEAD
    /// <summary>
    /// Document confirmation DTO model for commnication with user
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class DocumentConfirmationDto
    {
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

    }
}
