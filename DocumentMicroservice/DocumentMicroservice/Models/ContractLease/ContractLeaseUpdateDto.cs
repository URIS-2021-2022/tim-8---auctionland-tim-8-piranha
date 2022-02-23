using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.ContractLease
{
    public class ContractLeaseUpdateDto
    {

        /// <summary>
        /// ContractLeaseID - ID ugovora o zakupu
        /// Example: 86c9ac76-a632-4ffc-b2a2-26ea8600dc86
        /// </summary>
        public Guid contractLeaseID { get; set; }




        /// <summary>
        /// SerialNumber - Zavodni broj
        /// </summary>
        public string serialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// Example:"2021-02-01 00:00:00"
        /// </summary>
        public DateTime? submissionDate { get; set; }







        //rok za vracanje zemljista
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
        /// DocumentId - ID dokumenta
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
        /// DocumentID - ID dokumenta
        /// </summary>
        public Guid documentID { get; set; }

        /// <summary>
        /// Buyer ID from Buyer microservice.
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
        /// This is optional attributte
        /// </summary>
        public Guid? buyerId { get; set; }

        /// <summary>
        /// Person ID from Person microservice.
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
        /// This is optional attributte
        /// </summary>
        public Guid? personId { get; set; }

        /// <summary>
        /// Plot ID  from Plot microservice.
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
        /// This is optional attributte
        /// </summary>
        public Guid? plotId { get; set; }

    }
}
