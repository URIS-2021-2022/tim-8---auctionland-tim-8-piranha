using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.ContractLease
{
    public class ContractLeaseConfirmationDto
    {

        /// <summary>
        /// Maturities - Rokovi dospeća
        /// </summary>
       // public int[] Maturities { get; set; }


        /// <summary>
        /// SerialNumber - Zavodni broj
        /// </summary>
        public string SerialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// </summary>
        public DateTime? SubmissionDate { get; set; }

        

        /// <summary>
        /// DeadlineLandRestitution - Rok za vracanje zemljista
        /// </summary>
        public DateTime? DeadlineLandRestitution { get; set; }


        /// <summary>
        /// Place Of Signing - Mesto potpisivanja 
        /// </summary>
        public string PlaceOfSigning { get; set; }


        /// <summary>
        /// Date Of Signing - Datum potpisivanja
        /// </summary>
        public DateTime DateOfSigning { get; set; }

        /// <summary>
        /// DocumentID - ID dokumenta
        /// </summary>
        
        public Guid documentId { get; set; }


    }
}
