using AdMicroservice.Entities.Journal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities.Ad
{
    public class AdModel
    {
        [Key]
        public Guid AdId { get; set; }
        public string PublicationDate { get; set; }
        public Guid JournalId { get; set; }
        public JournalModel Journal { get; set; }
    }
}
