using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class BoardNumberConfirmation
    {
        public Guid boardNumberID { get; set; }
        public int number { get; set; }
    }
}
