using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface ISemaphoreSlimAdapter : ISynchronizedAdapter
    {
        Task ExecuteAsync(
            Func<Task> action,
            Action<Exception> excHandler = null,
            bool rethrowExc = true);

        Task<T> GetAsync<T>(
            Func<Task<T>> action,
            Func<Exception, T> excHandler = null,
            bool rethrowExc = true);
    }

    public class SemaphoreSlimAdapter : ISemaphoreSlimAdapter
    {
        private readonly SemaphoreSlim sempahore;

        public SemaphoreSlimAdapter(SemaphoreSlim sempahore)
        {
            this.sempahore = sempahore ?? throw new ArgumentNullException(nameof(sempahore));
        }

        public void Dispose()
        {
            sempahore.Dispose();
        }

        public void Execute(
            Action action,
            Action<Exception> excHandler = null,
            bool rethrowExc = true)
        {
            sempahore.Wait();

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
                sempahore.Release();
            }
        }

        public T Get<T>(
            Func<T> action,
            Func<Exception, T> excHandler = null,
            bool rethrowExc = true)
        {
            T retVal;
            sempahore.Wait();

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
                sempahore.Release();
            }

            return retVal;
        }

        public async Task ExecuteAsync(
            Func<Task> action,
            Action<Exception> excHandler = null,
            bool rethrowExc = true)
        {
            sempahore.Wait();

            try
            {
                await action();
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
                sempahore.Release();
            }
        }

        public async Task<T> GetAsync<T>(
            Func<Task<T>> action,
            Func<Exception, T> excHandler = null,
            bool rethrowExc = true)
        {
            T retVal;
            sempahore.Wait();

            try
            {
                retVal = await action();
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
                sempahore.Release();
            }

            return retVal;
        }
    }
}
