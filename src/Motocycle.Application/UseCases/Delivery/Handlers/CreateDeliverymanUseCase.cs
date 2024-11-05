using MediatR;
using AutoMapper;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Application.UseCases.Base;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Models;
using Motocycle.Application.UseCases.Delivery.Request;
using Motocycle.Application.UseCases.Delivery.Response;

namespace Motocycle.Application.UseCases.Delivery.Handlers
{
    public class CreateDeliverymanUseCase : UseCaseBaseRequestToDomain<CreateDeliverymanRequest, Deliveryman, DeliverymanResponse>
    {

        public CreateDeliverymanUseCase(
            IMapper mapper,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IHandler<DomainNotification> notifications,
            IDeliverymanService baseService) : base(notifications, unitOfWork, mediator, baseService, mapper)
        {


        }

        public override async Task<DeliverymanResponse> HandleSafeMode(CreateDeliverymanRequest request, CancellationToken cancellationToken)
        {
            return await RegisterAsync(request);

        }

    }
}
