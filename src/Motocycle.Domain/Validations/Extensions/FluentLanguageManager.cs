using System.Linq;
using FluentValidation;
using System.Globalization;
using FluentValidation.Resources;
using FluentValidation.Validators;

namespace Motocycle.Domain.Validations.Extensions
{
    public class FluentLanguageManager : LanguageManager
    {
        public FluentLanguageManager()
        {
            AddTranslation("pt-BR", nameof(NotNullValidator<object, object>), "'{PropertyName}' é obrigatório.");
            AddTranslation("pt-BR", "OnlyDigitsValidator", "'{PropertyName}' s� aceita d�gitos.");
            AddTranslation("pt-BR", "GuidValidator", "'{PropertyName}' n�o pode estar vazio.");
            AddTranslation("pt-BR", "ExactLengthValidator2", "'{PropertyName}' deve ter o comprimento exato de {MaxLength} caracteres.");
            AddTranslation("pt-BR", "LengthValidator2", "'{PropertyName}' deve ter entre {MinLength} e {MaxLength} caracteres.");
        }

        private static string GetMessage(string key, string propertyName, object parameters = null, CultureInfo culture = null)
        {
            var result = ValidatorOptions.Global.LanguageManager.GetString(key, culture);
            var messageBuilder = ValidatorOptions.Global.MessageFormatterFactory();
            messageBuilder.AppendArgument("PropertyName", propertyName);
            if (parameters is not null)
            {
                parameters.GetType().GetProperties().ToList().ForEach(property =>
                {
                    messageBuilder.AppendArgument(property.Name, property.GetValue(parameters));
                });
            }
            result = messageBuilder.BuildMessage(result);

            return result;
        }

        public static string GetEnumValidator(string propertyName)
        {
            return GetMessage(nameof(EnumValidator<object, object>), propertyName);
        }

        public static string GetEqualValidator(string propertyName, object comparisonValue)
        {
            return GetMessage(nameof(EqualValidator<object, object>), propertyName, new { ComparisonValue = comparisonValue });
        }



        public static string GetLengthValidator2(string propertyName, object minLength, object maxLength)
        {
            return GetMessage("LengthValidator2", propertyName, new { MinLength = minLength, MaxLength = maxLength });
        }



        public static string GetMinimumLengthValidator2(string propertyName, object minLength)
        {
            return GetMessage("MinimumLengthValidator2", propertyName, new { MinLength = minLength });
        }

        public static string GetMaximumLengthValidator2(string propertyName, object maxLength)
        {
            return GetMessage("MaximumLengthValidator2", propertyName, new { MinLength = maxLength, MaxLength = maxLength });
        }
        public static string GetNotNullValidator(string propertyName)
        {
            return GetMessage(nameof(NotNullValidator<object, object>), propertyName);
        }

        public static string GetGuidValidator(string propertyName)
        {
            return GetMessage("GuidValidator", propertyName);
        }

        public static string GetFieldNotInformedValidator(string propertyName, string fieldName)
        {
            return GetMessage("FieldNotInformedValidator", propertyName, new { FieldName = fieldName });
        }

        public static string GetFieldNotInUseValidator(string propertyName)
        {
            return GetMessage("FieldNotInUseValidator", propertyName);
        }

        public static string GetFieldInUseValidator(string propertyName)
        {
            return GetMessage("FieldInUseValidator", propertyName);
        }

        public static string GetInvalidImageValidator(string propertyName)
        {
            return GetMessage("InvalidImageValidator", propertyName);
        }

        public static string GetForeignKeyValidator(string propertyName)
        {
            return GetMessage("ForeignKeyValidator", propertyName);
        }

        public static string GetUniqueValidator(string propertyName)
        {
            return GetMessage("UniqueValidator", propertyName);
        }

        public static string GetNotAllowedValidator()
        {
            return GetMessage("NotAllowedValidator", "");
        }

        public static string GetOnlyDigitsValidator(string propertyName)
        {
            return GetMessage("OnlyDigitsValidator", propertyName);
        }

        public static string GetMutualExcludeTransportProviderServiceValidator()
        {
            return GetMessage("MutualExcludeTransportProviderServiceValidator", "");
        }

        public static string GetAtLeastOneTransportProviderServiceValidator()
        {
            return GetMessage("AtLeastOneTransportProviderServiceValidator", "");
        }

        public static string GetExecutionRequestWithoutCallbackValidation()
        {
            return GetMessage("ExecutionRequestWithoutCallbackValidation", "");
        }
    }
}
