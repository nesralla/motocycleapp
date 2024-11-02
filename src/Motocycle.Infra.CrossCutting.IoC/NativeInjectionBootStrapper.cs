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
using Motocycle.Infra.Data.Repositories;
using Motocycle.Domain.Models;
using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Validations.Moto;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Application.UseCases.Moto.Handlers;
using Motocycle.Application.Events.MotocyEvent;
using Motocycle.Domain.Validations.ApiErrorLog;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic;

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
            services.AddScoped<IMotocyRepository, MotocyRepository>();

            return services;
        }

        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IApiErrorLogService, ApiErrorLogService>();
            services.AddScoped<IMotocyService, MotocyService>();
            services.AddScoped<IRegisterValidation<Motocy>, RegisterMotoValidation>();
            services.AddScoped<IRegisterValidation<ErrorLog>, RegisterApiErrorLogValidation>();

            services.AddScoped<IHttpFactoryService, HttpFactoryService>();
            services.AddHttpClient(typeof(HttpFactoryService).Name)
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler((services, request) => HttpFactoryPolicy.GetRetryPolicy(services, request));

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificationHandler<MotocyEvent>, MotocyEventHandler>();
            //services.AddScoped<IPublishTopic, PublishTopic>();

            services.AddScoped<IRequestHandler<GetErrorsRequest, GetErrorsResponse>, GetApiErrorLogUseCase>();
            services.AddScoped<IRequestHandler<ApiErrorLogRequest, ApiErrorLogResponse>, CreateApiErrorLogUseCase>();
            services.AddScoped<IRequestHandler<MotoRequest, MotoResponse>, CreateMotocycleUseCase>();





            return services;
        }
    }
}
