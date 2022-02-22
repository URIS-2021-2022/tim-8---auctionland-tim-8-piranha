namespace AuthMicroservice.Initializers.Security
{
    using AuthMicroservice.Consts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public class SwaggerUIInitializer : IInitializer
    {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = GeneralConsts.MICROSERVICE_NAME,
                    Description = "API Documentation",
                    Contact = new OpenApiContact()
                    {
                        Name = "Stefan Radojevic",
                        Email = "radojevic.stefan.sr@gmail.com",
                        Url = new System.Uri("https://github.com/Stefi99R")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new System.Uri("https://opensource.org/licenses/MIT")
                    },
                });
            });
        }
    }
}
