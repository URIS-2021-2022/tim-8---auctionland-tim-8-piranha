using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public class TypeRepository : ITypeRepository
    {
        private readonly PublicBiddingContext context;

        public TypeRepository(PublicBiddingContext context)
        {
            this.context = context;
        }

        public Entities.Type GetTypeById(Guid typeId)
        {
            return context.Types.FirstOrDefault(e => e.TypeId == typeId);
        }

        public List<Entities.Type> GetTypes()
        {
            return context.Types.ToList();
        }
    }
}
