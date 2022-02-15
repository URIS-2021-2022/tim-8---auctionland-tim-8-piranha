using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models
{
    /// <summary>
    /// Update model for plot cadastral municipality.
    /// </summary>
    public class PlotCadastralMunicipalityUpdateDto
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
