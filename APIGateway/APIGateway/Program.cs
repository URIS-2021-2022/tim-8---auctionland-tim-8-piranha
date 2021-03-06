namespace APIGateway
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Ocelot.Middleware;
    using Ocelot.DependencyInjection;
    using System;
    using System.IO;
    using Commons.ExceptionHandling;
    using APIGateway.Middlewares;
    using Microsoft.Net.Http.Headers;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder().Build().Run();
        }

        public static WebHostBuilder CreateHostBuilder() =>
            (WebHostBuilder) new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("ocelot.json")
                .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {
                s.AddOcelot();
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                var config = new OcelotPipelineConfiguration
                {
                    PreErrorResponderMiddleware = async (context, next) =>
                    {
                        try
                        {
                            await next.Invoke();
                        } catch (BaseException ex)
                        {
                            await ApiGatewayExceptionHandler.HandleExceptionAsync(context, ex);
                        }

                        await next.Invoke();
                    },
                    PreAuthenticationMiddleware = async (context, next) =>
                    {
                        (new TokenValidationMiddleware()).ValidateToken(context?.Request?.Headers[HeaderNames.Authorization]);

                        await next.Invoke();
                    }
                };

                app.UseOcelot(config).Wait();
            });
    }
}
