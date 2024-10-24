using System;
using Castle.Core.Internal;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class StringExtension
    {
        public static string FormatNumber(this double num)
            => num.ToString("N0", CultureInfo.GetCultureInfo("pt-BR"));

        public static string BrFormatMoney(this double num)
            => num.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));

        public static string LastCardNumbers(this string str)
            => !str.Length.Equals(16) ? string.Empty : str.Substring(12, 4);

        public static string DocumentFormatter(this string str) => str.Length switch
        {
            11 => $"{Convert.ToUInt64(str):000\\.000\\.000\\-00}",
            14 => $"{Convert.ToUInt64(str):00\\.000\\.000\\/0000\\-00}",
            _ => str
        };

        public static bool IsBelongsToSource(this string contractor, string branch)
        {
            branch.RemoveNotNumbers();
            contractor.RemoveNotNumbers();
            string cnpjBranch = branch[..8];
            string cnpjContractor = contractor[..8];
            return cnpjBranch.Equals(cnpjContractor);
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            string cleanedString = Regex.Replace(str, @"[^a-zA-Z0-9\s]", " ");
            return Regex.Replace(cleanedString, @"\s+", " ").Trim();
        }

        public static string RemoveNotNumbers(this string value)
        {
            Regex reg = new(@"[^0-9]");
            string ret = reg.Replace(value, string.Empty);
            return ret;
        }

        public static string DbStringFormat(this string value, string host, string user, string secret)
            => value.Replace("{Host}", host).Replace("{User}", user).Replace("{Secret}", secret);

        public static string RntcFormater(this string value)
            => value?.PadLeft(9, '0');

        public static string CargoTypeFormater(this string value)
            => value?.PadLeft(4, '0') ?? "0001";

        public static string MessageFormater(this string value, string concatValue)
            => (string.IsNullOrEmpty(value) ? concatValue : $"{value} | {concatValue}").Replace("_", "");
    }
}
