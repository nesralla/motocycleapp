using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class JsonExtension
    {
        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    Converters = new[] { new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal } }
                };
            }
        }

        public static JsonSerializerSettings JsonSettingsIndented
        {
            get
            {
                return new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    Converters = new[] { new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal } }
                };
            }
        }

        public static string ToJson(this object objToJson, bool useSettings = false)
        {
            if (useSettings)
            {
                return JsonConvert.SerializeObject(objToJson, JsonSettings);
            }

            return JsonConvert.SerializeObject(objToJson);
        }

        public static string ToJsonIndented(this object objToJson)
        {
            return JsonConvert.SerializeObject(objToJson, JsonSettingsIndented);
        }

        public static (bool IsParseOK, string ParseValue, string ErrorMessage) TryParseToJson(this object objToJson, bool useSettings = false)
        {
            try
            {
                return (true, objToJson.ToJson(useSettings), default);
            }
            catch (Exception ex)
            {
                return (false, default, ex.GetErrorMsg());
            }
        }

        public static T ToObject<T>(this string stringToObject, bool useSettings = false)
        {
            if (useSettings)
            {
                return JsonConvert.DeserializeObject<T>(stringToObject, JsonSettings);
            }

            return JsonConvert.DeserializeObject<T>(stringToObject);
        }
        public static T ToObject<T>(this JObject obj)
            => obj.ToJson().ToObject<T>();

        public static JObject ToJObject(this object objToJObject)
           => objToJObject.ToJson().ToObject<JObject>();

        public static (bool IsParseOK, T ParseValue, string ErrorMessage) TryParseToObject<T>(this string stringToObject, bool useSettings = false)
        {
            try
            {
                return (true, stringToObject.ToObject<T>(useSettings), string.Empty);
            }
            catch (Exception ex)
            {
                return (false, default, ex.GetErrorMsg());
            }
        }
    }
}
