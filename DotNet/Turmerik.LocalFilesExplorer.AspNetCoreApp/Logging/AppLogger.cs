using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using System.Globalization;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public partial class AppLogger : IAppLogger
    {
        private const int FILE_SIZE_LIMIT_BYTES = 1024 * 1024;
        private const string LOG_FILE_NAME_PARTS_JOIN_STR = "-";
        private const string CORE_LOG_FILE_NAME = "log";
        private const string BUFFERED_LOG_FILE_NAME_PFX = "buffered";
        private const string SHARED_LOG_FILE_NAME_PFX = "shared";
        private const string LOG_FILE_NAME_EXT = ".json";
        private const string APP_PROCESS_DIR_NAME_PFX = "[[]]";

        public AppLogger(AppLoggerOpts.IClnbl opts)
        {
            AppEnv = opts.AppEnv;
            AppInstanceStartInfoProvider = opts.AppInstanceStartInfoProvider;
            TextFormatter = opts.TextFormatter;
            StringTemplateParser = opts.StringTemplateParser;
            LogEventLevel = opts.LogLevel.ToLogEventLevel();
            IsLoggerBuffered = opts.IsLoggerBuffered;
            IsLoggerShared = opts.IsLoggerShared;

            LogDirRelPath = GetLogDirRelPath(
                opts.LogDirRelPath);

            LogFileName = GetLogFileName();
            LogFilePath = GetLogFilePath();

            Logger = GetLogger();
        }

        public string LogDirRelPath { get; private set; }
        public string LogFileName { get; private set; }
        public string LogFilePath { get; private set; }

        private Serilog.ILogger Logger { get; }
        private IAppEnv AppEnv { get; }
        private IAppInstanceStartInfoProvider AppInstanceStartInfoProvider { get; }
        private ITextFormatter TextFormatter { get; }
        private IStringTemplateParser StringTemplateParser { get; }
        private LogEventLevel LogEventLevel { get; }
        private bool IsLoggerBuffered { get; }
        private bool IsLoggerShared { get; }
        private TimeSpan? FlushToDiskInterval => null;

        private string GetLogFileName()
        {
            var partsList = new List<string>
            {
                CORE_LOG_FILE_NAME,
                LOG_FILE_NAME_EXT
            };

            if (IsLoggerBuffered)
            {
                partsList.Insert(0, BUFFERED_LOG_FILE_NAME_PFX);
            }

            if (IsLoggerShared)
            {
                partsList.Insert(0, SHARED_LOG_FILE_NAME_PFX);
            }

            string logFile = string.Join(
                LOG_FILE_NAME_PARTS_JOIN_STR,
                partsList.ToArray());

            return logFile;
        }

        private string GetLogDirRelPath(
            string logDirRelPath)
        {
            string appProcessDirName = GetAppProcessDirName() ?? string.Empty;

            logDirRelPath = Path.Combine(
                appProcessDirName,
                logDirRelPath);

            return logDirRelPath;
        }

        private string? GetAppProcessDirName()
        {
            string? appProcessDirName = AppInstanceStartInfoProvider?.ProcessDirName;

            if (appProcessDirName != null)
            {
                appProcessDirName = Path.Combine(
                    APP_PROCESS_DIR_NAME_PFX,
                    appProcessDirName);
            }

            return appProcessDirName;
        }

        private int GetFileSizeLimitBytes()
        {
            return FILE_SIZE_LIMIT_BYTES;
        }

        private FileLifecycleHooks GetFileLifecycleHooks()
        {
            return null;
        }

        private Serilog.ILogger GetLogger()
        {
            var logger = GetLoggerConfiguration().CreateLogger();
            return logger;
        }

        private string GetLogFilePath() => AppEnv.GetPath(
            AppEnvDir.Logs,
            LogDirRelPath,
            LogFileName);

        private LoggerConfiguration GetLoggerConfiguration()
        {
            var loggerConfiguration = new LoggerConfiguration();
            LoggerSinkConfiguration loggerSinkConfiguration = GetLoggerSinkConfiguration(loggerConfiguration);

            loggerConfiguration = GetFileLoggerConfiguration(loggerSinkConfiguration);
            return loggerConfiguration;
        }

        private LoggerSinkConfiguration GetLoggerSinkConfiguration(LoggerConfiguration loggerConfiguration)
        {
            LoggerSinkConfiguration loggerSinkConfiguration = loggerConfiguration.MinimumLevel.Verbose().WriteTo;
            return loggerSinkConfiguration;
        }

        private LoggerConfiguration GetFileLoggerConfiguration(
            LoggerSinkConfiguration loggerSinkConfiguration)
        {
            /*
             * string path,
             * LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose,
             * string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
             * IFormatProvider formatProvider = null,
             * long? fileSizeLimitBytes = 1073741824,
             * LoggingLevelSwitch levelSwitch = null,
             * bool buffered = false,
             * bool shared = false,
             * TimeSpan? flushToDiskInterval = null,
             * RollingInterval rollingInterval = RollingInterval.Infinite,
             * bool rollOnFileSizeLimit = false,
             * int? retainedFileCountLimit = 31,
             * Encoding encoding = null, FileLifecycleHooks hooks = null
             */

            LoggerConfiguration loggerConfiguration = loggerSinkConfiguration.File(
                GetTextFormatter(),
                LogFilePath,
                restrictedToMinimumLevel: LogEventLevel,
                fileSizeLimitBytes: GetFileSizeLimitBytes(),
                levelSwitch: new LoggingLevelSwitch(LogEventLevel),
                buffered: IsLoggerBuffered,
                shared: IsLoggerShared,
                flushToDiskInterval: FlushToDiskInterval,
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: null,
                encoding: null,
                hooks: GetFileLifecycleHooks());

            return loggerConfiguration;
        }

        private ITextFormatter GetTextFormatter() => TextFormatter ?? GetJsonFormatter();

        private ITextFormatter GetJsonFormatter()
        {
            ITextFormatter formatter = new JsonFormatter(
                closingDelimiter: $",{Environment.NewLine}",
                renderMessage: true,
                formatProvider: CultureInfo.InvariantCulture);

            return formatter;
        }
    }
}