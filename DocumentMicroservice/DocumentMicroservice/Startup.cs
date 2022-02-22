using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Data.Repositories;
using DocumentMicroservice.Entities.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using DocumentMicroservice.ServiceCalls;
using DocumentMicroservice.Models;
using DocumentMicroservice.ServiceCalls.Mocks;

namespace DocumentMicroservice
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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // pogledaj ceo domen za servis i trazi konfiguracije za automapper(trazi profile da bi znao na koji nacin da mapira)

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentStatusRepository, DocumentStatusRepository>();
            services.AddScoped<IGuaranteeTypeRepository, GuaranteeTypeRepository>();
            services.AddScoped<IContractLeaseRepository, ContractLeaseRepository>();

            services.AddScoped<ILoggerService, LoggerService>();

            services.AddScoped<IServiceCall<BuyerDto>, BuyerServiceCallMock<BuyerDto>>();
            services.AddScoped<IServiceCall<PersonDto>, PersonServiceCallMock<PersonDto>>();
            services.AddScoped<IServiceCall<AuctionDto>, AuctionServiceCallMock<AuctionDto>>();
            services.AddScoped<IServiceCall<UserDto>, UserServiceCallMock<UserDto>>();
            services.AddScoped<IServiceCall<PlotDto>, PlotServiceCallMock<PlotDto>>();

            //services.AddScoped<IDocumentStatusRepository, PlotCadastralMunicipalityRepository>();
            //-- svaki put kada stigne novi rikvest od klijenta uvek se pravi nova instanca(to je vezano za parametar konstruktora u kontroleru)



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("UplataOpenApiSpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "DocumentMicroservice API",
                    Version = "1",
                    Description = "Pomocu ovog API-ja mogu da se vrše sve CRUD operacije u okviru agregata Dokument.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Dimitrije Corlija",
                        Email = "corlija.dimitrije822@gmail.com",
                        Url = new Uri("http://www.ftn.uns.ac.rs/")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "FTN licence",
                        Url = new Uri("http://www.ftn.uns.ac.rs/")
                    },
                    TermsOfService = new Uri("http://www.ftn.uns.ac.rs/uplataTermsOfService")
                });

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

               // c.IncludeXmlComments(xmlCommentsPath);
                

            });
            services.AddDbContext<DocumentContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DocumentDB")));
        } 


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Doslo je do greske. Molim Vas pokusajte kasnije!");
                    });
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/DocumentStatusApiSpecification/swagger.json", "DocumentMicroservice");
               
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

