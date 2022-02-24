using RegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Data
{
    public interface IRegistrationRepository
    {
        Task<RegistrationConfirmation> CreateRegistrationAsync(Registration registration);

        Task<List<Registration>> GetRegistrationsAsync();

        Task<Registration> GetRegistrationByIdAsync(Guid RegistrationId);

        Task UpdateRegistrationAsync(Registration registration);

        Task DeleteRegistrationAsync(Guid RegistrationId);

        Task<bool> SaveChangesAsync();
    }
}
