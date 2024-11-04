using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Motocycle.Domain.Models;
using Motocycle.Application.UseCases.Moto.Response;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Application.UseCases.Base;
using Motocycle.Application.UseCases.Moto.Request;
using Microsoft.EntityFrameworkCore;

namespace Motocycle.Application.UseCases.Moto.Handlers
{
    public class GetMotocycleByIdUseCase : UseCaseBaseRequestToDomain<GetMotocycleByIdRequest, Motocy, MotoResponse>
    {
        private readonly IMotocyService _motocyService;
        public GetMotocycleByIdUseCase(
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IMotocyService baseService,
            IMapper mapper) :
            base(notifications, unitOfWork, mediator, baseService, mapper)
        {
            _motocyService = baseService;
        }

        public override async Task<MotoResponse> HandleSafeMode(GetMotocycleByIdRequest request, CancellationToken cancellationToken)
        {
            var motocyList = await _motocyService.GetByIdAsync(request.Id);
            var response = Mapper.Map<MotoResponse>(motocyList);
            return response;
        }


    }
}
