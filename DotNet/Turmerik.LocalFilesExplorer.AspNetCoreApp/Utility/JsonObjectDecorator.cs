using Newtonsoft.Json.Linq;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IJsonObjectDecorator<T>
    {
        T Data { get; }

        T ShallowMergeWith(T newData);

        string Serialize(
            bool refresh = false);
    }

    public class JsonObjectDecorator<T> : IJsonObjectDecorator<T>
    {
        private readonly JsonSerializerOpts jsonSerializerOpts;

        public JsonObjectDecorator(
            IJsonConversionAdapter adapter,
            string rawJson,
            JsonSerializerOpts jsonSerializerOpts)
        {
            Adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            RawObj = Adapter.Deserialize(rawJson, jsonSerializerOpts) as JObject;
            Data = Deserialize(RawObj);
            this.jsonSerializerOpts = jsonSerializerOpts;
        }

        public JsonObjectDecorator(
            IJsonConversionAdapter adapter,
            JObject rawObj,
            JsonSerializerOpts jsonSerializerOpts)
        {
            Adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            RawObj = rawObj ?? throw new ArgumentNullException(nameof(rawObj));
            Data = Deserialize(rawObj);
            this.jsonSerializerOpts = jsonSerializerOpts;
        }

        public JsonObjectDecorator(
            IJsonConversionAdapter adapter,
            T data,
            JsonSerializerOpts jsonSerializerOpts)
        {
            Adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            RawObj = Adapter.ToJObject(data, jsonSerializerOpts);
            Data = data;
            this.jsonSerializerOpts = jsonSerializerOpts;
        }

        public T Data { get; private set; }

        protected IJsonConversionAdapter Adapter { get; }
        protected JObject RawObj { get; }

        public T ShallowMergeWith(T newData)
        {
            SerializeCore(newData);
            return Data;
        }

        public string Serialize(
            bool refresh = false)
        {
            if (refresh)
            {
                SerializeCore(Data);
            }

            string json = RawObj.ToString();
            return json;
        }

        private T Deserialize(
            JObject rawObj) => rawObj.ToObject<T>();

        private void SerializeCore(T data)
        {
            JObject newObj = Adapter.ToJObject(
                data, jsonSerializerOpts);

            foreach (var kvp in newObj)
            {
                RawObj[kvp.Key] = kvp.Value;
            }
        }
    }
}
