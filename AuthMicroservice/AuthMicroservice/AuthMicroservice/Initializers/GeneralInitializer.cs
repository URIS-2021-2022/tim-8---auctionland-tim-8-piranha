namespace AuthMicroservice.Initializers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// General initializator.
    /// </summary>
    public class GeneralInitializer : IInitializer
    {
        /// <summary>
        /// Method that initializes services for controllers.
        /// </summary>
        /// <param name="services">Services to be initialized.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}
