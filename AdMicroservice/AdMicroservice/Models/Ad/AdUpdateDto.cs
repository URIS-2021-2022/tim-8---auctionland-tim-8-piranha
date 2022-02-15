using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    public class AdUpdateDto
    {
        public Guid AdId { get; set; }
        [Required(ErrorMessage = "Must enter a date of publication")]
        public string PublicationDate { get; set; }
    }
}
