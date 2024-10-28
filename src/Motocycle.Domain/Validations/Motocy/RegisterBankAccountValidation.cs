

using Motocycle.Domain.Interfaces.Validation;

namespace Motocycle.Domain.Validations.Motocy
{
    public class RegisterMotoValidation : MotocyValidation, IRegisterValidation<Models.Motocy>
    {
        public RegisterMotoValidation()
        {
            ValidateMotocy();
        }
    }
}
