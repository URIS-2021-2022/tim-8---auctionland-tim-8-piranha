using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    [Keyless]
    public class Buyer
    {
        public string Priority { get; set; }

        public string TypeOfBuyer { get; set; }

        public int RealizedSurface { get; set; }

        public bool IsBan { get; set; }

        public DateTime BanStartDate { get; set; }

        public DateTime BanEndDate { get; set; }

        public int BanLength { get; set; }

        public List<PublicBidding> PublicBiddings { get; set; }
    }
}
