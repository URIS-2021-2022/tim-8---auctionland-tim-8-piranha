using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Buyer
{
    public class BuyerCreationDto
    {
        
        public Guid buyerID { get; set; }
        
        public Guid priorityID { get; set; }

        public bool IsIndividual { get; set; }

        public int realizedArea { get; set; }

       // public string paymentID { get; set; }
        
        public string authorizedPerson { get; set; }

        public bool hasBan { get; set; }

        public DateTime? startDateOfBan { get; set; }

        public int durationOfBanInYear { get; set; }

        public DateTime? endDateOfBan { get; set; }

       // public Guid publicTender { get; set; }
    }
}
