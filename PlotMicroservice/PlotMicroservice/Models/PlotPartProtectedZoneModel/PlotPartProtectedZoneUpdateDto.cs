using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartProtectedZoneModel
{
    public class PlotPartProtectedZoneUpdateDto
    {
        public Guid PlotPartProtectedZoneId { get; set; }

        public string ProtectedZone { get; set; }
    }
}
