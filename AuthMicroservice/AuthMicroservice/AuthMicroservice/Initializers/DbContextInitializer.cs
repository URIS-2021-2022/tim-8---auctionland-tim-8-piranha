namespace AuthMicroservice.Initializers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AuthMicroservice.Utils;

    /// <summary>
    /// Class that initializes database context.
    /// </summary>
    public class DbContextInitializer : IInitializer
    {
        /// <summary>
        /// Method used for database initialization.
        /// </summary>
        /// <param name="services">Services to initialize.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer"), x => x.MigrationsAssembly("AuthMicroservice")));
        }
    }
}
