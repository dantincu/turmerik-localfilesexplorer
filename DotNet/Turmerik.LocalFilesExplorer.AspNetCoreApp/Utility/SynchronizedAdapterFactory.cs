using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface ISynchronizedAdapterFactory
    {
        IMutexAdapter Mutex(Mutex mutex);

        ISemaphoreSlimAdapter SempahoreSlim(
            SemaphoreSlim semaphoreSlim);
    }

    public class SynchronizedAdapterFactory : ISynchronizedAdapterFactory
    {
        public IMutexAdapter Mutex(
            Mutex mutex) => new MutexAdapter(mutex);

        public ISemaphoreSlimAdapter SempahoreSlim(
            SemaphoreSlim semaphoreSlim) => new SemaphoreSlimAdapter(semaphoreSlim);
    }
}
