using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public interface IAppLoggerCreator
    {
        bool UseAppProcessIdnfByDefault { get; }
        LogLevel MinLogLevel { get; set; }

        void AssureAppProcessIdnfDumped();
        void DumpAppProcessIdnf();

        IAppLogger GetAppLogger(
            string loggerRelPath,
            LogLevel? logEventLevel = null,
            bool? useAppProcessIdnf = null);

        IAppLogger GetAppLogger(
            Type loggerNameType,
            LogLevel? logEventLevel = null,
            bool? useAppProcessIdnf = null);

        IAppLogger GetSharedAppLogger(
            string loggerRelPath,
            LogLevel? logEventLevel = null,
            bool? useAppProcessIdnf = null);

        IAppLogger GetSharedAppLogger(
            Type loggerNameType,
            LogLevel? logEventLevel = null,
            bool? useAppProcessIdnf = null);

        IAppLogger GetBufferedAppLogger(
            string loggerRelPath,
            out int bufferedLoggerDirNameIdx,
            LogLevel? logEventLevel = null,
            bool? useAppProcessIdnf = null);

        IAppLogger GetBufferedAppLogger(
            Type loggerNameType,
            out int bufferedLoggerDirNameIdx,
            LogLevel? logEventLevel = null,
            bool? useAppProcessIdnf = null);
    }
}
