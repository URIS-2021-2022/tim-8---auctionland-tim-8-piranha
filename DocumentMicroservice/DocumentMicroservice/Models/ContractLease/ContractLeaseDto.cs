using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.ContractLease
{
    public class ContractLeaseDto
    {
<<<<<<< Updated upstream
        
=======


        /// <summary>
        /// Maturities - Rokovi dospeća
        /// </summary>
        //public int[] Maturities { get; set; }
>>>>>>> Stashed changes


        /// <summary>
        /// SerialNumber - Zavodni broj
        /// </summary>
        public string serialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// </summary>
        public DateTime? submissionDate { get; set; }

<<<<<<< Updated upstream

=======
       
>>>>>>> Stashed changes

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
        /// Buyer DTO.
        /// </summary>
        public BuyerDto buyer { get; set; }

        public PersonDto person { get; set; }

        public PlotDto plot { get; set; }


    }
}
