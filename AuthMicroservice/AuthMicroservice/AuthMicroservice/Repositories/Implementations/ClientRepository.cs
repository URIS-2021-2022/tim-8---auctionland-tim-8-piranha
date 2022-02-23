namespace AuthMicroservice.Repositories.Implementations
{
    using AuthMicroservice.Domain;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Utils;
    using Common.Repository;
    using Microsoft.EntityFrameworkCore;

    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
