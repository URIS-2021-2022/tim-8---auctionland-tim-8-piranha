using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotCadastralMunicipalityRepository
    {
        Task<List<PlotCadastralMunicipality>> GetPlotCadastralMunicipalitiesAsync(string cadastrialMunicipality = null);

        Task<PlotCadastralMunicipality> GetPlotCadastralMunicipalityByIdAsync(Guid plotCadastrialMunicipalityId);

        Task<PlotCadastralMunicipalityConfirmation> CreatePlotCadastralMunicipalityAsync(PlotCadastralMunicipality plotCadastralMunicipality);

        Task UpdatePlotCadastralMunicipalityAsync(PlotCadastralMunicipality plotCadastralMunicipality);

        Task DeletePlotCadastralMunicipalityAsync(Guid plotCadastrialMunicipalityId);

        Task<bool> SaveChangesAsync();
    }
}
