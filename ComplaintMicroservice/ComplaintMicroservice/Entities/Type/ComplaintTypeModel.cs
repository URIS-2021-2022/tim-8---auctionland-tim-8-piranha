using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    public class ComplaintTypeModel
    {
        [Key]
        public Guid ComplaintTypeId { get; set; }
        public string ComplaintType { get; set; }
    }
}
