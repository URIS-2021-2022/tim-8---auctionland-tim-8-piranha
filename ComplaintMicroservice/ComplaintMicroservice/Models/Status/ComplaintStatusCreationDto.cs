using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.ComplaintStatusDto
{
    public class ComplaintStatusCreationDto
    {
        [Required(ErrorMessage = "Must enter a status.")]
        public string Status { get; set; }
    }
}
