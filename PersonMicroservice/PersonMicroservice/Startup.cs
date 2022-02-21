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
using PersonMicroservice.Data;
using PersonMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PersonMicroservice
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
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();

            services.AddDbContext<PersonContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PersonDB")));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PersonMicroserviceOpenApiSpecification",
                    new OpenApiInfo()
                    {
                        Title = "Person API",
                        Version = "1",
                        //Često treba da dodamo neke dodatne informacije
                        Description = "Pomocu ovog API-ja može da se kreira licnost i komisija, modifikacija istih kao i pregled kreiranih licnosti i komisija.",
                        Contact = new OpenApiContact
                        {
                            Name = "Davor Jelic",
                            Email = "davorjelic@uns.ac.rs",
                            Url = new Uri("https://github.com/davorjelic")
                        }
                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/PersonMicroserviceOpenApiSpecification/swagger.json", "Person API");
                setupAction.RoutePrefix = ""; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
