using Serilog.Events;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public class TrmrkLogEvent : LogEvent
    {
        public TrmrkLogEvent(
            DateTimeOffset timestamp,
            LogEventLevel level,
            Exception exception,
            MessageTemplate messageTemplate,
            IEnumerable<LogEventProperty> properties,
            object data = null) : base(
                timestamp,
                level,
                exception,
                messageTemplate,
                properties)
        {
            Data = data;
        }

        public object Data { get; }
    }
}
