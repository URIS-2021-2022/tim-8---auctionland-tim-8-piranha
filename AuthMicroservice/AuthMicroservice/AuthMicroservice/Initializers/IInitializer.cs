namespace AuthMicroservice.Initializers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Initializator interface.
    /// </summary>
    public interface IInitializer
    {
        /// <summary>
        /// Method to be used for initialization of services.
        /// </summary>
        /// <param name="services">Services to be initialized.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        void InitializeServices(IServiceCollection services, IConfiguration configuration);
    }
}
