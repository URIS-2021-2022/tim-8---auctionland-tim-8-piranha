namespace AuthMicroservice.Initializers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public interface IInitializer
    {
        void InitializeServices(IServiceCollection services, IConfiguration configuration);
    }
}
