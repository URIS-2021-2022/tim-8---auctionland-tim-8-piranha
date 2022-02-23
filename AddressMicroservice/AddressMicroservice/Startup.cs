using AddressMicroservice.Data.Interfaces;
using AddressMicroservice.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AddressMicroservice.Filters;
using AddressMicroservice.Entities.Contexts;
using Microsoft.EntityFrameworkCore;
using DocumentMicroservice.ServiceCalls;

namespace AddressMicroservice
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
            services.AddDbContext<AddressContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AddressDB")));

            services.AddControllers(setup =>
            {
                setup.Filters.Add<ApiExceptionFilterAttribute>();
                setup.ReturnHttpNotAcceptable = true;//SVAKI PUT KADA STIGNE NESTO STO NISAM PODRZAO VRATI ODGOVARAJUCI NOT EXECTABLE STATUS I KAZI KLIJENTU DA TO NIJE PODRZANO
            })
                .AddXmlDataContractSerializerFormatters();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // pogledaj ceo domen za servis i trazi konfiguracije za automapper(trazi profile da bi znao na koji nacin da mapira)

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IStateRepository, StateRepository>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ILoggerService, LoggerService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressMicroservice", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressMicroservice v1"));
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
