using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartClassModel
{
    /// <summary>
    /// DTO for plot part class.
    /// </summary>
    public class PlotPartClassDto
    {
        /// <summary>
        /// Plot part class. Example: V
        /// </summary>
        public string Class { get; set; }
    }
}
