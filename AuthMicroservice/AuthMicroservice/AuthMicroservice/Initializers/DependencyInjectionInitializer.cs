namespace AuthMicroservice.Initializers
{
    using AuthMicroservice.Services.Abstractions;
    using AuthMicroservice.Services.Implementations;
    using AuthMicroservice.Utils;
    using AuthMicroservice.Utils.LoggerService;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyInjectionInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<JwtGenerator>();
            services.AddSingleton<ILoggerService, LoggerService>();
        }
    }
}
