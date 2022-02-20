using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.BoardNumber
{
    public class BoardNumberCreationDto
    {
        public Guid boardID { get; set; }

        public string boardNum { get; set; }
    }
}
