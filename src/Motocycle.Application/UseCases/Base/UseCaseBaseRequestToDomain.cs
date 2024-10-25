using System;
using MediatR;
using AutoMapper;
using Motocycle.Application.Interfaces.Base;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Interfaces.Services.Base;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Domain.Interfaces.Repositories;

namespace Motocycle.Application.UseCases.Base
{
    public abstract class UseCaseBaseRequestToDomain<TRequest, TDomainModel, TResponse> : UseCaseBase<TRequest, TResponse>, IUseCaseBase<TRequest, TResponse>
        where TRequest : CommandRequest<TResponse>, new()
        where TDomainModel : Entity, new()
    {
        protected IBaseServiceEntity<TDomainModel> BaseDomainService { get; }
        protected IMapper Mapper { get; }

        protected UseCaseBaseRequestToDomain(
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IBaseServiceEntity<TDomainModel> baseService,
            IMapper mapper) : base(notifications, unitOfWork, mediator)
        {
            BaseDomainService = baseService;
            Mapper = mapper;
        }

        public async Task<TResponse> RegisterAsync(TRequest request)
        {
            var registerCommand = Mapper.Map<TDomainModel>(request);
            Notifications.LogInfo($"Register {nameof(registerCommand)} with: {registerCommand.ToJson(true)}");
            registerCommand = await BaseDomainService.RegisterAsync(registerCommand);
            await CommitAsync();

            var registerResponse = Mapper.Map<TResponse>(registerCommand);
            return registerResponse;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
