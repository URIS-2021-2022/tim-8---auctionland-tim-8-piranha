using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetStatuses();

        Task<Status> GetStatusById(Guid statusId);
    }
}
