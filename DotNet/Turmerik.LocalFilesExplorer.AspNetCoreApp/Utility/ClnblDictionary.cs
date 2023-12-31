using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class ClnblDictionary<TKey, TValue, TImmtbl, TMtbl> : IReadOnlyDictionary<TKey, TValue>
        where TImmtbl : TValue
        where TMtbl : TValue
    {
        private readonly ReadOnlyDictionary<TKey, TImmtbl> immtblDictnr;
        private readonly Dictionary<TKey, TMtbl> mtblDictnr;

        public ClnblDictionary(
            ClnblDictionary<TKey, TValue, TImmtbl, TMtbl> src,
            bool createNewInstances = true)
        {
            IsReadOnly = src.IsReadOnly;

            if (src.IsReadOnly)
            {
                immtblDictnr = src.AsImmtblDictnr(
                    createNewInstances);
            }
            else
            {
                mtblDictnr = src.AsMtblDictnr(
                    createNewInstances);
            }
        }

        public ClnblDictionary(
            ReadOnlyDictionary<TKey, TImmtbl> immtblDictnr)
        {
            this.immtblDictnr = immtblDictnr ?? throw new ArgumentNullException(
                nameof(immtblDictnr));

            IsReadOnly = true;
        }

        public ClnblDictionary(
            Dictionary<TKey, TMtbl> mtblDictnr)
        {
            this.mtblDictnr = mtblDictnr ?? throw new ArgumentNullException(
                nameof(mtblDictnr));

            IsReadOnly = false;
        }

        public TValue this[TKey key] => GetValueCore<TValue>(
            dictnr => dictnr[key],
            dictnr => dictnr[key]);

        public IEnumerable<TKey> Keys => GetValueCore(
            dictnr => dictnr.Keys as IEnumerable<TKey>,
            dictnr => dictnr.Keys as IEnumerable<TKey>);

        public IEnumerable<TValue> Values => GetValueCore(
            dictnr => dictnr.Values.Select(
                value => value.SafeCast<TValue>()),
            dictnr => dictnr.Values.Select(
                value => value.SafeCast<TValue>()));

        public int Count => GetValueCore(
            dictnr => dictnr.Count,
            dictnr => dictnr.Count);

        public bool IsReadOnly { get; }

        public bool ContainsKey(TKey key) => GetValueCore(
            dictnr => dictnr.ContainsKey(key),
            dictnr => dictnr.ContainsKey(key));

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => GetValueCore(
            dictnr => dictnr.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SafeCast<TValue>()),
            dictnr => dictnr.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SafeCast<TValue>())).GetEnumerator();

        public bool TryGetValue(TKey key, out TValue value)
        {
            TValue retVal = default;

            bool foundMatching = GetValueCore(
                dictnr => dictnr.TryGetValue(key, out var val).ActWith(found => retVal = val),
                dictnr => dictnr.TryGetValue(key, out var val).ActWith(found => retVal = val));

            value = retVal;
            return foundMatching;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Dictionary<TKey, TMtbl> ToMtblDictnr() => GetValueCore(
            dictnr => dictnr.ToDictionary(kvp => kvp.Key, kvp => kvp.SafeCast<TMtbl>()),
            dictnr => dictnr.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

        public Dictionary<TKey, TMtbl> AsMtblDictnr(
            bool createNewInstance = true) => GetValueCore(
            dictnr => ToMtblDictnr(),
            dictnr => createNewInstance ? ToMtblDictnr() : dictnr);

        public ReadOnlyDictionary<TKey, TImmtbl> ToImmtblDictnr() => GetValueCore(
            dictnr => dictnr.ToDictionary(kvp => kvp.Key, kvp => kvp.Value).RdnlD(),
            dictnr => dictnr.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.SafeCast<TImmtbl>()).RdnlD());

        public ReadOnlyDictionary<TKey, TImmtbl> AsImmtblDictnr(
            bool createNewInstance = true) => GetValueCore(
            dictnr => createNewInstance ? ToImmtblDictnr() : dictnr,
            dictrn => ToImmtblDictnr());

        public ClnblDictionary<TKey, TValue, TImmtbl, TMtbl> Clone(
            bool createNewInstances = true) => new ClnblDictionary<TKey, TValue, TImmtbl, TMtbl>(
                this, createNewInstances);

        private TRetVal GetValueCore<TRetVal>(
            Func<ReadOnlyDictionary<TKey, TImmtbl>, TRetVal> immtblFactory,
            Func<Dictionary<TKey, TMtbl>, TRetVal> mtblFactory)
        {
            TRetVal retVal;

            if (immtblDictnr != null)
            {
                retVal = immtblFactory(immtblDictnr);
            }
            else if (mtblDictnr != null)
            {
                retVal = mtblFactory(mtblDictnr);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return retVal;
        }
    }
}
