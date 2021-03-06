using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlotMicroservice.Data.Interfaces;
using PlotMicroservice.Data.Repositories;
using PlotMicroservice.Entities;
using PlotMicroservice.Models;
using PlotMicroservice.ServiceCalls;
using PlotMicroservice.ServiceCalls.Mocks;
using PlotMicroservice.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PlotMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(setup => {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
              .AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IPlotCadastralMunicipalityRepository, PlotCadastralMunicipalityRepository>();
            services.AddScoped<IPlotCultureRepository, PlotCultureRepository>();
            services.AddScoped<IPlotWorkabilityRepository, PlotWorkabilityRepository>();
            services.AddScoped<IPlotPartFormOfOwnershipRepository, PlotPartFormOfOwnershipRepository>();
            services.AddScoped<IPlotPartClassRepository, PlotPartClassRepository>();
            services.AddScoped<IPlotPartProtectedZoneRepository, PlotPartProtectedZoneRepository>();
            services.AddScoped<IPlotRepository, PlotRepository>();
            services.AddScoped<IPlotPartRepository, PlotPartRepository>();
            
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddScoped<IServiceCall<BuyerDto>, BuyerServiceCallMock<BuyerDto>>();

            services.AddDbContext<PlotContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PlotDB")));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PlotMicroserviceOpenApiSpecification", new OpenApiInfo()
                {
                    Title = "Plot microservice API",
                    Version = "1",
                    Description = "Throughout this API, you can view or modify existing plots, also you can create new plots.",
                    Contact = new OpenApiContact
                    {
                        Name = "Andrija Pavlov",
                        Email = "pavlovandrija9@gmail.com",
                        Url = new Uri(Configuration["Urls:Contact"])
                    },
                    License = new OpenApiLicense
                    {
                        Name = "FTN license",
                        Url = new Uri(Configuration["Urls:License"])
                    }
                });

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                // Making path to XML file with comments
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                // Telling Swagger where file with XML comments is located
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/PlotMicroserviceOpenApiSpecification/swagger.json", "Plot microservice API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
