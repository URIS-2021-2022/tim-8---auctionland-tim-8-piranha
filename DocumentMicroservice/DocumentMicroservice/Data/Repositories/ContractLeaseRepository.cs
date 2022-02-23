using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Entities.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Repositories
{

    public class ContractLeaseRepository : IContractLeaseRepository
    {
        private readonly DocumentContext context;
        private readonly IMapper mapper;


        public ContractLeaseRepository(DocumentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ContractLeaseConfirmation> CreateContractLeaseAsync(ContractLease contractLease)
        {
           var createdEntity = await context.AddAsync(contractLease);

            return mapper.Map<ContractLeaseConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteContractLeaseAsync(Guid contractLeaseID)
        {
            var contractLease = await GetContractLeaseByIdAsync(contractLeaseID);
            context.Remove(contractLease);
        }

        public async Task<List<ContractLease>> GetContractLeaseAsync(string serialNumber = null)
        {
            return await context.contractLease.Where(o => o.serialNumber == null || serialNumber == null).ToListAsync();


        }

        public async Task<ContractLease> GetContractLeaseByIdAsync(Guid contractLeaseID)
        {
            return await context.contractLease.FirstOrDefaultAsync(o => o.contractLeaseID == contractLeaseID);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateContractLeaseAsync(ContractLease contractLease)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */

        }
    }
}
