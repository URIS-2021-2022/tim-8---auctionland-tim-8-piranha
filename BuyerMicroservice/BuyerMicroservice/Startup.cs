using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Data.Repositories;
using BuyerMicroservice.Entities.Context;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using BuyerMicroservice.ServiceCalls;
using BuyerMicroservice.Models.Buyer;
using BuyerMicroservice.Models;
using BuyerMicroservice.ServiceCalls.Mocks;
using System.Reflection;
using System.IO;

namespace BuyerMicroservice
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
            }).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IAuthorizedPersonRepository, AuthorizedPersonRepository>();
            services.AddScoped<IContactPersonRepository, ContactPersonRepository>();
            services.AddScoped<IBoardNumberRepository, BoardNumberRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IServiceCall<AddressDto>, AddressServiceCallMock<AddressDto>>();
            services.AddScoped<IServiceCall<PaymentDto>, PaymentServiceCallMock<PaymentDto>>();

            services.AddScoped<IPriorityRepository, PriorityRepository>();



            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("PlotMicroserviceOpenApiSpecification", new OpenApiInfo()
                {
                    Title = "Plot microservice API",
                    Version = "1",
                    Description = "Throughout this API, you can view or modify existing plots, also you can create new plots.",
                    Contact = new OpenApiContact
                    {
                        Name = "Dimitrije Corlija",
                        Email = "corlija.dimitrije822@gmail.com",
                        Url = new Uri("https://github.com/Dimitrije-Corlija")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "FTN license",
                        Url = new Uri("http://www.ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka")
                    }
                });
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                // Making path to XML file with comments
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                // Telling Swagger where file with XML comments is located
                c.IncludeXmlComments(xmlCommentsPath);
            });
            services.AddDbContext<BuyerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BuyerDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuyerMicroservice v1"));*/
            }
            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/BuyerMicroserviceOpenApiSpecification/swagger.json", "Buyer microservice API");
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
