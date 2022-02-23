namespace AuthMicroservice.Initializers
{
    using AuthMicroservice.Utils;
    using AuthMicroservice.Utils.LoggerService;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Method that initializes singletons accross used in the api.
    /// </summary>
    public class DependencyInjectionInitializer : IInitializer
    {
        /// <summary>
        /// Method used for dependency injection initialization.
        /// </summary>
        /// <param name="services">Services to initialize.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<JwtGenerator>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
