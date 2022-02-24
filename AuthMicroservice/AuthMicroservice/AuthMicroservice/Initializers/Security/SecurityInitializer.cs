namespace AuthMicroservice.Initializers.Security
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Security initializer class.
    /// </summary>
    public class SecurityInitializer : IInitializer
    {
        /// <summary>
        /// Method that initializes security on api startup.
        /// </summary>
        /// <param name="services">Services to configure.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AudienceModel>(configuration.GetSection("Audience"));
        }
    }
}
