using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public interface ITypeRepository
    {
        List<Entities.Type> GetTypes();

        Entities.Type GetTypeById(Guid typeId);
    }
}
