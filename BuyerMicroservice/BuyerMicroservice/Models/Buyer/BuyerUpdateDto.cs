using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Buyer
{
    public class BuyerUpdateDto
    {
       
        public Guid buyerID { get; set; }

        public Guid priorityID { get; set; }

        public int realizedArea { get; set; }

        // public string paymentID { get; set; }

        public Guid authorizedPersonID { get; set; }

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

        public Guid? addressId { get; set; }

        public Guid? paymentId { get; set; }
    }
}
