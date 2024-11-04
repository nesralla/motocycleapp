

using Motocycle.Domain.Interfaces.Validation;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Validations.Moto
{
    public class RegisterDeliverymanValidation : DeliverymanValidation, IRegisterValidation<Deliveryman>
    {
        public RegisterDeliverymanValidation()
        {
            ValidateDeliveryman();
        }
    }
}
