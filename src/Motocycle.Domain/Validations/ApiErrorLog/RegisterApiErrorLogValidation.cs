using Motocycle.Domain.Interfaces.Services;
using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Validations.ApiErrorLog
{
    public class RegisterApiErrorLogValidation : ApiErrorLogValidation, IRegisterValidation<ErrorLog>
    {
        public RegisterApiErrorLogValidation()
        {
            ValidateRootCause();
            ValidateMessage();
            ValidateType();
        }


    }
}
