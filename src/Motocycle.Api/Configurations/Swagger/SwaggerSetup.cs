using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Motocycle.Api.Configurations.Swagger
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Motocycle.Api", Version = "v1" });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                c.AddServer(new OpenApiServer()
                {
                    Url = $"/{AppProvider.Name}"
                });

                c.CustomSchemaIds(x => x.FullName);
                c.ExampleFilters();
                c.OperationFilter<SwaggerIgnoreQueryParameterFilter>();
                c.SchemaFilter<SwaggerSkipPropertyFilter>();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string name)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{AppProvider.Name}/swagger/v1/swagger.json", name);
                c.RoutePrefix = "api-docs";
            });
            return app;
        }
    }
}