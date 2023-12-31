using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IMutexAdapter : ISynchronizedAdapter
    {
    }

    public class MutexAdapter : IMutexAdapter
    {
        private readonly Mutex mutex;

        public MutexAdapter(Mutex mutex)
        {
            this.mutex = mutex ?? throw new ArgumentNullException(nameof(mutex));
        }

        public void Dispose()
        {
            mutex.Dispose();
        }

        public void Execute(
            Action action,
            Action<Exception> excHandler = null,
            bool rethrowExc = true)
        {
            mutex.WaitOne();

            try
            {
                action();
            }
            catch (Exception exc)
            {
                excHandler?.Invoke(exc);

                if (rethrowExc)
                {
                    throw;
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public T Get<T>(
            Func<T> action,
            Func<Exception, T> excHandler = null,
            bool rethrowExc = true)
        {
            T retVal;
            mutex.WaitOne();

            try
            {
                retVal = action();
            }
            catch (Exception exc)
            {
                if (excHandler != null)
                {
                    retVal = excHandler(exc);
                }
                else
                {
                    retVal = default;
                }

                if (rethrowExc)
                {
                    throw;
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return retVal;
        }
    }
}
