using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class IndividualConfirmation : BuyerConfirmation
    {
        

        public string surname { get; set; }

        public string JMBG { get; set; }

        
    }
}
