using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.AuthorizedPersonBuyer
{
    public class AuthorizedPersonBuyerDto
    {
        public Guid authorizedPersonId { get; set; }

        public Guid buyerId { get; set; }
    }
}
