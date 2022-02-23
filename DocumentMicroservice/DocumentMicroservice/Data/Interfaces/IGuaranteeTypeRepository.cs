using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Interfaces
{
    public interface IGuaranteeTypeRepository
    {
        Task<List<GuaranteeType>> GetGuaranteeTypeAsync(string guaranteeType = null);

        Task<GuaranteeType> GetGuaranteeTypeByIdAsync(Guid guaranteeTypeId);

        Task<GuaranteeTypeConfirmation> CreateGuaranteeTypeAsync(GuaranteeType guaranteeType);

        Task UpdateGuaranteeTypeAsync(GuaranteeType guaranteeType);

        Task DeleteGuaranteeTypeAsync(Guid guaranteeTypeId);

        Task<bool> SaveChangesAsync();
    }
}
