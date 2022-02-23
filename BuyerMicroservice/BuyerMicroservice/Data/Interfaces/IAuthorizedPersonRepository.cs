using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
    public interface IAuthorizedPersonRepository
    {

        Task<List<AuthorizedPerson>> GetAuthorizedPersonAsync(string personalDocNum = null);

        Task<AuthorizedPerson> GetAuthorizedPersonByIdAsync(Guid authorizedPersonID);

        Task<AuthorizedPersonConfirmation> CreateAuthorizedPersonAsync(AuthorizedPerson authorizedPerson);

        Task UpdateAuthorizedPersonAsync(AuthorizedPerson authorizedPerson);

        Task AddBuyerToAuthorizedPerson(Buyer buyer, Guid authorizedPersonId);

        Task RemoveBuyerFromAuthorizedPerson(Buyer buyer, Guid authorizedPersonId);

        Task DeleteAuthorizedPersonAsync(Guid authorizedPersonID);

        Task<bool> SaveChangesAsync();
    }
}
