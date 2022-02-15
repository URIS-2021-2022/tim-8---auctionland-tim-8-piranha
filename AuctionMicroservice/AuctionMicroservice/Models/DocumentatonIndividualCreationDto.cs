using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    /// <summary>
    /// Represents individual documentation creation model
    /// </summary>
    public class DocumentatonLegalEntitylCreationDto : IValidatableObject
    {
        #region

        /// <summary>
        /// Individual first name
        /// </summary>
        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(20)]
        public string FirstName { get; set; }

        /// <summary>
        /// Individual surname
        /// </summary>
        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(20)]
        public string Surname { get; set; }

        /// <summary>
        /// Individual identification number
        /// </summary>
        [Required(ErrorMessage = "Identification is required")]
        public string IdentificationNumber { get; set; }


        /// <summary>
        /// ID of auction that this documentation belongs to
        /// </summary>
        public Guid AuctionId { get; set; }
        //public AuctionDto AuctionDto { get; set; }

        /// <summary>
        /// Validates inidividual surname and first name
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
