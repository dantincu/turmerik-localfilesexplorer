using Newtonsoft.Json;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public readonly struct JsonSerializerTuple
    {
        public JsonSerializerTuple(
            JsonSerializer serializer,
            JsonSerializerSettings serializerSettings)
        {
            Serializer = serializer;
            SerializerSettings = serializerSettings;
        }

        public JsonSerializer Serializer { get; }
        public JsonSerializerSettings SerializerSettings { get; }
    }
}
