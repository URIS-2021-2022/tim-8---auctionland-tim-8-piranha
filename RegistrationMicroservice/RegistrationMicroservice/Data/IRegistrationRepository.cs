using RegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Data
{
    public interface IRegistrationRepository
    {
        RegistrationConfirmation CreateRegistration(Registration registration);

        List<Registration> GetRegistrations();

        Registration GetRegistrationById(Guid RegistrationId);

        void UpdateRegistration(Registration registration);

        void DeleteRegistration(Guid RegistrationId);

        bool SaveChanges();
    }
}
