using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class ObjectExtensions
    {
        public static IQueryable<T> AsIQueryable<T>(this T obj)
        {
            List<T> list = new();
            list.Add(obj);

            return list.AsQueryable();
        }

        public static List<string> GetNullProperties(object obj)
        {
            List<string> properties = new();
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                var value = pi.GetValue(obj);
                if (value is null)
                    properties.Add(pi.Name);
            }

            return properties;
        }

        public static void UpdatePropertiesFrom<T>(this T target, T source)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Id")
                {
                    var sourceValue = property.GetValue(source);
                    if (sourceValue is not null && !string.IsNullOrEmpty(sourceValue.ToString()))
                    {
                        property.SetValue(target, sourceValue);
                    }
                }
            }
        }
    }
}