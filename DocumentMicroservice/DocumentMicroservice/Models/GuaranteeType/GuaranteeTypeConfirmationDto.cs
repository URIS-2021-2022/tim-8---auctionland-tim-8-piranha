using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.GuaranteeType
{
    public class GuaranteeTypeConfirmationDto
    {
        public Guid GuaranteeTypeID { get; set; }

        /// <summary>
        /// Type - tip garancije
        /// Example: Bankarska garancija
        /// </summary>
        public string Type { get; set; }
    }
}
