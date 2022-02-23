namespace AuthMicroservice.Initializers
{
    using AuthMicroservice.Services.Abstractions;
    using AuthMicroservice.Services.Implementations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Initializator for the services.
    /// </summary>
    public class ServicesInitializer : IInitializer
    {
        /// <summary>
        /// Method that is used to initialize services.
        /// </summary>
        /// <param name="services">Services to be initialized.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IClientService, ClientService>();
        }
    }
}
