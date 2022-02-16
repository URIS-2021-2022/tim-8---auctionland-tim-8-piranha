using AdMicroservice.Models.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    public class AdDto
    {
        public string PublicationDate { get; set; }
        public JournalDto Journal { get; set; }
    }
}
