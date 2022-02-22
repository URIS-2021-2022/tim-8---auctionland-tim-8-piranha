using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyerMicroservice.Entities
{
    public class AuthorizedPerson
    {
        [Key]
        public Guid authorizedPersonID { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string personalDocNum { get; set; }

        public string address { get; set; }
        //lice za koje se vrsi aukcija 
        // ne treba foreign key zato sto strelica pokazuje u kontra smeru 
        public string country { get; set; }

        public ICollection<Buyer> buyers { get; set; }

        
    }
}
