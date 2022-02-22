using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.ContractLease
{
    public class ContractLeaseCreationDto
    {
        /// <summary>
        /// ContractLeaseID - ID ugovora o zakupu
        /// </summary>
        public Guid contractLeaseID { get; set; }


        


        /// <summary>
        /// SerialNumber - Zavodni broj
        /// </summary>
        public string serialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// </summary>
        public DateTime? submissionDate { get; set; }

        

        //rok za vracanje zemljista
        /// <summary>
        /// DeadlineLandRestitution - Rok za vracanje zemljista
        /// </summary>
        public DateTime? deadlineLandRestitution { get; set; }


        /// <summary>
        /// Place Of Signing - Mesto potpisivanja 
        /// </summary>
        public string placeOfSigning { get; set; }


        /// <summary>
        /// Date Of Signing - Datum potpisivanja
        /// </summary>
        public DateTime dateOfSigning { get; set; }


        /// <summary>
        /// GuaranteeTypeID - ID tipa garancije
        /// </summary>
        public Guid guaranteeTypeID { get; set; }

        /// <summary>
        /// DocumentID - ID dokumenta
        /// </summary>
        public Guid documentID { get; set; }

        /// <summary>
        /// Buyer ID.
        /// </summary>
        public Guid? buyerId { get; set; }

        public Guid? personId { get; set; }

        public Guid? plotId { get; set; }

    }
}
