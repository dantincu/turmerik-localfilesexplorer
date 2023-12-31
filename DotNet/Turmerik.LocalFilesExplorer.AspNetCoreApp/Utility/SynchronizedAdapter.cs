using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface ISynchronizedAdapter : IDisposable
    {
        void Execute(
            Action action,
            Action<Exception> excHandler = null,
            bool rethrowExc = true);

        T Get<T>(
            Func<T> action,
            Func<Exception, T> excHandler = null,
            bool rethrowExc = true);
    }
}
