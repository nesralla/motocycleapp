using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Converters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using OpenIddict.Validation.AspNetCore;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

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
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining(typeof(ApiErrorLogValidation));
                });

            FluentConfiguration.ConfigureFluent();

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

            services.AddStorageConfiguration(configuration);



            return services;
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseSwaggerDocumentation("Motocycla.Api v1");

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseRouting();

            app.UseCors("All");

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHsts();

            app.UseCustomAuth();


            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}