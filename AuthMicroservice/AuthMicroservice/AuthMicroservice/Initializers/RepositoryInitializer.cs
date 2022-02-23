namespace AuthMicroservice.Initializers
{
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Repositories.Implementations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class RepositoryInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
