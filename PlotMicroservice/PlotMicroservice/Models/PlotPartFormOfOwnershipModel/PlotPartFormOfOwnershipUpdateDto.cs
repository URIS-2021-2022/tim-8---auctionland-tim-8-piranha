using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models.PlotPartFormOfOwnershipModel
{
    /// <summary>
    /// Update model for plot part form of ownership.
    /// </summary>
    public class PlotPartFormOfOwnershipUpdateDto
    {
        /// <summary>
        /// Plot part form of ownership ID.
        /// </summary>
        public Guid PlotPartFormOfOwnershipId { get; set; }

        /// <summary>
        /// Plot part form of ownership.
        /// </summary>
        public string FormOfOwnership { get; set; }
    }
}
