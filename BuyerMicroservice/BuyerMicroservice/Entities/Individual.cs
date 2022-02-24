using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class Individual : Buyer
    {
        
        public string surname { get; set; }
        
        public string JMBG { get; set; }

       
    }
}
