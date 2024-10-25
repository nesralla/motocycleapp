using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Interfaces.Validation.Base;

namespace Motocycle.Domain.Interfaces.Validation
{
    public interface IRegisterValidation<TEntity> : IBaseValidation<TEntity> where TEntity : Entity
    {
    }
}
