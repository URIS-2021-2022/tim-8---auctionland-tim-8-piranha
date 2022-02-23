using ComplaintMicroservice.Data;
using ComplaintMicroservice.Data.Complaint;
using ComplaintMicroservice.Data.Event;
using ComplaintMicroservice.Data.Status;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
using ComplaintMicroservice.ServiceCalls;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ComplaintMicroservice
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
            }).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IComplaintTypeRepository,ComplaintTypeRepository>();
            services.AddScoped<IComplaintStatusRepository, ComplaintStatusRepository>();
            services.AddScoped<IComplaintEventRepository, ComplaintEventRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IServiceCall<PublicBiddingDto>, ServiceCall<PublicBiddingDto>>();
            services.AddScoped<IServiceCall<BuyerDto>, ServiceCall<BuyerDto>>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComplaintMicroservice", Version = "v1" });
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                c.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContext<ComplaintContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ComplaintDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComplaintMicroservice v1"));
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
