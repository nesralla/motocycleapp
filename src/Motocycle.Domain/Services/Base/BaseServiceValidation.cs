using System.Threading.Tasks;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Interfaces.Validation.Base;

namespace Motocycle.Domain.Services.Base
{
    public class BaseServiceValidation<TEntity> : BaseServiceEntity<TEntity> where TEntity : Entity, new()
    {
        private readonly IRegisterValidation<TEntity> _registerValidation;

        public BaseServiceValidation(IBaseRepository<TEntity> baseRepository,
            IHandler<DomainNotification> notifications,
            IRegisterValidation<TEntity> registerValidation)
            : base(baseRepository, notifications)
        {
            _registerValidation = registerValidation;
        }

        private async Task<bool> CallValidationAsync(TEntity entity, IBaseValidation<TEntity> validator, TEntity oldEntity)
        {
            var result = await validator.IsValidAsync(entity, oldEntity);
            if (!result.IsValid)
            {
                NotifyValidationErrors(result);
                return false;
            }

            return true;
        }

        protected async Task<bool> VerifyRegister(TEntity entity)
            => await CallValidationAsync(entity, _registerValidation, null);

        public override async Task<TEntity> RegisterAsync(TEntity entity)
        {
            if (!await CallValidationAsync(entity, _registerValidation, null))
                return default;

            return await base.RegisterAsync(entity);
        }
    }
}
