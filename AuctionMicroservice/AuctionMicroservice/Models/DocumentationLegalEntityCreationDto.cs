using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class DocumentationLegalEntityCreationDto
    {
        #region
        [Key]
        public Guid DocumentationLegalEntityId { get; set; }


        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Identification number is required!")]
        [MaxLength(20)]
        public string IdentificationNumber { get; set; }


        [Required(ErrorMessage = "Legal entity address is required!")]
        [MaxLength(20)]
        public string Address { get; set; }

        [Required]
        public Guid AuctionId { get; set; }

        #endregion

    }
}
