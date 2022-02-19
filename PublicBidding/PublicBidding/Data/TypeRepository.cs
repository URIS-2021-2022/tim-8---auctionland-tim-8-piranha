using Microsoft.EntityFrameworkCore;
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

        public async Task<Entities.Type> GetTypeById(Guid typeId)
        {
            return await context.Types.FirstOrDefaultAsync(e => e.TypeId == typeId);
        }

        public async Task<List<Entities.Type>> GetTypes()
        {
            return await context.Types.ToListAsync();
        }
    }
}
