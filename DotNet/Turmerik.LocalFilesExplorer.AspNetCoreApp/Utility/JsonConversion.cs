using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IJsonConversion
    {
        StaticDataCache<JsonSerializerOpts, JsonSerializerTuple> SettingsMap { get; }
        IJsonConversionAdapter Adapter { get; }

        IJsonObjectDecorator<T> Decorator<T>(
            string rawJson,
            JsonSerializerOpts opts);

        IJsonObjectDecorator<T> Decorator<T>(
            string rawJson,
            bool useCamelCase = false,
            bool useStringEnumConverter = false);

        IJsonObjectDecorator<T> Decorator<T>(
            JObject rawObj,
            JsonSerializerOpts opts);

        IJsonObjectDecorator<T> Decorator<T>(
            JObject rawObj,
            bool useCamelCase = false,
            bool useStringEnumConverter = false);

        IJsonObjectDecorator<T> Decorator<T>(
            T data,
            JsonSerializerOpts opts);

        IJsonObjectDecorator<T> Decorator<T>(
            T data,
            bool useCamelCase = false,
            bool useStringEnumConverter = false);
    }

    public class JsonConversion : IJsonConversion
    {
        public JsonConversion()
        {
            SettingsMap = new StaticDataCache<JsonSerializerOpts, JsonSerializerTuple>(
                opts => JsonH.CreateJsonSerializerSettings(
                    opts).With(settings => new JsonSerializerTuple(
                        JsonSerializer.Create(settings), settings)));

            Adapter = new JsonConversionAdapter(SettingsMap);
        }

        public StaticDataCache<JsonSerializerOpts, JsonSerializerTuple> SettingsMap { get; }
        public IJsonConversionAdapter Adapter { get; }

        public IJsonObjectDecorator<T> Decorator<T>(
            string rawJson,
            JsonSerializerOpts opts) => new JsonObjectDecorator<T>(
                Adapter, rawJson, opts);

        public IJsonObjectDecorator<T> Decorator<T>(
            string rawJson,
            bool useCamelCase = false,
            bool useStringEnumConverter = false) => Decorator<T>(
                rawJson, new JsonSerializerOpts(
                    useCamelCase, true, useStringEnumConverter));

        public IJsonObjectDecorator<T> Decorator<T>(
            JObject rawObj,
            JsonSerializerOpts opts) => new JsonObjectDecorator<T>(
                Adapter, rawObj, opts);

        public IJsonObjectDecorator<T> Decorator<T>(
            JObject rawObj,
            bool useCamelCase = false,
            bool useStringEnumConverter = false) => Decorator<T>(
                rawObj, new JsonSerializerOpts(
                    useCamelCase, true, useStringEnumConverter));

        public IJsonObjectDecorator<T> Decorator<T>(
            T data,
            JsonSerializerOpts opts) => new JsonObjectDecorator<T>(
                Adapter, data, opts);

        public IJsonObjectDecorator<T> Decorator<T>(
            T data,
            bool useCamelCase = false,
            bool useStringEnumConverter = false) => Decorator(
                data, new JsonSerializerOpts(
                    useCamelCase, true, useStringEnumConverter));
    }
}
