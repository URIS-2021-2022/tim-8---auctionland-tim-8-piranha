namespace AuthMicroservice.Repositories.Abstractions
{
    using AuthMicroservice.Domain;
    using Commons.Repository;

    /// <summary>
    /// Auth repository interface.
    /// </summary>
    public interface IAuthRepository : IBaseRepository<Client>
    {
    }
}
