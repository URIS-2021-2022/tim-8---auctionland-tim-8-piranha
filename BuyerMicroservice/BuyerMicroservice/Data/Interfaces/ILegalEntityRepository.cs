using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
    public interface ILegalEntityRepository
    {
        Task<List<LegalEntity>> GetLegalEntityAsync(string identificationNumber = null);

        Task<LegalEntity> GetLegalEntityByIdAsync(Guid legalEntityID);

        Task<LegalEntityConfirmation> CreateLegalEntityAsync(LegalEntity legalEntity);

        Task UpdateLegalEntityAsync(LegalEntity legalEntity);

        Task DeleteLegalEntityAsync(Guid legalEntityID);

        Task<bool> SaveChangesAsync();
    }
}
