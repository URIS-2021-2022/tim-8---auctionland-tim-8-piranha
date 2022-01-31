using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPartProtectedZoneConfirmation
    {
        public Guid PlotPartProtectedZoneId { get; set; }

        public string ProtectedZone { get; set; }
    }
}
