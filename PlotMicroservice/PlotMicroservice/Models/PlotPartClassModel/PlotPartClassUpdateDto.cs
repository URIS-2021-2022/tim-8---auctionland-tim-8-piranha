using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartClassModel
{
    public class PlotPartClassUpdateDto
    {
        public Guid PlotPartClassId { get; set; }

        public string Class { get; set; }
    }
}
