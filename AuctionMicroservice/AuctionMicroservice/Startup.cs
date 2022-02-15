using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
 

namespace AuctionMicroservice
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
                s.DisableDataAnnotationsValidation = true;

            });//.ConfigureApiBehaviorOptions(setupAction =>
            //{
            //    setupAction.InvalidModelStateResponseFactory = context =>
            //    {
            //        ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
            //        .GetRequiredService<ProblemDetailsFactory>();

            //        ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
            //            context.HttpContext,
            //            context.ModelState
            //            );

            //        problemDetails.Instance = context.HttpContext.Request.Path;

            //        var actionExecutingContext = context as ActionExecutingContext;

            //        if ((context.ModelState.ErrorCount > 0) &&
            //           (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
            //        {
            //            problemDetails.Type = "https://google.com"; //ina?e treba da stoji link ka stranici sa detaljima greške
            //            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            //            problemDetails.Title = "There has been an error during validation.";

            //            //sve vra?amo kao UnprocessibleEntity objekat
            //            return new UnprocessableEntityObjectResult(problemDetails)
            //            {
            //                ContentTypes = { "application/problem+json" }
            //            };
            //        };

            //        problemDetails.Status = StatusCodes.Status400BadRequest;
            //        problemDetails.Title = "There has been an error during parsing of the content.";
            //        return new BadRequestObjectResult(problemDetails)
            //        {
            //            ContentTypes = { "application/problem+json" }
            //        };

            //    };
            //});

            

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("AuctionOpenApiSpecification", new OpenApiInfo() 
                { 
                    Title = "AuctionMicroservice", 
                    Version = "1" ,
                    Description = "Using this API, it's possible to manipulate data regarding auction",
                    Contact = new OpenApiContact()
                    {
                        Name = "Luka Panic",
                        Email = "Panic.Luka@uns.ac.rs",
                        Url = new Uri("https://sova.uns.ac.rs/")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Licence",
                        Url = new Uri("https://sova.uns.ac.rs/"),

                    },
                    TermsOfService = new Uri("https://sova.uns.ac.rs/")
                });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                c.IncludeXmlComments(xmlCommentsPath);
            });

           



            //services.AddDbContext<DocumentationIndividualContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuctionDB")));
            //services.AddScoped<IDocumentationIndividualRepository, DocumentationIndividualRepository>();


            //services.AddDbContext<DocumentationLegalEntityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuctionDB")));
            //services.AddScoped<IDocumentationLegalEntityRepository, DocumentationLegalEntityRepository>();

            services.AddDbContext<AuctionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuctionDB")));
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IDocumentationIndividualRepository, DocumentationIndividualRepository>();
            services.AddScoped<IDocumentationLegalEntityRepository, DocumentationLegalEntityRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/AuctionOpenApiSpecification/swagger.json", "AuctionMicroservice 1"));
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
