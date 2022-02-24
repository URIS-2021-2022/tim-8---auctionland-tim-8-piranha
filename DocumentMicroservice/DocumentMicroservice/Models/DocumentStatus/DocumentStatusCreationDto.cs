using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class DocumentStatusCreationDto
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
