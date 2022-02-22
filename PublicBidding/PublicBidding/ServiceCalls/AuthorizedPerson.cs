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
                name = "Davor",
                surname = "Jelic",
                personalDocNum = "013124124112",
                address = "Save Kovacevica 15 Novi Sad",
                country = "Srbija"
            };

            return await Task.FromResult((T)Convert.ChangeType(authorizedPerson, typeof(T)));
        }
    }
}
