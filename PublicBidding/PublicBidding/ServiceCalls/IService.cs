using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public interface IService<T>
    {
        Task<T> SendGetRequestAsync();
    }
}
