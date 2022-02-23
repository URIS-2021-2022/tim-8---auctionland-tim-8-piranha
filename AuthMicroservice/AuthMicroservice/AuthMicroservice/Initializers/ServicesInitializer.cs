using AuthMicroservice.Services.Abstractions;
using AuthMicroservice.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMicroservice.Initializers
{
    public class ServicesInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IClientService, ClientService>();
        }
    }
}
