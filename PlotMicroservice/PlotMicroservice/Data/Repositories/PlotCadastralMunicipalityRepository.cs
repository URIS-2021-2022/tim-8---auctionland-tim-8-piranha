using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotCadastralMunicipalityRepository : IPlotCadastralMunicipalityRepository
    {
        private readonly PlotContext Context;
        private readonly IMapper Mapper;

        public PlotCadastralMunicipalityRepository(PlotContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public PlotCadastralMunicipalityConfirmation CreatePlotCadastralMunicipality(PlotCadastralMunicipality plotCadastralMunicipality)
        {
            var createdEntity = Context.Add(plotCadastralMunicipality);
            return Mapper.Map<PlotCadastralMunicipalityConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotCadastralMunicipality(Guid plotCadastrialMunicipalityId)
        {
            var cadastralMunicipality = GetPlotCadastralMunicipalityById(plotCadastrialMunicipalityId);
            Context.Remove(cadastralMunicipality);
        }

        public List<PlotCadastralMunicipality> GetPlotCadastralMunicipalities(string cadastrialMunicipality = null)
        {
            return Context.PlotCadastralMunicipalities.Where(o => o.CadastralMunicipality == cadastrialMunicipality || cadastrialMunicipality == null).ToList();
        }

        public PlotCadastralMunicipality GetPlotCadastralMunicipalityById(Guid plotCadastrialMunicipalityId)
        {
            return Context.PlotCadastralMunicipalities.FirstOrDefault(o => o.PlotCadastralMunicipalityId == plotCadastrialMunicipalityId);
        }

        public void UpdatePlotCadastralMunicipality(PlotCadastralMunicipality plotCadastralMunicipality)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
               kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }

        public bool SaveChanges()
        {
            return Context.SaveChanges() > 0;
        }
    }
}
