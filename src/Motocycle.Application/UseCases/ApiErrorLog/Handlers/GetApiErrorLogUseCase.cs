using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Application.UseCases.Base;
using Motocycle.Domain.Interfaces.Repositories;

namespace Motocycle.Application.UseCases.ApiErrorLog.Handlers
{
    public class GetApiErrorLogUseCase : UseCaseBaseRequestToDomain<GetErrorsRequest, Domain.Models.ApiErrorLog, GetErrorsResponse>
    {
        public GetApiErrorLogUseCase(
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IApiErrorLogService baseService,
            IMapper mapper) : base(notifications, unitOfWork, mediator, baseService, mapper)
        {
        }

        public override async Task<GetErrorsResponse> HandleSafeMode(GetErrorsRequest request, CancellationToken cancellationToken)
        {
            var entities = await BaseDomainService.GetAllQueryAsNoTracking
                .Where(x => x.Timestamp >= request.StartDate && x.Timestamp <= request.EndDate)
                .ToListAsync(cancellationToken);

            var response = new GetErrorsResponse
            {
                Data = Mapper.Map<List<ApiErrorLogResponse>>(entities),
                Format = request.Format
            };

            return response.FormaterReport();
        }
    }
}
