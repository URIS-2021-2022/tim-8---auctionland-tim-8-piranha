using AuthMicroservice.Domain;
using AuthMicroservice.Repositories.Abstractions;
using AuthMicroservice.Utils;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthMicroservice.Repositories.Implementations
{
    public class AuthRepository : BaseRepository<Client>, IAuthRepository
    {
        public AuthRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
