using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
    public interface IContactPersonRepository
    {
        Task<List<ContactPerson>> GetContactPersonAsync(string name = null);

        Task<ContactPerson> GetContactPersonByIdAsync(Guid contactPersonID);

        Task<ContactPersonConfirmation> CreateContactPersonAsync(ContactPerson contactPerson);

        Task UpdateContactPersonAsync(ContactPerson contactPerson);

        Task DeleteContactPersonAsync(Guid contactPersonID);

       Task<bool> SaveChangesAsync();
    }
}
