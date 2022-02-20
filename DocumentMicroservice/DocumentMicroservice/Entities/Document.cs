using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class Document
    {
        /// <summary>
        /// ID dokumenta
        /// Example:07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        [Key]
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Registration number - broj registracije 
        /// Exaple: 1123232323
        /// </summary>
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
        [ForeignKey("DocumentStatus")]
        public Guid DocStatusID { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

    }
}
