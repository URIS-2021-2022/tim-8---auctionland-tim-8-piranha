using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotCultureModel
{
    public class PlotCultureUpdateDto
    {
        public Guid PlotCultureId { get; set; }

        public string Culture { get; set; }
    }
}
