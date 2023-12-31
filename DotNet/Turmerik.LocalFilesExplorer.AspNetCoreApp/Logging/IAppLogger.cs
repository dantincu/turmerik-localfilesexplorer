using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public interface IAppLogger : IDisposable
    {
        string LogDirRelPath { get; }
        string LogFileName { get; }
        string LogFilePath { get; }

        void Write(LogLevel logLevel, string messageTemplate, params object[] propertyValues);
        void Write(LogLevel logLevel, Exception ex, string messageTemplate, params object[] propertyValues);
        void Verbose(string messageTemplate, params object[] propertyValues);
        void Verbose(Exception exception, string messageTemplate, params object[] propertyValues);
        void Debug(string messageTemplate, params object[] propertyValues);
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);
        void Information(string messageTemplate, params object[] propertyValues);
        void Information(Exception exception, string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate, params object[] propertyValues);
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);
        void Error(string messageTemplate, params object[] propertyValues);
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);
        void Fatal(string messageTemplate, params object[] propertyValues);
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);

        void WriteData(LogLevel logLevel, object data, string messageTemplate, params object[] propertyValues);
        void WriteData(LogLevel logLevel, object data, Exception ex, string messageTemplate, params object[] propertyValues);
        void VerboseData(object data, string messageTemplate, params object[] propertyValues);
        void VerboseData(object data, Exception exception, string messageTemplate, params object[] propertyValues);
        void DebugData(object data, string messageTemplate, params object[] propertyValues);
        void DebugData(object data, Exception exception, string messageTemplate, params object[] propertyValues);
        void InformationData(object data, string messageTemplate, params object[] propertyValues);
        void InformationData(object data, Exception exception, string messageTemplate, params object[] propertyValues);
        void WarningData(object data, string messageTemplate, params object[] propertyValues);
        void WarningData(object data, Exception exception, string messageTemplate, params object[] propertyValues);
        void ErrorData(object data, string messageTemplate, params object[] propertyValues);
        void ErrorData(object data, Exception exception, string messageTemplate, params object[] propertyValues);
        void FatalData(object data, string messageTemplate, params object[] propertyValues);
        void FatalData(object data, Exception exception, string messageTemplate, params object[] propertyValues);
    }
}
