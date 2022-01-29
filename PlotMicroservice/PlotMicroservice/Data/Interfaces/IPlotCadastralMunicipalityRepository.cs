using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotCadastralMunicipalityRepository
    {
        List<PlotCadastralMunicipality> GetPlotCadastralMunicipalities(string cadastrialMunicipality = null);

        PlotCadastralMunicipality GetPlotCadastralMunicipalityById(Guid plotCadastrialMunicipalityId);

        PlotCadastralMunicipalityConfirmation CreatePlotCadastralMunicipality(PlotCadastralMunicipality plotCadastralMunicipality);

        void UpdatePlotCadastralMunicipality(PlotCadastralMunicipality plotCadastralMunicipality);

        void DeletePlotCadastralMunicipality(Guid plotCadastrialMunicipalityId);

        bool SaveChanges();
    }
}
