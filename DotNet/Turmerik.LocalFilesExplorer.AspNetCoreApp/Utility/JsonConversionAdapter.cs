using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IJsonConversionAdapter
    {
        StaticDataCache<JsonSerializerOpts, JsonSerializerTuple> SettingsMap { get; }

        string Serialize(
            object obj,
            JsonSerializerOpts opts,
            Formatting formatting = Formatting.Indented);

        string Serialize(
            object obj,
            bool useCamelCase = false,
            bool ignoreNullValues = true,
            Formatting formatting = Formatting.Indented,
            bool useStringEnumConverter = true);

        object Deserialize(
            string json,
            JsonSerializerOpts opts);

        object Deserialize(
            string json,
            bool useCamelCase = false,
            bool useStringEnumConverter = true);

        TData Deserialize<TData>(
            string json,
            JsonSerializerOpts opts);

        TData Deserialize<TData>(
            string json,
            bool useCamelCase = false,
            bool useStringEnumConverter = true);

        JObject ToJObject(
            object obj,
            JsonSerializerOpts opts);

        JObject ToJObject(
            object obj,
            bool useCamelCase = false,
            bool useStringEnumConverter = true);
    }

    public class JsonConversionAdapter : IJsonConversionAdapter
    {
        public JsonConversionAdapter(
            StaticDataCache<JsonSerializerOpts, JsonSerializerTuple> settingsMap)
        {
            SettingsMap = settingsMap ?? throw new ArgumentNullException(nameof(settingsMap));
        }

        public StaticDataCache<JsonSerializerOpts, JsonSerializerTuple> SettingsMap { get; }

        public string Serialize(
            object obj,
            JsonSerializerOpts opts,
            Formatting formatting = Formatting.Indented)
        {
            var settings = SettingsMap.Get(opts).SerializerSettings;

            string json = JsonConvert.SerializeObject(
                obj, formatting, settings);

            return json;
        }

        public string Serialize(
            object obj,
            bool useCamelCase = false,
            bool ignoreNullValues = true,
            Formatting formatting = Formatting.Indented,
            bool useStringEnumConverter = true)
        {
            var settings = SettingsMap.Get(
                new JsonSerializerOpts(
                    useCamelCase,
                    ignoreNullValues,
                    useStringEnumConverter)).SerializerSettings;

            string json = JsonConvert.SerializeObject(
                obj, formatting, settings);

            return json;
        }

        public object Deserialize(
            string json,
            JsonSerializerOpts opts)
        {
            var settings = SettingsMap.Get(opts).SerializerSettings;
            object data = JsonConvert.DeserializeObject(json, settings);

            return data;
        }

        public object Deserialize(
            string json,
            bool useCamelCase = false,
            bool useStringEnumConverter = true)
        {
            var settings = SettingsMap.Get(
                new JsonSerializerOpts(
                    useCamelCase,
                    true,
                    useStringEnumConverter)).SerializerSettings;

            object data = JsonConvert.DeserializeObject(json, settings);
            return data;
        }

        public TData Deserialize<TData>(
            string json,
            JsonSerializerOpts opts)
        {
            var settings = SettingsMap.Get(
                opts).SerializerSettings;

            TData data = JsonConvert.DeserializeObject<TData>(json, settings);

            return data;
        }

        public TData Deserialize<TData>(
            string json,
            bool useCamelCase = false,
            bool useStringEnumConverter = true)
        {
            var settings = SettingsMap.Get(
                new JsonSerializerOpts(
                    useCamelCase,
                    true,
                    useStringEnumConverter)).SerializerSettings;

            TData data = JsonConvert.DeserializeObject<TData>(json, settings);
            return data;
        }

        public JObject ToJObject(
            object obj,
            JsonSerializerOpts opts)
        {
            var serializer = SettingsMap.Get(opts).Serializer;

            JObject retObj = JObject.FromObject(
                obj, serializer);

            return retObj;
        }

        public JObject ToJObject(
            object obj,
            bool useCamelCase = false,
            bool useStringEnumConverter = true) => ToJObject(
                obj, new JsonSerializerOpts(
                    useCamelCase, true,
                    useStringEnumConverter));
    }
}
