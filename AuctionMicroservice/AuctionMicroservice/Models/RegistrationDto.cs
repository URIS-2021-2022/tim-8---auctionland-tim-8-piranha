using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    public class RegistrationDto
    {
        #region
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }
        #endregion
    }
}
