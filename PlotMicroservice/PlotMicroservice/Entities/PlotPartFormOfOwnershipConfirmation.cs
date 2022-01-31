using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPartFormOfOwnershipConfirmation
    {
        public Guid PlotPartFormOfOwnershipId { get; set; }

        public string FormOfOwnership { get; set; }
    }
}
