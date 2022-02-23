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
        /// </summary>
        public Guid contractLeaseID { get; set; }

<<<<<<< HEAD
=======

<<<<<<< Updated upstream
        
=======
        /// <summary>
        /// Maturities - Rokovi dospeća
        /// </summary>
        //public int[] Maturities { get; set; }
>>>>>>> Stashed changes


>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
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
        public Guid documentId { get; set; }

        /// <summary>
        /// Buyer ID.
        /// </summary>
        public Guid? buyerId { get; set; }

        public Guid? personId { get; set; }

        public Guid? plotId { get; set; }

    }
}
