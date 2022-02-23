using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
<<<<<<< HEAD
    /// <summary>
    /// Document status DTO model for commnication with user
    /// </summary>
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class DocumentStatusDto
    {
        /// <summary>
        /// DocStatusID - ID statusa dokumenta
        /// Example: 07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid DocStatusID { get; set; }

        /// <summary>
        /// Status - Status dokumenta
        /// Example:Usvojen
        /// </summary>
        public string Status { get; set; }
    }
}
