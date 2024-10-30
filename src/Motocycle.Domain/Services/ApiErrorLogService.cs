using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Models;
using Motocycle.Domain.Services.Base;

namespace Motocycle.Domain.Services
{
    public class ApiErrorLogService : BaseServiceValidation<ErrorLog>, IApiErrorLogService
    {
        public ApiErrorLogService(IBaseRepository<ErrorLog> baseRepository,
            IHandler<DomainNotification> notifications,
            IRegisterValidation<ErrorLog> registerValidation)
            : base(baseRepository, notifications, registerValidation)
        {
        }
    }
}
