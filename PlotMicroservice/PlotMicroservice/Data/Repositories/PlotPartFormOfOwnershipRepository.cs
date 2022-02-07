using AutoMapper;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Repositories
{
    public class PlotPartFormOfOwnershipRepository : IPlotPartFormOfOwnershipRepository
    {
        private readonly PlotContext PlotContext;
        private readonly IMapper Mapper;

        public PlotPartFormOfOwnershipRepository(PlotContext context, IMapper mapper)
        {
            PlotContext = context;
            Mapper = mapper;
        }

        public PlotPartFormOfOwnershipConfirmation CreatPlotPartFormOfOwnership(PlotPartFormOfOwnership plotPartFormOfOwnership)
        {
            var createdEntity = PlotContext.Add(plotPartFormOfOwnership);
            return Mapper.Map<PlotPartFormOfOwnershipConfirmation>(createdEntity.Entity);
        }

        public void DeletePlotPartFormOfOwnership(Guid plotPartOfOwnershipId)
        {
            var plotPartOfOwnership = GetPlotPartFormOfOwnershipById(plotPartOfOwnershipId);
            PlotContext.Remove(plotPartOfOwnership);
        }

        public PlotPartFormOfOwnership GetPlotPartFormOfOwnershipById(Guid plotPartOfOwnershipId)
        {
            return PlotContext.PlotPartFormOfOwnerships.FirstOrDefault(o => o.PlotPartFormOfOwnershipId == plotPartOfOwnershipId);
        }

        public List<PlotPartFormOfOwnership> GetPlotPartFormOfOwnerships(string formOfOwnership = null)
        {
            return PlotContext.PlotPartFormOfOwnerships.Where(o => o.FormOfOwnership == formOfOwnership || formOfOwnership == null).ToList();
        }

        public bool SaveChanges()
        {
            return PlotContext.SaveChanges() > 0;
        }

        public void UpdatePlotPartFormOfOwnership(PlotPartFormOfOwnership plotPartFormOfOwnership)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
