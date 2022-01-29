using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPartProtectedZone
    {
        public Guid PlotPartProtectedZoneId { get; set; }

        public string ProtectedZone { get; set; }
    }
}
