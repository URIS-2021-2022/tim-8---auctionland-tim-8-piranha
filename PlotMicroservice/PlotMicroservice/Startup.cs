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
using PlotMicroservice.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
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

            services.AddDbContext<PlotContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PlotDB")));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PlotMicroserviceOpenApiSpecification", new OpenApiInfo()
                {
                    Title = "Plot microservice API",
                    Version = "1"
                });
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
