namespace AuthMicroservice.Initializers.Security
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class SecurityInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AudienceModel>(configuration.GetSection("Audience"));
        }
    }
}
