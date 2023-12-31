using Serilog.Events;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public static class LogHelperMethods
    {
        public const string LOG_FILE_NAME_TIME_STAMP_TPL = "yyyyMMdd";
        public const string LOG_FILE_NAME_UNIQUIFIER_IDX_TPL = "D3";

        public static LogEventLevel ToLogEventLevel(
            this LogLevel logLevel) => (LogEventLevel)((int)logLevel);

        public static LogLevel ToLogLevel(
            this LogEventLevel logLevel) => (LogLevel)((int)logLevel);

        public static IAppLogger GetAppLogger(
            this IServiceProvider serviceProvider,
            Type loggerNameType,
            LogLevel logEventLevel = LogLevel.Information)
        {
            var appLoggerFactory = serviceProvider.GetRequiredService<IAppLoggerCreator>();
            var appLogger = appLoggerFactory.GetAppLogger(loggerNameType, logEventLevel);

            return appLogger;
        }

        public static IAppLogger GetSharedAppLogger(
            this IServiceProvider serviceProvider,
            Type loggerNameType,
            LogLevel logEventLevel = LogLevel.Information)
        {
            var appLoggerFactory = serviceProvider.GetRequiredService<IAppLoggerCreator>();
            var appLogger = appLoggerFactory.GetSharedAppLogger(loggerNameType, logEventLevel);

            return appLogger;
        }

        public static string GetLogFileName(
            this string logFileNameTemplate,
            DateTime? timeStamp = null,
            int? uniquifierIdx = null)
        {
            var timeStampVal = timeStamp ?? DateTime.UtcNow;
            string dateStr = timeStampVal.ToString(LOG_FILE_NAME_TIME_STAMP_TPL);

            string logFileName = Path.GetFileNameWithoutExtension(logFileNameTemplate);
            string logFileNameExtn = Path.GetExtension(logFileNameTemplate);
            logFileName = string.Concat(logFileName, dateStr);

            if (uniquifierIdx.HasValue)
            {
                string uniquifierIdxStr = uniquifierIdx.Value.ToString(
                    LOG_FILE_NAME_UNIQUIFIER_IDX_TPL);

                logFileName = $"{logFileName}-{uniquifierIdxStr}";
            }

            logFileName = $"{logFileName}{logFileNameExtn}";
            return logFileName;
        }

        public static string GetLogFilePath(
            this string logFilePathTemplate,
            DateTime? timeStamp = null,
            int? uniquifierIdx = null)
        {
            string logDirPath = Path.GetDirectoryName(logFilePathTemplate);
            string logFileNameTemplate = Path.GetFileName(logFilePathTemplate);

            string logFileName = logFileNameTemplate.GetLogFileName(
                timeStamp,
                uniquifierIdx);

            string logFilePath = Path.Combine(
                logDirPath,
                logFileName);

            return logFilePath;
        }
    }
}
