using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    public abstract class Buyer
    {
        [Key]
        public Guid buyerID { get; set; }
        

        [ForeignKey("Priority")]
        public Guid priorityID { get; set; }

        public Priority priority { get; set; } 

        public int realizedArea { get; set; }
        
        [ForeignKey("AuthorizedPerson")]
        public Guid authorizedPersonID { get; set; }

        public AuthorizedPerson authorizedPerson { get; set; }

        public bool hasBan { get; set; }

        public DateTime? startDateOfBan { get; set; }

        public int durationOfBanInYear { get; set; }

        public DateTime? endDateOfBan { get; set; }

        public string name { get; set; }

        public string addresse { get; set; }

        public string phone1 { get; set; }

        public string phone2 { get; set; }

        public string email { get; set; }

        public string accountNumber { get; set; }

    }
}
