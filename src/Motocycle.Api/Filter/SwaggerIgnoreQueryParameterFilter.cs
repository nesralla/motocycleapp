using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Motocycla.Api.Filter
{
    public class SwaggerIgnoreQueryParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var propertiesToRemove = context.MethodInfo.GetParameters()
                .Where(methodParam => methodParam.GetCustomAttribute(typeof(FromQueryAttribute), true) is not null)
                .SelectMany(methodParam => methodParam.ParameterType.GetProperties())
                .Where(ShouldRemove)
                .ToList();

            foreach (var parameter in operation.Parameters.ToList())
            {
                if (propertiesToRemove.Any(p => IsExactMatch(parameter.Name, p.Name)))
                {
                    operation.Parameters.Remove(parameter);
                }
            }
        }

        private static bool ShouldRemove(PropertyInfo property)
        {
            return property.GetCustomAttribute<SwaggerIgnoreAttribute>() is not null;
        }

        private static bool IsExactMatch(string parameterName, string propertyName)
        {
            if (parameterName.Equals(propertyName, StringComparison.Ordinal))
            {
                return true;
            }

            if (parameterName.StartsWith(propertyName + ".", StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }
    }
}