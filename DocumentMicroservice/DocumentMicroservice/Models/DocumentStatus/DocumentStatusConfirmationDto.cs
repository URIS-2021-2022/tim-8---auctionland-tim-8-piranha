using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class DocumentStatusConfirmationDto
    {

        /// <summary>
        /// Status - Status dokumenta
        /// Example:Usvojen
        /// </summary>
        public string Status { get; set; }
    }
}
