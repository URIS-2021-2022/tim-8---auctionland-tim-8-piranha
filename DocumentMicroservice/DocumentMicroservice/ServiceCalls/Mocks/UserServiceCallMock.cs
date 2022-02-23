using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.ServiceCalls.Mocks
{
    public class UserServiceCallMock<T> : IServiceCall<T>
    {

        public async Task<T> SendGetRequestAsync(string url)
        {
            UserDto user = new UserDto
            {
                nameU = "Dimitrije",
                surnameU = "Corlija",
                username = "corlija.dimitrije",
                password = "dimitrije123"
                
            };

            return await Task.FromResult((T)Convert.ChangeType(user, typeof(T)));
        }
    }
}
