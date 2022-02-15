using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Models
{
    /// <summary>
    /// DTO for cadastral municipality
    /// </summary>
    public class PlotCadastralMunicipalityDto
    {
        /// <summary>
        /// Plot cadastral municipality. Example: Bikovo
        /// </summary>
        public string CadastralMunicipality { get; set; }
    }
}
