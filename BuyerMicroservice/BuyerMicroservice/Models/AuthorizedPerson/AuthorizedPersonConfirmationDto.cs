using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyerMicroservice.Entities;

namespace BuyerMicroservice.Models.AuthorizedPerson
{
    public class AuthorizedPersonConfirmationDto
    {
       

        public string name { get; set; }

        public string surname { get; set; }

        public string personalDocNum { get; set; }

        public string address { get; set; }

      
        public string country { get; set; }


        //public BoardNumber[] boardNums { get; set; }
    }
}
