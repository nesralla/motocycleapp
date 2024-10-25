using System.Linq;
using System.Collections.Generic;

namespace Motocycle.Domain.Validations.Resources
{
    public static class ValidationMessages
    {
        private static readonly Dictionary<string, string> Messages = new()
        {

        };

        public static string GetMessage(string key)
            => Messages.FirstOrDefault(x => x.Key.Equals(key)).Value ?? key;
    }
}
