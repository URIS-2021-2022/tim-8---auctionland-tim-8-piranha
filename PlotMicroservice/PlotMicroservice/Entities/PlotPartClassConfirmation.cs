using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotPartClassConfirmation
    {
        public Guid PlotPartClassId { get; set; }

        public string Class { get; set; }
    }
}
