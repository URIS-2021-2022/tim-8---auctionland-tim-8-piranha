using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
<<<<<<< HEAD
    /// <summary>
    /// Document status confirmation model
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class DocumentStatusConfirmation
    {
        /// <summary>
        /// DocStatusID - ID statusa dokumenta
        /// Example: 07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        [Key]
        public Guid docStatusID { get; set; }

        /// <summary>
        /// Status - Status dokumenta
        /// Example:Usvojen
        /// </summary>
        public string status { get; set; }
    }
}
