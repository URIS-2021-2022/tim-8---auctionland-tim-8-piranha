using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartFormOfOwnershipModel
{
    /// <summary>
    /// DTO for plot part form of ownership.
    /// </summary>
    public class PlotPartFormOfOwnershipDto
    {
        /// <summary>
        /// Plot part form of ownership. Example: Privatna svojina
        /// </summary>
        public string FormOfOwnership { get; set; }
    }
}
