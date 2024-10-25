using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Application.UseCases.Base;
using Motocycle.Domain.Interfaces.Repositories;

namespace Motocycle.Application.UseCases.ApiErrorLog.Handlers
{
    public class CreateApiErrorLogUseCase : UseCaseBaseRequestToDomain<ApiErrorLogRequest, Domain.Models.ApiErrorLog, ApiErrorLogResponse>
    {
        public CreateApiErrorLogUseCase(
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IApiErrorLogService baseService,
            IMapper mapper) : base(notifications, unitOfWork, mediator, baseService, mapper)
        {
        }

        public override async Task<ApiErrorLogResponse> HandleSafeMode(ApiErrorLogRequest request, CancellationToken cancellationToken)
        {
            ScapeError = true;
            var entity = Mapper.Map<Domain.Models.ApiErrorLog>(request);
            await BaseDomainService.RegisterAsync(entity);
            await CommitAsync(true);
            return Mapper.Map<ApiErrorLogResponse>(entity);
        }

    }
}
