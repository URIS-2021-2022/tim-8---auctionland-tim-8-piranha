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
    /// Document confirmation model 
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class DocumentConfirmation
    {
        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
      
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
        /// Document status - Status dokumenta 
<<<<<<< HEAD
        /// This is ForeignKey from Entity "DocumentStatus"
        /// </summary>

=======
        /// </summary>
        
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public Guid docStatusID { get; set; }

        
    }
}
