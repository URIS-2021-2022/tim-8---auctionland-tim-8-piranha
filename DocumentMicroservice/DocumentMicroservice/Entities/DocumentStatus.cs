using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class DocumentStatus
    {
        /// <summary>
        /// DocStatusID - ID statusa dokumenta
        /// Example: 07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        [Key]
        public Guid DocStatusID { get; set; }

        /// <summary>
        /// Status - Status dokumenta
        /// Example:Usvojen
        /// </summary>
        public string Status { get; set; }
    }
}
