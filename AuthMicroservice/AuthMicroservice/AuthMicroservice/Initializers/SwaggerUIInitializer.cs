namespace AuthMicroservice.Initializers.Security
{
    using AuthMicroservice.Consts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Swagger UI initializer.
    /// </summary>
    public class SwaggerUIInitializer : IInitializer
    {
        /// <summary>
        /// Method used for swagger initialization.
        /// </summary>
        /// <param name="services">Services to be initialized.</param>
        /// <param name="configuration">Configuration to be applied.</param>
        public void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(setupAction => {
                setupAction.SwaggerDoc("v1", new OpenApiInfo
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

                setupAction.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
        }
    }
}
