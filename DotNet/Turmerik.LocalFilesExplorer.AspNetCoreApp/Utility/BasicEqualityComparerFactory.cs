using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IBasicEqualityComparerFactory
    {
        SimpleEqualityComparer<T> GetEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null);

        SequenceBasicEqualityComparer<TSeq, T> GetSequenceEqualityComparer<TSeq, T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<TSeq, int> seqHashCodeFunc = null)
            where TSeq : IEnumerable<T>;

        NmrblBasicEqualityComparer<T> GetNmrblBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        ListBasicEqualityComparerCore<T> GetListBasicEqualityComparerCore<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        ListBasicEqualityComparer<T> GetListBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        ArrayBasicEqualityComparer<T> GetArrayBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        CollectionBasicEqualityComparerCore<T> GetCollectionBasicEqualityComparerCore<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        CollectionBasicEqualityComparer<T> GetCollectionBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        ReadOnlyCollectionBasicEqualityComparerCore<T> GetReadOnlyCollectionBasicEqualityComparerCore<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        ReadOnlyCollectionBasicEqualityComparer<T> GetReadOnlyCollectionBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null);

        DictionaryBasicEqualityComparer<TDictnr, TKey, TValue> GetDictionaryBasicEqualityComparer<TDictnr, TKey, TValue>(
            Func<TDictnr, IEnumerable<TKey>> keysFactory,
            TryRetrieve2In1Out<TDictnr, TKey, TValue> valueRetriever,
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null)
            where TDictnr : IEnumerable<KeyValuePair<TKey, TValue>>;

        DictionaryBasicEqualityComparerCore<TKey, TValue> GetDictionaryBasicEqualityComparerCore<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null);

        DictionaryBasicEqualityComparer<TKey, TValue> GetDictionaryBasicEqualityComparer<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null);

        ReadOnlyDictionaryBasicEqualityComparerCore<TKey, TValue> GetReadOnlyDictionaryBasicEqualityComparerCore<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null);

        ReadOnlyDictionaryBasicEqualityComparer<TKey, TValue> GetReadOnlyDictionaryBasicEqualityComparer<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null);

        ConcurrentDictionaryBasicEqualityComparer<TKey, TValue> GetConcurrentDictionaryBasicEqualityComparer<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null);
    }

    public class BasicEqualityComparerFactory : IBasicEqualityComparerFactory
    {
        public SimpleEqualityComparer<T> GetEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null)
        {
            var comparer = new SimpleEqualityComparer<T>(
                equalsFunc, valuesCanBeNull, hashCodeFunc);

            return comparer;
        }

        public SequenceBasicEqualityComparer<TSeq, T> GetSequenceEqualityComparer<TSeq, T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<TSeq, int> seqHashCodeFunc = null)
            where TSeq : IEnumerable<T> => new SequenceBasicEqualityComparer<TSeq, T>(
                GetSeqEqualityComparerPredicate<TSeq, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    checkCountFirst,
                    hashCodeFunc),
                seqHashCodeFunc);

        public NmrblBasicEqualityComparer<T> GetNmrblBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new NmrblBasicEqualityComparer<T>(
                GetSeqEqualityComparerPredicate<IEnumerable<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    checkCountFirst,
                    hashCodeFunc),
                seqHashCodeFunc);

        public ListBasicEqualityComparerCore<T> GetListBasicEqualityComparerCore<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new ListBasicEqualityComparerCore<T>(
                GetSeqEqualityComparerPredicate<IList<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    checkCountFirst,
                    hashCodeFunc),
                seqHashCodeFunc);

        public ListBasicEqualityComparer<T> GetListBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new ListBasicEqualityComparer<T>(
                GetSeqEqualityComparerPredicate<List<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    true,
                    hashCodeFunc),
                seqHashCodeFunc);

        public ArrayBasicEqualityComparer<T> GetArrayBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new ArrayBasicEqualityComparer<T>(
                GetSeqEqualityComparerPredicate<T[], T>(
                    equalsFunc,
                    valuesCanBeNull,
                    true,
                    hashCodeFunc),
                seqHashCodeFunc);

        public CollectionBasicEqualityComparerCore<T> GetCollectionBasicEqualityComparerCore<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new CollectionBasicEqualityComparerCore<T>(
                GetSeqEqualityComparerPredicate<ICollection<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    checkCountFirst,
                    hashCodeFunc),
                seqHashCodeFunc);

        public CollectionBasicEqualityComparer<T> GetCollectionBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new CollectionBasicEqualityComparer<T>(
                GetSeqEqualityComparerPredicate<Collection<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    true,
                    hashCodeFunc),
                seqHashCodeFunc);

        public ReadOnlyCollectionBasicEqualityComparerCore<T> GetReadOnlyCollectionBasicEqualityComparerCore<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new ReadOnlyCollectionBasicEqualityComparerCore<T>(
                GetSeqEqualityComparerPredicate<IReadOnlyCollection<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    checkCountFirst,
                    hashCodeFunc),
                seqHashCodeFunc);

        public ReadOnlyCollectionBasicEqualityComparer<T> GetReadOnlyCollectionBasicEqualityComparer<T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null,
            Func<IEnumerable<T>, int> seqHashCodeFunc = null) => new ReadOnlyCollectionBasicEqualityComparer<T>(
                GetSeqEqualityComparerPredicate<ReadOnlyCollection<T>, T>(
                    equalsFunc,
                    valuesCanBeNull,
                    true,
                    hashCodeFunc),
                seqHashCodeFunc);

        public DictionaryBasicEqualityComparer<TDictnr, TKey, TValue> GetDictionaryBasicEqualityComparer<TDictnr, TKey, TValue>(
            Func<TDictnr, IEnumerable<TKey>> keysFactory,
            TryRetrieve2In1Out<TDictnr, TKey, TValue> valueRetriever,
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null)
            where TDictnr : IEnumerable<KeyValuePair<TKey, TValue>> => new DictionaryBasicEqualityComparer<TDictnr, TKey, TValue>(
                GetDictnrEqualityComparerPredicate(
                    keysFactory,
                    valueRetriever,
                    valueEqualsFunc,
                    keyEqualsFunc,
                    valuesCanBeNull,
                    keyHashCodeFunc,
                    valueHashCodeFunc));

        public DictionaryBasicEqualityComparerCore<TKey, TValue> GetDictionaryBasicEqualityComparerCore<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null) => new DictionaryBasicEqualityComparerCore<TKey, TValue>(
                GetDictnrEqualityComparerPredicate(
                    map => map.Keys,
                    delegate (
                        IDictionary<TKey, TValue> map,
                        TKey key,
                        out TValue value)
                        {
                            bool retVal = map.TryGetValue(key, out var val);
                            value = val;

                            return retVal;
                        },
                    valueEqualsFunc,
                    keyEqualsFunc,
                    valuesCanBeNull,
                    keyHashCodeFunc,
                    valueHashCodeFunc));

        public DictionaryBasicEqualityComparer<TKey, TValue> GetDictionaryBasicEqualityComparer<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null) => new DictionaryBasicEqualityComparer<TKey, TValue>(
                GetDictnrEqualityComparerPredicate(
                    map => map.Keys,
                    delegate (
                        Dictionary<TKey, TValue> map,
                        TKey key,
                        out TValue value)
                    {
                        bool retVal = map.TryGetValue(key, out var val);
                        value = val;

                        return retVal;
                    },
                    valueEqualsFunc,
                    keyEqualsFunc,
                    valuesCanBeNull,
                    keyHashCodeFunc,
                    valueHashCodeFunc));

        public ReadOnlyDictionaryBasicEqualityComparerCore<TKey, TValue> GetReadOnlyDictionaryBasicEqualityComparerCore<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null) => new ReadOnlyDictionaryBasicEqualityComparerCore<TKey, TValue>(
                GetDictnrEqualityComparerPredicate(
                    map => map.Keys,
                    delegate (
                        IReadOnlyDictionary<TKey, TValue> map,
                        TKey key,
                        out TValue value)
                    {
                        bool retVal = map.TryGetValue(key, out var val);
                        value = val;

                        return retVal;
                    },
                    valueEqualsFunc,
                    keyEqualsFunc,
                    valuesCanBeNull,
                    keyHashCodeFunc,
                    valueHashCodeFunc));

        public ReadOnlyDictionaryBasicEqualityComparer<TKey, TValue> GetReadOnlyDictionaryBasicEqualityComparer<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null) => new ReadOnlyDictionaryBasicEqualityComparer<TKey, TValue>(
                GetDictnrEqualityComparerPredicate(
                    map => map.Keys,
                    delegate (
                        ReadOnlyDictionary<TKey, TValue> map,
                        TKey key,
                        out TValue value)
                    {
                        bool retVal = map.TryGetValue(key, out var val);
                        value = val;

                        return retVal;
                    },
                    valueEqualsFunc,
                    keyEqualsFunc,
                    valuesCanBeNull,
                    keyHashCodeFunc,
                    valueHashCodeFunc));

        public ConcurrentDictionaryBasicEqualityComparer<TKey, TValue> GetConcurrentDictionaryBasicEqualityComparer<TKey, TValue>(
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null) => new ConcurrentDictionaryBasicEqualityComparer<TKey, TValue>(
                GetDictnrEqualityComparerPredicate(
                    map => map.Keys,
                    delegate (
                        ConcurrentDictionary<TKey, TValue> map,
                        TKey key,
                        out TValue value)
                    {
                        bool retVal = map.TryGetValue(key, out var val);
                        value = val;

                        return retVal;
                    },
                    valueEqualsFunc,
                    keyEqualsFunc,
                    valuesCanBeNull,
                    keyHashCodeFunc,
                    valueHashCodeFunc));

        private Func<TSeq, TSeq, bool> GetSeqEqualityComparerPredicate<TSeq, T>(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            bool checkCountFirst = true,
            Func<T, int> hashCodeFunc = null)
            where TSeq : IEnumerable<T> => GetEqualityComparer(
                equalsFunc,
                valuesCanBeNull,
                hashCodeFunc).With<SimpleEqualityComparer<T>, Func<TSeq, TSeq, bool>>(
                    itemsComparer => (seq1, seq2) =>
                    {
                        bool retVal = !checkCountFirst || seq1.Count() == seq2.Count();

                        if (retVal)
                        {
                            var nmrtr1 = seq1.GetEnumerator();
                            var nmrtr2 = seq2.GetEnumerator();

                            while (nmrtr1.MoveNext())
                            {
                                retVal = nmrtr2.MoveNext();

                                retVal = retVal && itemsComparer.Equals(
                                    nmrtr1.Current,
                                    nmrtr2.Current);

                                if (!retVal)
                                {
                                    break;
                                }
                            }

                            retVal = !nmrtr2.MoveNext();
                        }

                        return retVal;
                    });

        private Func<TDictnr, TDictnr, bool> GetDictnrEqualityComparerPredicate<TDictnr, TKey, TValue>(
            Func<TDictnr, IEnumerable<TKey>> keysFactory,
            TryRetrieve2In1Out<TDictnr, TKey, TValue> valueRetriever,
            Func<TValue, TValue, bool> valueEqualsFunc = null,
            Func<TKey, TKey, bool> keyEqualsFunc = null,
            bool valuesCanBeNull = true,
            Func<TKey, int> keyHashCodeFunc = null,
            Func<TValue, int> valueHashCodeFunc = null)
            where TDictnr : IEnumerable<KeyValuePair<TKey, TValue>>
        {
            var keyEqCompr = GetEqualityComparer(
                keyEqualsFunc,
                false,
                keyHashCodeFunc);

            var keysNmrblEqCompr = GetNmrblBasicEqualityComparer(
                keyEqualsFunc,
                true);

            var valueEqCompr = GetEqualityComparer(
                valueEqualsFunc,
                valuesCanBeNull,
                valueHashCodeFunc);

            Func<TDictnr, TDictnr, bool> predicate = (map1, map2) =>
            {
                var keys1 = keysFactory(map1);
                var keys2 = keysFactory(map2);

                bool retVal = keysNmrblEqCompr.Equals(keys1, keys2);

                if (retVal)
                {
                    foreach (var key in keys1)
                    {
                        retVal = valueRetriever(
                            map1,
                            key,
                            out var value1) && valueRetriever(
                                map2,
                                key,
                                out var value2) && valueEqCompr.Equals(value1, value2);

                        if (!retVal)
                        {
                            break;
                        }
                    }
                }

                return retVal;
            };

            return predicate;
        }
    }

    public class SimpleEqualityComparer<T> : EqualityComparer<T>
    {
        private readonly Func<T, T, bool> equalsFunc;
        private readonly bool valuesCanBeNull;
        private readonly Func<T, int> hashCodeFunc;

        public SimpleEqualityComparer(
            Func<T, T, bool> equalsFunc = null,
            bool valuesCanBeNull = true,
            Func<T, int> hashCodeFunc = null)
        {
            this.equalsFunc = equalsFunc.FirstNotNull((a, b) => a?.Equals(b) ?? b == null);
            this.valuesCanBeNull = valuesCanBeNull;
            this.hashCodeFunc = hashCodeFunc?.FirstNotNull(val => val?.GetHashCode() ?? 0);
        }

        public override bool Equals(T x, T y)
        {
            bool retVal;

            if (valuesCanBeNull)
            {
                if (x != null && y != null)
                {
                    retVal = equalsFunc(x, y);
                }
                else
                {
                    retVal = x == null && y == null;
                }
            }
            else
            {
                retVal = equalsFunc(x, y);
            }

            return retVal;
        }

        public override int GetHashCode(T obj)
        {
            int hashCode;

            if (valuesCanBeNull)
            {
                if (obj != null)
                {
                    hashCode = hashCodeFunc(obj);
                }
                else
                {
                    hashCode = 0;
                }
            }
            else
            {
                hashCode = hashCodeFunc(obj);
            }

            return hashCode;
        }
    }

    public class SequenceBasicEqualityComparer<TSeq, T> : SimpleEqualityComparer<TSeq>
        where TSeq : IEnumerable<T>
    {
        public SequenceBasicEqualityComparer(
            Func<TSeq, TSeq, bool> equalsFunc = null,
            Func<TSeq, int> hashCodeFunc = null) : base(
                equalsFunc,
                false,
                hashCodeFunc)
        {
        }
    }

    public class NmrblBasicEqualityComparer<T> : SequenceBasicEqualityComparer<IEnumerable<T>, T>
    {
        public NmrblBasicEqualityComparer(
            Func<IEnumerable<T>, IEnumerable<T>, bool> equalsFunc = null,
            Func<IEnumerable<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ListBasicEqualityComparerCore<T> : SequenceBasicEqualityComparer<IList<T>, T>
    {
        public ListBasicEqualityComparerCore(
            Func<IList<T>, IList<T>, bool> equalsFunc = null,
            Func<IList<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ListBasicEqualityComparer<T> : SequenceBasicEqualityComparer<List<T>, T>
    {
        public ListBasicEqualityComparer(
            Func<List<T>, List<T>, bool> equalsFunc = null,
            Func<List<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ArrayBasicEqualityComparer<T> : SequenceBasicEqualityComparer<T[], T>
    {
        public ArrayBasicEqualityComparer(
            Func<T[], T[], bool> equalsFunc = null,
            Func<T[], int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class CollectionBasicEqualityComparerCore<T> : SequenceBasicEqualityComparer<ICollection<T>, T>
    {
        public CollectionBasicEqualityComparerCore(
            Func<ICollection<T>, ICollection<T>, bool> equalsFunc = null,
            Func<ICollection<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class CollectionBasicEqualityComparer<T> : SequenceBasicEqualityComparer<Collection<T>, T>
    {
        public CollectionBasicEqualityComparer(
            Func<Collection<T>, Collection<T>, bool> equalsFunc = null,
            Func<Collection<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ReadOnlyCollectionBasicEqualityComparerCore<T> : SequenceBasicEqualityComparer<IReadOnlyCollection<T>, T>
    {
        public ReadOnlyCollectionBasicEqualityComparerCore(
            Func<IReadOnlyCollection<T>, IReadOnlyCollection<T>, bool> equalsFunc = null,
            Func<IReadOnlyCollection<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ReadOnlyCollectionBasicEqualityComparer<T> : SequenceBasicEqualityComparer<ReadOnlyCollection<T>, T>
    {
        public ReadOnlyCollectionBasicEqualityComparer(
            Func<ReadOnlyCollection<T>, ReadOnlyCollection<T>, bool> equalsFunc = null,
            Func<ReadOnlyCollection<T>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class DictionaryBasicEqualityComparer<TDictnr, TKey, TValue> : SequenceBasicEqualityComparer<TDictnr, KeyValuePair<TKey, TValue>>
        where TDictnr : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public DictionaryBasicEqualityComparer(
            Func<TDictnr, TDictnr, bool> equalsFunc = null,
            Func<TDictnr, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class DictionaryBasicEqualityComparerCore<TKey, TValue> : DictionaryBasicEqualityComparer<IDictionary<TKey, TValue>, TKey, TValue>
    {
        public DictionaryBasicEqualityComparerCore(
            Func<IDictionary<TKey, TValue>, IDictionary<TKey, TValue>, bool> equalsFunc = null,
            Func<IDictionary<TKey, TValue>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class DictionaryBasicEqualityComparer<TKey, TValue> : DictionaryBasicEqualityComparer<Dictionary<TKey, TValue>, TKey, TValue>
    {
        public DictionaryBasicEqualityComparer(
            Func<Dictionary<TKey, TValue>, Dictionary<TKey, TValue>, bool> equalsFunc = null,
            Func<Dictionary<TKey, TValue>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ReadOnlyDictionaryBasicEqualityComparerCore<TKey, TValue> : DictionaryBasicEqualityComparer<IReadOnlyDictionary<TKey, TValue>, TKey, TValue>
    {
        public ReadOnlyDictionaryBasicEqualityComparerCore(
            Func<IReadOnlyDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, bool> equalsFunc = null,
            Func<IReadOnlyDictionary<TKey, TValue>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ReadOnlyDictionaryBasicEqualityComparer<TKey, TValue> : DictionaryBasicEqualityComparer<ReadOnlyDictionary<TKey, TValue>, TKey, TValue>
    {
        public ReadOnlyDictionaryBasicEqualityComparer(
            Func<ReadOnlyDictionary<TKey, TValue>, ReadOnlyDictionary<TKey, TValue>, bool> equalsFunc = null,
            Func<ReadOnlyDictionary<TKey, TValue>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }

    public class ConcurrentDictionaryBasicEqualityComparer<TKey, TValue> : DictionaryBasicEqualityComparer<ConcurrentDictionary<TKey, TValue>, TKey, TValue>
    {
        public ConcurrentDictionaryBasicEqualityComparer(
            Func<ConcurrentDictionary<TKey, TValue>, ConcurrentDictionary<TKey, TValue>, bool> equalsFunc = null,
            Func<ConcurrentDictionary<TKey, TValue>, int> hashCodeFunc = null) : base(
                equalsFunc,
                hashCodeFunc)
        {
        }
    }
}
