using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartFormOfOwnershipRepository
    {
        Task<List<PlotPartFormOfOwnership>> GetPlotPartFormOfOwnershipsAsync(string formOfOwnership = null);

        Task<PlotPartFormOfOwnership> GetPlotPartFormOfOwnershipByIdAsync(Guid plotPartOfOwnershipId);

        Task<PlotPartFormOfOwnershipConfirmation> CreatPlotPartFormOfOwnershipAsync(PlotPartFormOfOwnership plotPartFormOfOwnership);

        Task UpdatePlotPartFormOfOwnershipAsync(PlotPartFormOfOwnership plotPartFormOfOwnership);

        Task DeletePlotPartFormOfOwnershipAsync(Guid plotPartOfOwnershipId);

        Task<bool> SaveChangesAsync();
    }
}
