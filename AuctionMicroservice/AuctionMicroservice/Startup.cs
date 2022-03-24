using AuctionMicroservice.Data;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AuctionMicroservice.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS",
                builder => builder.WithOrigins("http://localhost:4200")

                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                );
                
            });

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;

            }
            ).AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
                s.DisableDataAnnotationsValidation = true;

            }).AddNewtonsoftJson();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:40003",
                    ValidAudience = "http://localhost:40003",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@123"))
                    

                };
            });
            
            //.ConfigureApiBehaviorOptions(setupAction =>
            
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
                    Description = "Using this API, it's possible to manipulate data regarding auctions",
                    Contact = new OpenApiContact()
                    {
                        Name = "Luka Panic",
                        Email = "Panic.Luka@uns.ac.rs",
                        
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Licence"
                        

                    },
                });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                c.IncludeXmlComments(xmlCommentsPath);
            });

           



            

            services.AddDbContext<AuctionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuctionDB")));
            services.AddDbContext<RegistrationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UserDB")));
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IDocumentationIndividualRepository, DocumentationIndividualRepository>();
            services.AddScoped<IDocumentationLegalEntityRepository, DocumentationLegalEntityRepository>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped <IService<PublicBiddingDto>, PublicBiddingMock<PublicBiddingDto>>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }





        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/AuctionOpenApiSpecification/swagger.json", "AuctionMicroservice 1"));
            }

            

            app.UseHttpsRedirection();


            app.UseCors("EnableCORS");
            app.UseRouting();


            //app.UseCors(x => x
            //.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader());

            

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
