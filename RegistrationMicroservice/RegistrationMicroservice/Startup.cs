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
using RegistrationMicroservice.Data;
using RegistrationMicroservice.Entities;
using RegistrationMicroservice.Models;
using RegistrationMicroservice.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RegistrationMicroservice
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




            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }
            ).AddXmlDataContractSerializerFormatters().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();

            });
            
            


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("RegistrationOpenApiSpecification", new OpenApiInfo 
                { 
                    Title = "RegistrationMicroservice", 
                    Version = "v1" ,
                    Description = "Using this API, it's possible to manipulate data regarding registrations",
                    Contact = new OpenApiContact()
                    {
                        Name = "Luka Panic",
                        Email = "Panic.Luka@uns.ac.rs",
                        
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Licence",
                        

                    },
                    



                });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //c.IncludeXmlComments(xmlCommentsPath);
            });


            services.AddDbContext<RegistrationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RegistrationDB")));
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IService<AuctionDto>, ServiceCall<AuctionDto>>();
            services.AddScoped<IService<BuyerDto>, BuyerMock<BuyerDto>>();
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/RegistrationOpenApiSpecification/swagger.json", "RegistrationMicroservice v1"));
            }

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
