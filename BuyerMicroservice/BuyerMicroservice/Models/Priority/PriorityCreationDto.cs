using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Priority
{
    public class PriorityCreationDto
    {
        public Guid priorityID { get; set; }

        public string priorityType { get; set; }
    }
}
