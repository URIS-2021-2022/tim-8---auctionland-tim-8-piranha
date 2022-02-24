namespace AuthMicroservice.Repositories.Implementations
{
    using AuthMicroservice.Domain;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Utils;
    using Common.Repository;

    /// <summary>
    /// Interface implementation of user type repository.
    /// </summary>
    public class UserTypeRepository : BaseRepository<UserType>, IUserTypeRepository
    {
        /// <summary>
        /// Default, needed, constructor for the client repository.
        /// </summary>
        /// <param name="context">Database context.</param>
        public UserTypeRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
