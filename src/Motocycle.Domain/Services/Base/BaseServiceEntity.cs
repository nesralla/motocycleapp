using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Interfaces.Services.Base;
using Motocycle.Domain.Validations.Resources;

namespace Motocycle.Domain.Services.Base
{
    public abstract class BaseServiceEntity<TEntity> : BaseService, IBaseServiceEntity<TEntity> where TEntity : Entity
    {
        protected IBaseRepository<TEntity> BaseRepository { get; }

        public virtual IQueryable<TEntity> GetAllQuery => BaseRepository.GetAllQuery;

        public virtual IQueryable<TEntity> GetAllQueryAsNoTracking => BaseRepository.GetAllQueryNoTracking;

        protected BaseServiceEntity(IBaseRepository<TEntity> baseRepository,
                                 IHandler<DomainNotification> notifications)
            : base(notifications)
        {
            BaseRepository = baseRepository;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            if (id == default)
                return default;

            return await BaseRepository.GetByIdAsync(id);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
            => await BaseRepository.ExistsAsync(id);

        public virtual async Task<TEntity> RegisterAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateAt = DateTime.UtcNow;
                entity.ModifyAt = DateTime.UtcNow;
            }

            var registeredEntity = await BaseRepository.AddAsync(entity);
            return registeredEntity;
        }

        protected bool NotifyValidationErrors(ValidationResult validationResult)
        {
            var notifications = validationResult.Errors.Select(validationError => DomainNotification.ModelValidation(ValidationMessages.GetMessage(validationError.PropertyName), validationError.ErrorMessage)).ToList();
            if (!notifications.Any()) return true;

            notifications.ToList().ForEach(x =>
            {
                Notifications.Handle(x);
            });
            return false;
        }
    }
}
