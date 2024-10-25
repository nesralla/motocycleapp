using Motocycle.Domain.Interfaces.Validation;

namespace Motocycle.Domain.Validations.ApiErrorLog
{
    public class RegisterApiErrorLogValidation : ApiErrorLogValidation
    {
        public RegisterApiErrorLogValidation()
        {
            ValidateRootCause();
            ValidateMessage();
            ValidateType();
        }
    }
}
