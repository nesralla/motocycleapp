
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class SolutionsExtension
    {
        public static bool IsGuid(this string stringValue)
        {
            string guidPattern = @"[a-fA-F0-9]{8}(\-[a-fA-F0-9]{4}){3}\-[a-fA-F0-9]{12}";
            if (string.IsNullOrEmpty(stringValue))
                return false;
            Regex guidRegEx = new Regex(guidPattern);
            return guidRegEx.IsMatch(stringValue);
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }

        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo is not null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs is not null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static string DoubleFormater(this double value)
            => value.ToString("0.00");

        public static List<T> GetAllEnumValues<T>()
            => Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(v => v)
                .ToList();


        public static bool ValidateCpf(this string value)
        {
            var documentNumber = value.RemoveNotNumbers();

            if (documentNumber.Length > 11)
                return false;

            while (documentNumber.Length != 11)
                documentNumber = '0' + documentNumber;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (documentNumber[i] != documentNumber[0])
                    igual = false;

            if (igual || documentNumber == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(documentNumber[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public static bool ValidateCnpj(this string value)
        {
            var documentNumber = value.RemoveNotNumbers();
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            documentNumber = documentNumber.Trim();
            documentNumber = documentNumber.Replace(".", "").Replace("-", "").Replace("/", "");
            if (documentNumber.Length != 14)
                return false;
            tempCnpj = documentNumber[..12];
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return documentNumber.EndsWith(digito);
        }


    }
}
