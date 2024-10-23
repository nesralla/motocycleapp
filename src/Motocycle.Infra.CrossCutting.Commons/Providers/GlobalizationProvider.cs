using System.Collections.Generic;
using System.Globalization;

namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public static class GlobalizationProvider
    {
        public static CultureInfo CultureInfoEnUS { get; } = new CultureInfo("en-US");
        public static List<CultureInfo> SupportedCultures { get; } = new List<CultureInfo> { CultureInfoEnUS };
    }
}
