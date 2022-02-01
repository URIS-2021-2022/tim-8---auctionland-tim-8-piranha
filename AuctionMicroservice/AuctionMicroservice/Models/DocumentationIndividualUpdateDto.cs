using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class DocumentationIndividualUpdateDto
    {
        #region
        public Guid DocumentationIndividualId { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(20)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Identification is required")]
        public string IdentificationNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == Surname)
            {
                yield return new ValidationResult(
                    "Individual cannot have same first name and same last name",
                    new[] { "DocumentatonIndividualCreationDto" });


            }


        }
        #endregion
    }
}
