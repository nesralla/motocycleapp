using MediatR;
using AutoMapper;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Application.UseCases.Base;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Models;

namespace Motocycle.Application.UseCases.Moto.Handlers
{
    public class CreateMotocycleUseCase : UseCaseBaseRequestToDomain<MotoRequest, Motocy, MotoResponse>
    {
        public CreateMotocycleUseCase(
            IMapper mapper,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IHandler<DomainNotification> notifications,
            IMotocyService baseService) : base(notifications, unitOfWork, mediator, baseService, mapper)
        {

        }

        public override async Task<MotoResponse> HandleSafeMode(MotoRequest request, CancellationToken cancellationToken)
        {
            return await RegisterAsync(request);

        }

    }
}
