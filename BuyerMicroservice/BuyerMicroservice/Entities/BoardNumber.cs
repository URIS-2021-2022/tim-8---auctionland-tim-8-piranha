using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class BoardNumber 
    {
        [Key]
        public Guid boardNumberID { get; set; }
        public int number { get; set; }

        
    }
}
