using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
    public interface IPriorityRepository
    {
        Task<List<Priority>> GetPriorityAsync(string priorityType = null);

        Task<Priority> GetPriorityByIdAsync(Guid priorityID);

        Task<PriorityConfirmation> CreatePriorityAsync(Priority priority);

        Task UpdatePriorityAsync(Priority priority);

        Task DeletePriorityAsync(Guid priorityID);

        Task<bool> SaveChangesAsync();
    }
}
