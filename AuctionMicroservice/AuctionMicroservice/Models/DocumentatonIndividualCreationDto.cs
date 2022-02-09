using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class DocumentatonLegalEntitylCreationDto : IValidatableObject
    {
        #region
        //[Key]
        //public Guid DocumentationIndividualId { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(20)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Identification is required")]
        public string IdentificationNumber { get; set; }


        //[ForeignKey("AuctionDto")]
        public Guid AuctionId { get; set; }
        //public AuctionDto AuctionDto { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(FirstName == Surname)
            {
                yield return new ValidationResult(
                    "Individual cannot have same first name and same last name",
                    new[] { "DocumentatonIndividualCreationDto" });

                    
            }


        }
        #endregion
    }
}
