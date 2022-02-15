using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities.Journal
{
    public class JournalConfirmation
    {
        public Guid JournalId { get; set; }
        public string JournalNumber { get; set; }
        public string Municipality { get; set; }
        public string DateOfIssue { get; set; }
    }
}
