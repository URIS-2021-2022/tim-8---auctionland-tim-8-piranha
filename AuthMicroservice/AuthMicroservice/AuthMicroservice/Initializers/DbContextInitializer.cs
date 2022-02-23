using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AuthMicroservice.Utils;

namespace AuthMicroservice.Initializers
{
    public class DbContextInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer"), x => x.MigrationsAssembly("AuthMicroservice")));
        }
    }
}
