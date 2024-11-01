using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Motocycle.Infra.Data.Context;
using Motocycle.Api.Configurations.HealthChecks;
using Motocycle.Api.Filter;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Domain.Validations.Extensions;
using Motocycle.Api.Configurations.Swagger;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Infra.Data;
using Motocycle.Api.Middleware;
using Motocycle.Infra.CrossCutting.Commons.Middlewares;
using SQS.ServiceBus.Configurations;
using FluentValidation.AspNetCore;
using Motocycle.Domain.Validations.ApiErrorLog;
using Amazon.Extensions.NETCore.Setup;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.Runtime;
using Amazon.SQS;
using Motocycle.Infra.CrossCutting.MessageBroker.Configuration;

namespace Motocycle.Api.Configurations.Api
{
    internal static class ApiConfig
    {
        public static IServiceCollection ConfigureStartupApi(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.LoadConfiguration(configuration);
            services.AddDbContext<ApplicationDbContext>();

            services.AddHealthCheckSetup();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                    options.Filters.Add(typeof(ValidationFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.AllowInputFormatterExceptionMessages = true;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApiErrorLogValidation>());



            services.AddWebApiVersioning();

            services.AddSwaggerDocumentation(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("All",
                    builder =>
                        builder
                            .WithExposedHeaders("X-Pagination")
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            //services.AddAwsSnsConfiguration(configuration);
            services.AddServiceBusConfiguration(configuration);

            services.AddHostedService<Application.MessageBroker.Worker>();


            return services;
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UsePathBase($"/{AppProvider.Name}");
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(GlobalizationProvider.CultureInfoEnUS),
                SupportedCultures = GlobalizationProvider.SupportedCultures,
                SupportedUICultures = GlobalizationProvider.SupportedCultures
            });

            app.UpdateDatabase();

            app.UseAppHealthChecks();

            app.UseSwaggerDocumentation("Motocycle.Api v1");

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseRouting();

            app.UseCors("All");

            app.UseStaticFiles();

            app.UseHsts();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}