using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class StaticDataCache<TKey, TValue>
    {
        private ConcurrentDictionary<TKey, TValue> map;
        private Func<TKey, TValue> factory;

        public StaticDataCache(
            Func<TKey, TValue> factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            map = new ConcurrentDictionary<TKey, TValue>();
        }

        public TValue Get(
            TKey key) => map.GetOrAdd(
                key, factory);

        public void Clear() => map.Clear();

        public bool TryRemove(
            TKey key,
            out TValue value) => map.TryRemove(
                key,
                out value);

        public KeyValuePair<TKey, TValue>[] ToArray(
            ) => map.ToArray();
    }
}
