using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class ContractLease
    {
        /// <summary>
        /// ContractLeaseID - ID ugovora o zakupu
        /// </summary>
        [Key]
        public Guid contractLeaseID { get; set; }

        
        /// <summary>
        /// Maturities - Rokovi dospeća
        /// </summary>
       // public ICollection<int> maturities { get; set; }

       
        /// <summary>
        /// SerialNumber - Zavodni broj
        /// </summary>
        public string serialNumber { get; set; }

        
        /// <summary>
        /// SubmissionDate - Datum zavodjenja
        /// </summary>
        public DateTime? submissionDate { get; set; }

      

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
        [ForeignKey("GuaranteeType")]
        public Guid guaranteeTypeID { get; set; }
        public GuaranteeType guaranteeType { get; set; }

        /// <summary>
        /// DocumentId - ID dokumenta
        /// </summary>
        [ForeignKey("Document")]
        public Guid documentId { get; set; }

        public Document document { get; set; }

        /// <summary>
        /// Buyer ID. Buyer ID from Buyer microservice.
        /// </summary>
        public Guid? buyerId { get; set; }

        public Guid? personId { get; set; }










    }
}
