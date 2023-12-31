using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class JsonH
    {
        public static JsonSerializerSettings CreateJsonSerializerSettings(
            JsonSerializerOpts opts)
        {
            var settings = new JsonSerializerSettings();

            if (opts.IgnoreNullValues)
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
            }

            if (opts.UseCamelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (opts.UseStringEnumConverter)
            {
                settings.Converters = settings.Converters ?? new List<JsonConverter>();

                settings.Converters.Add(
                    new StringEnumConverter());
            }

            return settings;
        }

        public static JToken TryGetToken(
            this JObject jObject,
            string[] altPropNames)
        {
            JToken token = null;

            foreach (var propName in altPropNames)
            {
                token = jObject.GetValue(propName);

                if (token != null)
                {
                    break;
                }
            }

            return token;
        }

        public static JToken TryGetToken(
            this JObject jObject,
            string propName,
            bool tryCamelCaseIfNotFound = false)
        {
            string[] altPropNames = null;

            if (tryCamelCaseIfNotFound)
            {
                string camelCasePropName = propName.DecapitalizeFirstLetter();

                if (camelCasePropName != propName)
                {
                    altPropNames = propName.Arr(camelCasePropName);
                }
            }

            altPropNames = altPropNames ?? propName.Arr();
            var token = jObject.TryGetToken(altPropNames);

            return token;
        }

        public static JToken TryGetToken(
            this JObject jObject,
            JsonPropRetrieverOpts opts) => jObject.TryGetToken(
                opts.PropName,
                opts.TryCamelCaseIfNotFound ?? false);

        public static TValue GetValueOrDefault<TValue>(
            this JToken token,
            Func<TValue> defaultPropValFactory = null)
        {
            TValue retVal;

            if (token != null)
            {
                retVal = token.ToObject<TValue>();
            }
            else if (defaultPropValFactory != null)
            {
                retVal = defaultPropValFactory();
            }
            else
            {
                retVal = default;
            }

            return retVal;
        }

        public static TValue TryGetValue<TValue>(
            this JObject jObject,
            string[] altPropNames,
            Func<TValue> defaultPropValFactory = null) => jObject.TryGetToken(
                altPropNames).GetValueOrDefault(
                defaultPropValFactory);

        public static TValue TryGetValue<TValue>(
            this JObject jObject,
            string propName,
            Func<TValue> defaultPropValFactory = null) => jObject.TryGetToken(
                propName).GetValueOrDefault(
                defaultPropValFactory);

        public static TValue TryGetValue<TValue>(
            this JObject jObject,
            string propName,
            bool tryCamelCaseIfNotFound,
            Func<TValue> defaultPropValFactory = null) => jObject.TryGetToken(
                propName,
                tryCamelCaseIfNotFound).GetValueOrDefault(
                defaultPropValFactory);

        public static TVal TryGetValue<TVal>(
            this JObject jObject,
            JsonPropRetrieverOpts opts) => jObject.TryGetValue<TVal>(
                opts.PropName,
                opts.TryCamelCaseIfNotFound ?? false);

        public static string EscapeQuotes(
            string idnf, char quote = '"')
        {
            string quoteStr = quote.ToString();
            string escapedStr = $"\\{quoteStr}";

            idnf = idnf.Replace(
                "\"", "\"\"").Replace(
                quoteStr, escapedStr);

            return idnf;
        }
    }

    public class JsonPropRetrieverOpts
    {
        public string PropName { get; set; }
        public bool? TryCamelCaseIfNotFound { get; set; }
    }
}
