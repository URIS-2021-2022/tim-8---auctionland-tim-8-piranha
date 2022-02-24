using PersonMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Data
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllPersons(string name = null);

        Task<Person> GetPersonById(Guid personId);

        Task<PersonConfirmation> CreatePerson(Person person);

        Task DeletePerson(Guid personId);

        Task UpdatePerson(Person person);
    }
}
