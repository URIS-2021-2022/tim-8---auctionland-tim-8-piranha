using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotCadastralMunicipalityConfirmation
    {
        public Guid PlotCadastralMunicipalityId { get; set; }

        public string CadastralMunicipality { get; set; }
    }
}
