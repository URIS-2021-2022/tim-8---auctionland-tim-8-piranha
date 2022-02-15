using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities.Ad
{
    public class AdConfirmation
    {
        public Guid AdId { get; set; }
        public string PublicationDate { get; set; }
    }
}
