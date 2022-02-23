using AuthMicroservice.Domain;
using Commons.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthMicroservice.Repositories.Abstractions
{
    public interface IClientRepository : IBaseRepository<Client>
    {
    }
}
