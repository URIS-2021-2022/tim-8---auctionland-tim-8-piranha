using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<RegistrationConfirmation> CreateRegistrationAsync(Registration registration)
        {
            var registrationEntity = await context.AddAsync(registration);
            return mapper.Map<RegistrationConfirmation>(registrationEntity.Entity);
        }

        public async Task DeleteRegistrationAsync(Guid RegistrationId)
        {
            var registration = await GetRegistrationByIdAsync(RegistrationId);

            context.Remove(registration);
        }

        public async Task<Registration> GetRegistrationByIdAsync(Guid RegistrationId)
        {
            return await context.registration.FirstOrDefaultAsync(e => e.RegistrationId == RegistrationId);
        }

        public async Task<List<Registration>> GetRegistrationsAsync()
        {
            return await context.registration.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateRegistrationAsync(Registration registration)
        {
            //updates registration
        }
    }
}
