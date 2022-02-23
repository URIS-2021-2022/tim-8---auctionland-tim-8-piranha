namespace AuthMicroservice.Repositories.Abstractions
{
    using AuthMicroservice.Domain;
    using Commons.Repository;

    public interface IAuthRepository : IBaseRepository<Client>
    {
    }
}
