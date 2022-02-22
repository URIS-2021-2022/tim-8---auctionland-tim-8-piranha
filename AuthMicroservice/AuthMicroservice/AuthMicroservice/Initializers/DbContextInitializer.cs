using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AuthMicroservice.Initializers
{
    public class DbContextInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DbContext>(options => options.UseMySql(
                configuration.GetConnectionString("MySql"), 
                new MySqlServerVersion(new Version(8, 0, 16))));
        }
    }
}
