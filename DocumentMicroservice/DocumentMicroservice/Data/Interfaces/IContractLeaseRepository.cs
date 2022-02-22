using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Interfaces
{
    public interface IContractLeaseRepository 
    {
        Task<List<ContractLease>> GetContractLeaseAsync(string serialNumber = null);

        Task<ContractLease> GetContractLeaseByIdAsync(Guid contractLeaseID);

        Task<ContractLeaseConfirmation> CreateContractLeaseAsync(ContractLease contractLease);

        Task UpdateContractLeaseAsync(ContractLease contractLease);

        Task DeleteContractLeaseAsync(Guid contractLeaseID);

        Task<bool> SaveChangesAsync();
    }
}
