using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class AuthorizedPersonMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            var authorizedPerson = new AuthorizedPersonDto
            {
                AuthorizedPerson = "Davor Jelic",
                NumberOfDocument = "013124124112",
                Housing = "Save Kovacevica 15 Novi Sad, Srbija"
            };

            return await Task.FromResult((T)Convert.ChangeType(authorizedPerson, typeof(T)));
        }
    }
}
