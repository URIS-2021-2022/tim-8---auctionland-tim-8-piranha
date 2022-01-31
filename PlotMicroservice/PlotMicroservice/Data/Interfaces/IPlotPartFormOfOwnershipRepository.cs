using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Data.Interfaces
{
    public interface IPlotPartFormOfOwnershipRepository
    {
        List<PlotPartFormOfOwnership> GetPlotPartFormOfOwnerships(string formOfOwnership = null);

        PlotPartFormOfOwnership GetPlotPartFormOfOwnershipById(Guid plotPartOfOwnershipId);

        PlotPartFormOfOwnershipConfirmation CreatPlotPartFormOfOwnership(PlotPartFormOfOwnership plotPartFormOfOwnership);

        void UpdatePlotPartFormOfOwnership(PlotPartFormOfOwnership plotPartFormOfOwnership);

        void DeletePlotPartFormOfOwnership(Guid plotPartOfOwnershipId);

        bool SaveChanges();
    }
}
