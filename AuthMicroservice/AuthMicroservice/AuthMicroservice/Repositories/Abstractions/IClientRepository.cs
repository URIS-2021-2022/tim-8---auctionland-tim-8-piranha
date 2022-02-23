namespace AuthMicroservice.Repositories.Abstractions
{
    using AuthMicroservice.Domain;
    using Commons.Repository;

    /// <summary>
    /// Client repository interface. 
    /// </summary>
    public interface IClientRepository : IBaseRepository<Client>
    {
    }
}
