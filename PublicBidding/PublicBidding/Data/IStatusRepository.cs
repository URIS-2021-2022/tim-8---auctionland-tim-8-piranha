using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public interface IStatusRepository
    {
        List<Status> GetStatuses();

        Status GetStatusById(Guid statusId);
    }
}
