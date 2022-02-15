using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    public class AdCreationDto
    {
        [Required(ErrorMessage = "Must enter a date of publication")]
        public string PublicationDate { get; set; }
    }
}
