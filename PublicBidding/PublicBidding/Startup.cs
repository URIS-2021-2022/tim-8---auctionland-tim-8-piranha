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
using PublicBidding.Data;
using PublicBidding.Entities;
using PublicBidding.Models;
using PublicBidding.ServiceCalls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PublicBidding
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

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IPublicBiddingRepository, PublicBiddingRepository>();
            services.AddScoped<IPublicBiddingService, PublicBiddingService>();
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddScoped<IService<AddressDto>, Service<AddressDto>>();
            services.AddScoped<IService<BuyerDto>, Service<BuyerDto>>();
            services.AddScoped<IService<AuthorizedPersonDto>, Service<AuthorizedPersonDto>>();
            services.AddScoped<IService<PlotPartDto>, Service<PlotPartDto>>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PublicBiddingMicroserviceOpenApiSpecification",
                    new OpenApiInfo()
                    {
                        Title = "PublicBidding API",
                        Version = "1",
                        //Često treba da dodamo neke dodatne informacije
                        Description = "Pomocu ovog API-ja moze da se kreira javno nadmetanje, da se vrsi modifikacija kao i pregled kreiranih javnih nadmetanja i statusa i tipova javnih nadmetanja.",
                        Contact = new OpenApiContact
                        {
                            Name = "Davor Jelic",
                            Email = "davorjelic@uns.ac.rs",
                            Url = new Uri(Configuration["Swagger:Github"])
                        }
                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContext<PublicBiddingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PublicBiddingDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/PublicBiddingMicroserviceOpenApiSpecification/swagger.json", "PublicBidding API");
                setupAction.RoutePrefix = ""; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
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
