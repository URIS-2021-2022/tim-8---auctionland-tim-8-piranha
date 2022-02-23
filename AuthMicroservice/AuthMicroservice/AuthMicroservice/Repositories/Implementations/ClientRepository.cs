namespace AuthMicroservice.Repositories.Implementations
{
    using AuthMicroservice.Domain;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Utils;
    using Common.Repository;

    /// <summary>
    /// Client repository interface implementation.
    /// </summary>
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        /// <summary>
        /// Default, needed, constructor for the client repository.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ClientRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
