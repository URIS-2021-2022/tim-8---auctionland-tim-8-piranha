using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    /// <summary>
    /// Represents confirmation of plot cadastral municipality.
    /// </summary>
    public class PlotCadastralMunicipalityConfirmation
    {
        /// <summary>
        /// Plot cadastral municipality ID.
        /// </summary>
        public Guid PlotCadastralMunicipalityId { get; set; }

        /// <summary>
        /// Plot cadastral municipality.
        /// </summary>
        public string CadastralMunicipality { get; set; }
    }
}
