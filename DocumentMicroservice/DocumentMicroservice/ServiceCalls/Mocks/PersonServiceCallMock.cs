using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.ServiceCalls.Mocks
{
    public class PersonServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            PersonDto person = new PersonDto
            {
                Name = "Dimitrije",
                Surname = "Corlija",
                Function = "Ministar"
               
            };

            return await Task.FromResult((T)Convert.ChangeType(person, typeof(T)));
        }
    }
}
