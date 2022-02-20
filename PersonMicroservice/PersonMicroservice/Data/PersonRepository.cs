using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Data
{
    public class PersonRepository : IPersonRepository
    {

        private readonly PersonContext context;
        private readonly IMapper mapper;
        public PersonRepository(PersonContext context)
        {
            this.context = context;

        }
        public async Task<PersonConfirmation> CreatePerson(Person person)
        {
            var createdEntity = await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();

            return mapper.Map<PersonConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePerson(Guid personId)
        {
            var person = await GetPersonById(personId);

            context.Persons.Remove(person);
            await context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetAllPersons(string name = null)
        {
            return await context.Persons
                .Where(p => (name == null || p.Name == name))
                .ToListAsync();
        }

        public async Task<Person> GetPersonById(Guid personId)
        {
            return await context.Persons
                .FirstOrDefaultAsync(p => p.PersonId == personId);
        }

        public async Task UpdatePerson(Person person)
        {
            await context.SaveChangesAsync();
        }
    }
}
