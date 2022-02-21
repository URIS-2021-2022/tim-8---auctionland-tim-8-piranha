using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Services
{
    public interface IService<T>
    {
        Task<T> SendGetRequestAsync();
    }
}
