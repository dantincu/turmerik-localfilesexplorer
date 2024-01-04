using System.Collections.ObjectModel;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class NmrblH
    {
        public static T[] Arr<T>(this T firstVal, params T[] nextItemsArr)
        {
            T[] retArr = new T[nextItemsArr.Length + 1];
            retArr[0] = firstVal;

            nextItemsArr.CopyTo(retArr, 1);
            return retArr;
        }

        public static TResult WithCount<TResult, TItem, TNmrbl>(
            this TNmrbl nmrbl,
            Func<TNmrbl, int, TResult> factory,
            Func<TNmrbl, int> countFunc = null) where TNmrbl : IEnumerable<TItem>
        {
            int count = countFunc.FirstNotNull(
                n => n.Count()).Invoke(nmrbl);

            var result = factory(nmrbl, count);
            return result;
        }

        public static ReadOnlyCollection<T> RdnlC<T>(
            this IList<T> list) => new ReadOnlyCollection<T>(list);

        public static ReadOnlyCollection<T> RdnlC<T>(
            this IEnumerable<T> nmrbl) => new ReadOnlyCollection<T>(
                nmrbl.ToArray());

        public static ReadOnlyDictionary<TKey, TValue> RdnlD<TKey, TValue>(
            this IDictionary<TKey, TValue> dictnr)
            where TKey : notnull => new ReadOnlyDictionary<TKey, TValue>(dictnr);
    }
}
