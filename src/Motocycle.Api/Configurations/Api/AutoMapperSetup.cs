using System;
using Microsoft.Extensions.DependencyInjection;

namespace Motocycle.Api.Configurations.Api
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(RequestToDomainMappingProfile));

            AutoMapperConfig.RegisterMappings();

            return services;
        }
    }
}
