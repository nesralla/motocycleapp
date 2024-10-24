using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value is null) { return null; }

            return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
