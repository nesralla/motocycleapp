using FluentValidation.Results;
using Motocycle.Domain.Core.Models;
using System.Threading.Tasks;

namespace Motocycle.Domain.Interfaces.Validation.Base
{
    public interface IBaseValidation<TEntity> where TEntity : Entity
    {
        Task<ValidationResult> IsValidAsync(TEntity entity, TEntity oldEntity);

        ValidationResult IsValid(TEntity entity, TEntity oldEntity);
    }
}
