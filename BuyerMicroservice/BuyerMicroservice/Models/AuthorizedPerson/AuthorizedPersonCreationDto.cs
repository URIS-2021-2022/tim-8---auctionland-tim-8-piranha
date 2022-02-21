using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.AuthorizedPerson
{
    public class AuthorizedPersonCreationDto
    {

        public string name { get; set; }

        public string surname { get; set; }

        public string personalDocNum { get; set; }

        public string address { get; set; }


        public string country { get; set; }


        public IList<BoardNumber> boardNums { get; set; }
    }
}
