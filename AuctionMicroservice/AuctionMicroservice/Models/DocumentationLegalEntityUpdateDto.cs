using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    /// <summary>
    /// Represents legal entity documentation update model
    /// </summary>
    public class DocumentationLegalEntityUpdateDto
    {

        #region
        /// <summary>
        /// legal entity documentation ID
        /// </summary>
        [Key]
        public Guid DocumentationLegalEntityId { get; set; }

        /// <summary>
        /// legal entity name
        /// </summary>
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// legal entity identification number
        /// </summary>
        [Required(ErrorMessage = "Identification number is required!")]
        [MaxLength(20)]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// legal entity address
        /// </summary>
        [Required(ErrorMessage = "Legal entity address is required!")]
        [MaxLength(20)]
        public string Address { get; set; }
        /// <summary>
        /// ID of auction that this documentation belongs to
        /// </summary>
        [Required]
        public Guid AuctionId { get; set; }

        #endregion




    }
}
