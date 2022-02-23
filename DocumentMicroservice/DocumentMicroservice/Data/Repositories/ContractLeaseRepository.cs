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
<<<<<<< HEAD

=======
<<<<<<< Updated upstream
=======
   
>>>>>>> Stashed changes
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
    public class ContractLeaseRepository : IContractLeaseRepository
    {
        private readonly DocumentContext context;
        private readonly IMapper mapper;
<<<<<<< HEAD
=======
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public ContractLeaseRepository(DocumentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
<<<<<<< HEAD

=======
<<<<<<< Updated upstream
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a

        public async Task<ContractLeaseConfirmation> CreateContractLeaseAsync(ContractLease contractLease)
        {
           var createdEntity = await context.AddAsync(contractLease);
<<<<<<< HEAD
=======
=======
        public async Task<ContractLeaseConfirmation> CreateContractLeaseAsync(ContractLease contractLease)
        {
            var createdEntity = await context.AddAsync(contractLease);
>>>>>>> Stashed changes
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
            return mapper.Map<ContractLeaseConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteContractLeaseAsync(Guid contractLeaseID)
        {
            var contractLease = await GetContractLeaseByIdAsync(contractLeaseID);
            context.Remove(contractLease);
        }

        public async Task<List<ContractLease>> GetContractLeaseAsync(string serialNumber = null)
        {
<<<<<<< HEAD
            return await context.contractLease.Where(o => o.serialNumber == null || serialNumber == null).ToListAsync();
=======
<<<<<<< Updated upstream
            return await context.contractLease.Where(o => o.serialNumber == null || serialNumber == null).ToListAsync();
=======
            return await context.contractLease.Where(o => (o.serialNumber == null || serialNumber == null)).ToListAsync();
>>>>>>> Stashed changes
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
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
<<<<<<< HEAD
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
=======
<<<<<<< Updated upstream
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
=======
               kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
>>>>>>> Stashed changes
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        }
    }
}
