using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Journal
{
    public class JournalUpdateDto
    {
        public Guid JournalId { get; set; }
        [Required(ErrorMessage = "Must enter a journal number")]
        public string JournalNumber { get; set; }
        [Required(ErrorMessage = "Must enter a municipality")]
        public string Municipality { get; set; }
        [Required(ErrorMessage = "Must enter a date of issue")]
        public string DateOfIssue { get; set; }
    }
}
