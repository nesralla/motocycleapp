using FluentValidation;
using Motocycle.Domain.Models;
using Motocycle.Domain.Validations.Base;

namespace Motocycle.Domain.Validations.ApiErrorLog
{
    public class ApiErrorLogValidation : BaseValidation<ErrorLog>
    {
        protected void ValidateRootCause()
        {
            RuleFor(x => x.RootCause)
                .NotEmpty();
        }

        protected void ValidateMessage()
        {
            RuleFor(x => x.Message)
                .NotEmpty();
        }

        protected void ValidateType()
        {
            RuleFor(x => x.Type)
                .NotEmpty();
        }
    }
}
