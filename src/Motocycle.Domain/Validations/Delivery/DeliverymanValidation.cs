
using FluentValidation;
using Motocycle.Domain.Models;
using Motocycle.Domain.Validations.Base;

namespace Motocycle.Domain.Validations.Moto
{
    public class DeliverymanValidation : BaseValidation<Deliveryman>
    {
        protected void ValidateDeliveryman()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.NationalID)
                .NotEmpty();
            RuleFor(x => x.DateBorn)
                  .NotEmpty();
            RuleFor(x => x.DriveLicense)
                 .NotEmpty();
            RuleFor(x => x.LicenseType)
                 .NotEmpty();
        }
    }
}
