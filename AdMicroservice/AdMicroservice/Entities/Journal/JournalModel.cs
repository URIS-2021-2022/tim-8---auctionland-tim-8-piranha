using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities.Journal
{
    public class JournalModel
    {
        [Key]
        public Guid JournalId { get; set; }
        public string JournalNumber { get; set; }
        public string Municipality { get; set; }
        public string DateOfIssue { get; set; }
    }
}
