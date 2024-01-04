namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class FilterH
    {
        public static T FirstNotNull<T>(
            this T firstItem,
            params T[] nextItemsArr)
        {
            T retVal = firstItem;

            if (retVal == null)
            {
                retVal = nextItemsArr.First(
                    item => item != null);
            }

            return retVal;
        }

        public static KeyValuePair<int, T> FirstKvp<T>(
            this IEnumerable<T> nmrbl,
            Func<T, int, bool> predicate)
        {
            KeyValuePair<int, T> retKvp = new KeyValuePair<int, T>(-1, default);
            int idx = 0;

            foreach (T item in nmrbl)
            {
                if (predicate(item, idx))
                {
                    retKvp = new KeyValuePair<int, T>(idx, item);
                    break;
                }
                else
                {
                    idx++;
                }
            }

            return retKvp;
        }

        public static KeyValuePair<int, T> FirstKvp<T>(
            this IEnumerable<T> nmrbl,
            Func<T, bool> predicate) => nmrbl.FirstKvp(
                (item, idx) => predicate(item));

        public static KeyValuePair<int, char> LastKvp(
            this string str,
            Func<char, int, bool> predicate)
        {
            KeyValuePair<int, char> retKvp = new KeyValuePair<int, char>(-1, default);

            for (int i = str.Length - 1; i >= 0; i--)
            {
                var item = str[i];

                if (predicate(item, i))
                {
                    retKvp = new KeyValuePair<int, char>(i, item);
                    break;
                }
            }

            return retKvp;
        }
    }
}
