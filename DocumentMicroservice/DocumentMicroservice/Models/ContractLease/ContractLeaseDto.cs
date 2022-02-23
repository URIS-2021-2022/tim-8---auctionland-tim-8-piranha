using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models.ContractLease
{
<<<<<<< HEAD
    /// <summary>
    /// Contract Lease creation DTO model
    /// </summary>
    public class ContractLeaseDto
    {
        /// <summary>
        /// SerialNumber - Zavodni broj
        /// Example:2342323
=======
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
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public string serialNumber { get; set; }


        /// <summary>
        /// SubmissionDate - Datum zavodjenja
<<<<<<< HEAD
        /// Example:"2021-02-01 00:00:00"
        /// </summary>
        public DateTime? submissionDate { get; set; }


        /// <summary>
        /// DeadlineLandRestitution - Rok za vracanje zemljista
        /// Example: "2021-02-01 00:00:00"
=======
        /// </summary>
        public DateTime? submissionDate { get; set; }

<<<<<<< Updated upstream

=======
       
>>>>>>> Stashed changes

        /// <summary>
        /// DeadlineLandRestitution - Rok za vracanje zemljista
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public DateTime? deadlineLandRestitution { get; set; }


        /// <summary>
        /// Place Of Signing - Mesto potpisivanja 
<<<<<<< HEAD
        /// Example:Zrenjanin
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public string placeOfSigning { get; set; }


        /// <summary>
        /// Date Of Signing - Datum potpisivanja
<<<<<<< HEAD
        /// Example : "2021-02-01 00:00:00"
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public DateTime dateOfSigning { get; set; }

        /// <summary>
        /// GuaranteeTypeID - ID tipa garancije
<<<<<<< HEAD
        /// Example: 68bf5d70-f26b-4c53-b014-bab74b7b86a0
=======
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public Guid guaranteeTypeID { get; set; }

        /// <summary>
<<<<<<< HEAD
        /// DocumentId - ID dokumenta
        /// Example: 3a3e6366-3a20-4d3b-ae15-be85ba277683
=======
        /// DocumentID - ID dokumenta
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// </summary>
        public Guid documentId { get; set; }

        /// <summary>
        /// Buyer DTO.
        /// </summary>
        public BuyerDto buyer { get; set; }

<<<<<<< HEAD
        /// <summary>
        /// Person DTO from person microservice
        /// </summary>
        public PersonDto person { get; set; }
        /// <summary>
        /// Plot DTO from plot microservice
        /// </summary>
=======
        public PersonDto person { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public PlotDto plot { get; set; }


    }
}
