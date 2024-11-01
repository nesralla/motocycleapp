
using FluentValidation;
using Motocycle.Domain.Models;
using Motocycle.Domain.Validations.Base;

namespace Motocycle.Domain.Validations.Moto
{
    public class MotocyValidation : BaseValidation<Motocy>
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
