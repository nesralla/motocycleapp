

using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Validations.Moto
{
    public class RegisterMotoValidation : MotocyValidation, IRegisterValidation<Motocy>
    {
        public RegisterMotoValidation()
        {
            ValidateMotocy();
        }
    }
}
