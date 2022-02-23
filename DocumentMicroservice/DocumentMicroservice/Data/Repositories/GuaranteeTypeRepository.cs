using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DocumentMicroservice.Data.Repositories
{
    public class GuaranteeTypeRepository : IGuaranteeTypeRepository
    {
        private readonly DocumentContext Context;
        private readonly IMapper Mapper;

        public GuaranteeTypeRepository(DocumentContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }


        public async Task<GuaranteeTypeConfirmation> CreateGuaranteeTypeAsync(GuaranteeType guaranteeType)
        {
            var createdEntity = await Context.AddAsync(guaranteeType);
            return Mapper.Map<GuaranteeTypeConfirmation>(createdEntity.Entity);
        }
  
        public async Task DeleteGuaranteeTypeAsync(Guid guaranteeTypeId)
        {
            var guaranteeType = await GetGuaranteeTypeByIdAsync(guaranteeTypeId);
            Context.Remove(guaranteeType);
        }

        public async Task<List<GuaranteeType>> GetGuaranteeTypeAsync(string guaranteeType = null)
        {
            return await Context.GuaranteeTypes.Where(o => o.type == null || guaranteeType == null).ToListAsync();
        }
        
        public async Task<GuaranteeType> GetGuaranteeTypeByIdAsync(Guid guaranteeTypeId)
        {
            return await Context.GuaranteeTypes.FirstOrDefaultAsync(o => o.guaranteeTypeID == guaranteeTypeId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task UpdateGuaranteeTypeAsync(GuaranteeType guaranteeType)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
