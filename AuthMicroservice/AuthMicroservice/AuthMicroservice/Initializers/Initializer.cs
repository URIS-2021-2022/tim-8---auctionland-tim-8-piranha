namespace AuthMicroservice.Initializers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    /// <summary>
    /// Initializer implementation.
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Method used to initialize all that is needed on startup.
        /// </summary>
        /// <param name="services">Services to be initialized.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public static void InitializeAll(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(IInitializer).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInitializer>().ToList();

            installers.ForEach(installer => installer.InitializeServices(services, configuration));
        }
    }
}
