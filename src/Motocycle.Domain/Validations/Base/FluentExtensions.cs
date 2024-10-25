using System;
using FluentValidation;

namespace Motocycle.Domain.Validations.Base
{
    public static class FluentExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> SetMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> options, string stringName)
            => options.WithMessage(ValidatorOptions.Global.LanguageManager.GetString(stringName));

        public static IRuleBuilderOptions<T, Guid> IsGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder
                .NotEqual(Guid.Empty).SetMessage("GuidValidator");
        }
    }
}
