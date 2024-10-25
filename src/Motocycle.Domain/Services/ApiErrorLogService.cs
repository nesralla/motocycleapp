using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Models;
using Motocycle.Domain.Services.Base;

namespace Motocycle.Domain.Services
{
    public class ApiErrorLogService : BaseServiceValidation<ApiErrorLog>, IApiErrorLogService
    {
        public ApiErrorLogService(IBaseRepository<ApiErrorLog> baseRepository,
            IHandler<DomainNotification> notifications,
            IRegisterValidation<ApiErrorLog> registerValidation)
            : base(baseRepository, notifications, registerValidation)
        {
        }
    }
}
