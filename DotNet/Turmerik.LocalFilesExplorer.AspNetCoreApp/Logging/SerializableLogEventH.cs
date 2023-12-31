namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public static class SerializableLogEventH
    {
        public static SerializableLogEventCoreDateTimeOffset.Mtbl ToEventCoreDateTimeOffset(
            this SerializableLogEventDateTimeOffset.IClnbl src) => new SerializableLogEventCoreDateTimeOffset.Mtbl
            {
                TimeStamp = src.TimeStamp,
                Level = src.Level.ToLogLevel(),
                Message = src.Message,
                Data = src.Data,
                Exception = src.GetException().AsMtbl(),
                Properties = src.GetProperties()?.ToList()
            };

        public static SerializableLogEventCoreDateTime.Mtbl ToEventCoreDateTime(
            this SerializableLogEventDateTime.IClnbl src) => new SerializableLogEventCoreDateTime.Mtbl
            {
                TimeStamp = src.TimeStamp,
                Level = src.Level.ToLogLevel(),
                Message = src.Message,
                Data = src.Data,
                Exception = src.GetException().AsMtbl(),
                Properties = src.GetProperties()?.ToList()
            };
        public static SerializableLogEventDateTimeOffset.Mtbl ToEventDateTimeOffset(
            this SerializableLogEventCoreDateTimeOffset.IClnbl src) => new SerializableLogEventDateTimeOffset.Mtbl
            {
                TimeStamp = src.TimeStamp,
                Level = src.Level.ToLogEventLevel(),
                Message = src.Message,
                Data = src.Data,
                Exception = src.GetException().AsMtbl(),
                Properties = src.GetProperties()?.ToList()
            };

        public static SerializableLogEventDateTime.Mtbl ToEventDateTime(
            this SerializableLogEventCoreDateTime.IClnbl src) => new SerializableLogEventDateTime.Mtbl
            {
                TimeStamp = src.TimeStamp,
                Level = src.Level.ToLogEventLevel(),
                Message = src.Message,
                Data = src.Data,
                Exception = src.GetException().AsMtbl(),
                Properties = src.GetProperties()?.ToList()
            };
    }
}
