using MediatR;
using AutoMapper;
using Motocycle.Application.UseCases.Base;
using Motocycle.Domain.Models;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;


namespace Motocycle.Application.UseCases.Transfer.Handlers
{
    public class RemoveMotocycleUseCase : UseCaseBaseRequestToDomain<RemoveMotocycleRequest, Motocy, MotoResponse>
    {
        private readonly IMotocyService _motocyService;
        public RemoveMotocycleUseCase(
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IMotocyService baseService,
            IMapper mapper) : base(notifications, unitOfWork, mediator, baseService, mapper)
        {
            _motocyService = baseService;
        }

        public override async Task<MotoResponse> HandleSafeMode(RemoveMotocycleRequest request, CancellationToken cancellationToken)
        {
            Action = $"Cancel a schedule ted where bankAccount: {request.Id}";
            var result = await _motocyService.RemoveMotocycleAsync(request.Id);
            await CommitAsync();
            var response = Mapper.Map<MotoResponse>(result);
            return response;
        }
    }
}
