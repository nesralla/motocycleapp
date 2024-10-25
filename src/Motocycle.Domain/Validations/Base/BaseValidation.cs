using FluentValidation;
using System.Threading.Tasks;
using FluentValidation.Results;
using Motocycle.Domain.Core.Models;
using Motocycle.Infra.CrossCutting.Commons.Extensions;

namespace Motocycle.Domain.Validations.Base
{
    public abstract class BaseValidation<TEntity> : AbstractValidator<TEntity> where TEntity : Entity
    {
        protected const int DefaultVarcharLenght = 2000;

        protected void ValidateId() => RuleFor(x => x.Id).NotNull().IsGuid();

        protected static bool ValidateCpfCnpj(string value)
            => value.ValidateCpf() || value.ValidateCnpj();

        public virtual async Task<ValidationResult> IsValidAsync(TEntity entity, TEntity oldEntity = null)
        {
            var context = new ValidationContext<TEntity>(entity);
            context.RootContextData.Add("oldEntity", oldEntity);
            return await ValidateAsync(context);
        }

        public virtual ValidationResult IsValid(TEntity entity, TEntity oldEntity = null)
        {
            var context = new ValidationContext<TEntity>(entity);
            context.RootContextData.Add("oldEntity", oldEntity);
            return Validate(context);
        }

        protected TEntity GetOldEntity(ValidationContext<TEntity> context)
            => context.RootContextData.TryGetValue("oldEntity", out var oldEntity) ? (TEntity)oldEntity : null;
    }
}
