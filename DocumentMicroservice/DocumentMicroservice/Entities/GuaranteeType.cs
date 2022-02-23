using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
<<<<<<< HEAD
    /// <summary>
    /// Guarantee type model
    /// </summary>
=======
    //tip garancije
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class GuaranteeType
    {
        /// <summary>
        /// GuaranteeTypeID - ID tipa garancije
        /// Example: 07af89f2-feee-4680-b489-9d0e31699588
        /// </summary>
        [Key]
        public Guid guaranteeTypeID { get; set; }

        /// <summary>
        /// Type - tip garancije
        /// Example: Bankarska garancija
        /// </summary>
        public string type { get; set; }
    }
}
