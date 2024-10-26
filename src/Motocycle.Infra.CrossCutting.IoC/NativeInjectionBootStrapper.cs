using System;
using MediatR;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Infra.Data.Repositories.Base;
using Motocycle.Infra.Data.UoW;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Services;
using Motocycle.Infra.CrossCutting.Commons.HttpFactory.Interfaces;
using Motocycle.Infra.CrossCutting.Commons.HttpFactory.Policy;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Application.UseCases.ApiErrorLog.Handlers;
using Motocycle.Infra.CrossCutting.Commons.HttpFactory.Services;

namespace Motocycle.Infra.CrossCutting.IoC
{
    public static class NativeInjectionBootStrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterInfraService(services, configuration);
            RegisterDomainServices(services);
            RegisterApplicationServices(services, configuration);
            return services;
        }

        public static IServiceCollection RegisterInfraService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }

        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IApiErrorLogService, ApiErrorLogService>();

            services.AddScoped<IHttpFactoryService, HttpFactoryService>();
            services.AddHttpClient(typeof(HttpFactoryService).Name)
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler((services, request) => HttpFactoryPolicy.GetRetryPolicy(services, request));

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddScoped<ICallbackAppService, CallbackAppService>();

            services.AddScoped<IRequestHandler<GetErrorsRequest, GetErrorsResponse>, GetApiErrorLogUseCase>();
            //services.AddScoped<IRequestHandler<ApiErrorLogRequest, ApiErrorLogResponse>, CreateApiErrorLogHandler>();



            return services;
        }
    }
}
