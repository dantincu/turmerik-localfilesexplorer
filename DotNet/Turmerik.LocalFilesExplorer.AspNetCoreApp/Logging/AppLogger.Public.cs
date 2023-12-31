using Serilog.Events;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public partial class AppLogger
    {
        public void Write(LogLevel logLevel, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(logLevel.ToLogEventLevel(), messageTemplate, propertyValues);
        }

        public void Write(LogLevel logLevel, Exception ex, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(logLevel.ToLogEventLevel(), ex, messageTemplate, propertyValues);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Trace, messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Trace, exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Debug, messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Debug, exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Information, messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Information, exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Warning, messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Warning, exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Error, messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Error, exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Critical, messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogLevel.Critical, exception, messageTemplate, propertyValues);
        }

        public void WriteData(LogLevel logLevel, object data, string messageTemplate, params object[] propertyValues)
        {
            Write(GetLogEvent(logLevel, data, null, messageTemplate, propertyValues));
        }

        public void WriteData(LogLevel logLevel, object data, Exception ex, string messageTemplate, params object[] propertyValues)
        {
            Write(GetLogEvent(logLevel, data, ex, messageTemplate, propertyValues));
        }

        public void VerboseData(object data, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Trace, data, messageTemplate, propertyValues);
        }

        public void VerboseData(object data, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Trace, data, exception, messageTemplate, propertyValues);
        }

        public void DebugData(object data, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Debug, data, messageTemplate, propertyValues);
        }

        public void DebugData(object data, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Debug, data, exception, messageTemplate, propertyValues);
        }

        public void InformationData(object data, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Information, data, messageTemplate, propertyValues);
        }

        public void InformationData(object data, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Information, data, exception, messageTemplate, propertyValues);
        }

        public void WarningData(object data, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Warning, data, messageTemplate, propertyValues);
        }

        public void WarningData(object data, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Warning, data, exception, messageTemplate, propertyValues);
        }

        public void ErrorData(object data, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Error, data, messageTemplate, propertyValues);
        }

        public void ErrorData(object data, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Error, data, exception, messageTemplate, propertyValues);
        }

        public void FatalData(object data, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Critical, data, messageTemplate, propertyValues);
        }

        public void FatalData(object data, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            WriteData(LogLevel.Critical, data, exception, messageTemplate, propertyValues);
        }

        public void Write(LogEvent logEvent)
        {
            Logger.Write(logEvent);
        }

        public void Dispose()
        {
            IDisposable disposable = Logger as IDisposable;
            disposable?.Dispose();
        }

        private TrmrkLogEvent GetLogEvent(
            LogLevel logLevel,
            object data,
            Exception exc,
            string messageTemplate,
            params object[] propertyValues)
        {
            var msgTemplateTokens = StringTemplateParser.Parse(messageTemplate);

            var msgTemplateTokensArr = msgTemplateTokens.Select(
                kvp => new TrmrkMessageTemplateToken(kvp.Value)).ToArray();

            var msgTemplateObj = new MessageTemplate(msgTemplateTokensArr);

            var propValuesArr = msgTemplateTokens.Where(
                kvp => kvp.Value is StringTemplateToken).Select(
                (kvp, idx) => new LogEventProperty(
                    idx.ToString(),
                    GetLogEvenPropertyValue(
                        kvp.Value,
                        propertyValues))).ToArray();

            var trmrkLogEvent = new TrmrkLogEvent(
                DateTimeOffset.UtcNow,
                logLevel.ToLogEventLevel(),
                exc,
                msgTemplateObj,
                propValuesArr,
                data);

            return trmrkLogEvent;
        }

        private TrmrkLogEventPropertyValue GetLogEvenPropertyValue(
            IStringTemplateToken msgTemplateToken,
            object[] propertyValues)
        {
            object propVal = GetPropertyValue(
                msgTemplateToken,
                propertyValues);

            var logEventPropVal = new TrmrkLogEventPropertyValue(
                msgTemplateToken, propVal);

            return logEventPropVal;
        }

        private object GetPropertyValue(
            IStringTemplateToken msgTemplateToken,
            object[] propertyValues)
        {
            object propVal = null;

            if (msgTemplateToken is StringTemplateToken templateToken)
            {
                propVal = propertyValues[templateToken.Idx];
            }

            return propVal;
        }
    }
}
