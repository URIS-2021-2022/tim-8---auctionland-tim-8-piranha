namespace AuthMicroservice.Repositories.Implementations
{
    using AuthMicroservice.Domain;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Utils;
    using Common.Repository;

    /// <summary>
    /// Auth repository interface implementation.
    /// </summary>
    public class AuthRepository : BaseRepository<Client>, IAuthRepository
    {
        /// <summary>
        /// Default, needed, constructor for the auth repository.
        /// </summary>
        /// <param name="context">Database context.</param>
        public AuthRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
