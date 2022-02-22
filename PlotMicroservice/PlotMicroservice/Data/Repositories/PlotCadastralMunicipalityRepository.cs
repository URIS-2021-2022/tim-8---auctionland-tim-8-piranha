using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PlotCadastralMunicipalityConfirmation> CreatePlotCadastralMunicipalityAsync(PlotCadastralMunicipality plotCadastralMunicipality)
        {
            var createdEntity = await Context.AddAsync(plotCadastralMunicipality);
            return Mapper.Map<PlotCadastralMunicipalityConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePlotCadastralMunicipalityAsync(Guid plotCadastrialMunicipalityId)
        {
            var cadastralMunicipality = await GetPlotCadastralMunicipalityByIdAsync(plotCadastrialMunicipalityId);
            Context.Remove(cadastralMunicipality);
        }

        public async Task<List<PlotCadastralMunicipality>> GetPlotCadastralMunicipalitiesAsync(string cadastrialMunicipality = null)
        {
            return await Context.PlotCadastralMunicipalities.Where(o => o.CadastralMunicipality == cadastrialMunicipality || cadastrialMunicipality == null).ToListAsync();
        }

        public async Task<PlotCadastralMunicipality> GetPlotCadastralMunicipalityByIdAsync(Guid plotCadastrialMunicipalityId)
        {
            return await Context.PlotCadastralMunicipalities.FirstOrDefaultAsync(o => o.PlotCadastralMunicipalityId == plotCadastrialMunicipalityId);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdatePlotCadastralMunicipalityAsync(PlotCadastralMunicipality plotCadastralMunicipality)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
               kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
