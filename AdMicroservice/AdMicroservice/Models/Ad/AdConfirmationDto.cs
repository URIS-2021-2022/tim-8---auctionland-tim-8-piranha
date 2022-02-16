using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    public class AdConfirmationDto
    {
        public Guid AdId { get; set; }
        public string PublicationDate { get; set; }
        public Guid JournalId { get; set; }
    }
}
