using System;
using System.Collections.Generic;
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
        #endregion
    }
}
