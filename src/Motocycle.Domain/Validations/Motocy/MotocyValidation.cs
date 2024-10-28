
using FluentValidation;
using Motocycle.Domain.Validations.Base;

namespace Motocycle.Domain.Validations.Motocy
{
    public class MotocyValidation : BaseValidation<Models.Motocy>
    {
        protected void ValidateMotocy()
        {
            RuleFor(x => x.LicensePlate)
                .NotEmpty();
            RuleFor(x => x.Year)
                .NotEmpty();
            RuleFor(x => x.MotocyModel)
                  .NotEmpty();
        }
    }
}
