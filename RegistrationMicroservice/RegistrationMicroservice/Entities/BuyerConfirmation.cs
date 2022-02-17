using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Entities
{
    public class BuyerConfirmation
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
