using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents plot part form of ownership.
    /// </summary>
    public class PlotPartFormOfOwnership
    {
        /// <summary>
        /// Plot part form of ownership ID.
        /// </summary>
        public Guid PlotPartFormOfOwnershipId { get; set; }

        /// <summary>
        /// Plot part form of ownership. Example: Privatna svojina
        /// </summary>
        public string FormOfOwnership { get; set; }
    }
}
