namespace AuthMicroservice.Initializers
{
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Repositories.Implementations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Repositories initializer.
    /// </summary>
    public class RepositoryInitializer : IInitializer
    {
        /// <summary>
        /// Method that initializes scoped repositories.
        /// </summary>
        /// <param name="services">Services to be initialized.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
        }
    }
}
