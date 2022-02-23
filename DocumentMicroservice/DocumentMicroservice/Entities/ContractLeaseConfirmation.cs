using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    /// <summary>
    /// Contract Lease confirmation model
    /// </summary>
    public class ContractLeaseConfirmation
    {
        /// <summary>
        /// ContractLeaseID - ID ugovora o zakupu
        /// Example: 86c9ac76-a632-4ffc-b2a2-26ea8600dc86
        /// </summary>

        public Guid contractLeaseID { get; set; }

        /// <summary>
        /// SerialNumber - Zavodni broj
        /// Example:2342323
        /// </summary>
        public string serialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// Example:"2021-02-01 00:00:00"
        /// </summary>
        public DateTime? submissionDate { get; set; }



        /// <summary>
        /// DeadlineLandRestitution - Rok za vracanje zemljista
        /// Example: "2021-02-01 00:00:00"
        /// </summary>
        public DateTime? deadlineLandRestitution { get; set; }


        /// <summary>
        /// Place Of Signing - Mesto potpisivanja 
        /// Example:Zrenjanin
        /// </summary>
        public string placeOfSigning { get; set; }


        /// <summary>
        /// Date Of Signing - Datum potpisivanja
        /// Example : "2021-02-01 00:00:00"
        /// </summary>
        public DateTime dateOfSigning { get; set; }


        /// <summary>
        /// GuaranteeTypeID - ID tipa garancije
        /// </summary>
        public Guid guaranteeTypeID { get; set; }
        public GuaranteeType guaranteeType { get; set; }

        /// <summary>
        /// DocumentId - ID dokumenta
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
        /// </summary>
        public Guid documentId { get; set; }
    }
}
