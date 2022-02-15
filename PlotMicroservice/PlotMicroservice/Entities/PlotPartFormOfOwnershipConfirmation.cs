using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot part form of ownership.
    /// </summary>
    public class PlotPartFormOfOwnershipConfirmation
    {
        /// <summary>
        /// Plot part form of ownership ID.
        /// </summary>
        public Guid PlotPartFormOfOwnershipId { get; set; }

        /// <summary>
        /// Plot part form of ownership. Example: Državna svojina|
        /// </summary>
        public string FormOfOwnership { get; set; }
    }
}
