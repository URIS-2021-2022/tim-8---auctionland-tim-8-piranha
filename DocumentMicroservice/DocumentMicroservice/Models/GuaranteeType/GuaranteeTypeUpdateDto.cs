using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.GuaranteeType
{
    /// <summary>
    /// Guarantee type update DTO model for commnication with user
    /// </summary>
    public class GuaranteeTypeUpdateDto
    {
        /// <summary>
        /// GuaranteeTypeID - ID tipa garancije
        /// Example: 07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        public Guid GuaranteeTypeID { get; set; }

        /// <summary>
        /// Type - tip garancije
        /// Example: Bankarska garancija
        /// </summary>
        public string Type { get; set; }
    }
}
