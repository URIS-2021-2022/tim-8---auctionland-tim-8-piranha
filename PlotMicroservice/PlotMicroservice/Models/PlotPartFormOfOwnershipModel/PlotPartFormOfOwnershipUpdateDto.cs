using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartFormOfOwnershipModel
{
    public class PlotPartFormOfOwnershipUpdateDto
    {
        public Guid PlotPartFormOfOwnershipId { get; set; }

        public string FormOfOwnership { get; set; }
    }
}
