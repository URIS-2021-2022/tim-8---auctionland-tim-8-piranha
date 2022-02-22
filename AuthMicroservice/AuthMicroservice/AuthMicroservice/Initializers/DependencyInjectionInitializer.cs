using AuthMicroservice.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthMicroservice.Initializers
{
    public class DependencyInjectionInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<JwtGenerator>();

            services.AddSingleton<ModelMapper>();
        }
    }
}
