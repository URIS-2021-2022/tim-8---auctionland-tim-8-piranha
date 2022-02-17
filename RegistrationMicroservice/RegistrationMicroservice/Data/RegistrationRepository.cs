using AutoMapper;
using RegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Data
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly RegistrationContext context;
        private readonly IMapper mapper;

        public RegistrationRepository(RegistrationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public RegistrationConfirmation CreateRegistration(Registration registration)
        {
            var registrationEntity = context.Add(registration);
            return mapper.Map<RegistrationConfirmation>(registrationEntity.Entity);
        }

        public void DeleteRegistration(Guid RegistrationId)
        {
            var registration = GetRegistrationById(RegistrationId);

            context.Remove(registration);
        }

        public Registration GetRegistrationById(Guid RegistrationId)
        {
            return context.registration.FirstOrDefault(e => e.RegistrationId == RegistrationId);
        }

        public List<Registration> GetRegistrations()
        {
            return context.registration.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateRegistration(Registration registration)
        {

        }
    }
}
