using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class DocumentationIndividualConfirmationDto
    {
        #region
        
        public Guid DocumentationIndividualId { get; set; }
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string IdentificationNumber { get; set; }
        
        //public Guid AuctionId { get; set; }
        //public AuctionDto AuctionDto { get; set; }

        #endregion
    }
}
