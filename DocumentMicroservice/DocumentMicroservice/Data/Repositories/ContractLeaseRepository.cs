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
<<<<<<< Updated upstream
=======
   
>>>>>>> Stashed changes
    public class ContractLeaseRepository : IContractLeaseRepository
    {
        private readonly DocumentContext context;
        private readonly IMapper mapper;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        public ContractLeaseRepository(DocumentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
<<<<<<< Updated upstream

        public async Task<ContractLeaseConfirmation> CreateContractLeaseAsync(ContractLease contractLease)
        {
           var createdEntity = await context.AddAsync(contractLease);
=======
        public async Task<ContractLeaseConfirmation> CreateContractLeaseAsync(ContractLease contractLease)
        {
            var createdEntity = await context.AddAsync(contractLease);
>>>>>>> Stashed changes
            return mapper.Map<ContractLeaseConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteContractLeaseAsync(Guid contractLeaseID)
        {
            var contractLease = await GetContractLeaseByIdAsync(contractLeaseID);
            context.Remove(contractLease);
        }

        public async Task<List<ContractLease>> GetContractLeaseAsync(string serialNumber = null)
        {
<<<<<<< Updated upstream
            return await context.contractLease.Where(o => o.serialNumber == null || serialNumber == null).ToListAsync();
=======
            return await context.contractLease.Where(o => (o.serialNumber == null || serialNumber == null)).ToListAsync();
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
=======
               kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
>>>>>>> Stashed changes
        }
    }
}
