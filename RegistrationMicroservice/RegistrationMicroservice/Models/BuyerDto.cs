using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Models
{
    public class BuyerDto
    {
        #region
        public Guid BuyerId { get; set; }

        public int BoughtSurface { get; set; }

        public DateTime RestrictionStart { get; set; }

        public int RestrictionPeriodInYears { get; set; }

        public DateTime RestrictionEnd { get; set; }

        
        #endregion

    }
}
