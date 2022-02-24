using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public class AuthorizedPersonConfirmation
    {
       
        public Guid authorizedPersonID { get; set; }
        public Guid boardNumbID { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string personalDocNum { get; set; }

        public string address { get; set; }
        
     
        public string country { get; set; }


    }
}
