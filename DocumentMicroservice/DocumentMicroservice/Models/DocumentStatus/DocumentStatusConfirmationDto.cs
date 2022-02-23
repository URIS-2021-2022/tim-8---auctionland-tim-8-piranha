using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    /// <summary>
    /// Document status confirmation DTO model for commnication with user
    /// </summary>

    public class DocumentStatusConfirmationDto
    {

        /// <summary>
        /// Status - Status dokumenta
        /// Example:Usvojen
        /// </summary>
        public string Status { get; set; }
    }
}
