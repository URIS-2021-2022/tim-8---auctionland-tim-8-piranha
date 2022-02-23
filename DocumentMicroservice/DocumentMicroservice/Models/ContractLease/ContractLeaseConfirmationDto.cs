using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.ContractLease
{
    /// <summary>
    /// Contract Lease confirmation DTO model
    /// </summary>

    public class ContractLeaseConfirmationDto
    {

        /// <summary>
        /// SerialNumber - Zavodni broj
        /// </summary>
        public string serialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// Example:"2021-02-01 00:00:00"
        /// </summary>
        public DateTime? submissionDate { get; set; }

        /// <summary>
        /// DeadlineLandRestitution - Rok za vracanje zemljista
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
        /// Example: 68bf5d70-f26b-4c53-b014-bab74b7b86a0
        /// </summary>
        public Guid guaranteeTypeID { get; set; }

        /// <summary>
        /// DocumentID - ID dokumenta
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
        /// </summary>
        public Guid documentId { get; set; }
    }
}
