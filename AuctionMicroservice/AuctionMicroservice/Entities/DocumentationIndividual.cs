using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class DocumentationIndividual
    {



        #region
       
        public Guid DocumentationIndividualId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string IdentificationNumber { get; set; }

        #endregion
    }
}
