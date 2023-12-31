namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class FilterH
    {
        public static IEnumerable<T> NotNull<T>(
            this IEnumerable<T> nmrbl) => nmrbl.Where(
            item => item != null);

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

        public static List<T> ReverseOrder<T>(
            this IEnumerable<T> nmrbl)
        {
            var list = nmrbl.ToList();
            list.Reverse();

            return list;
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

        public static KeyValuePair<int, T> LastKvp<T>(
            this IList<T> list,
            Func<T, int, bool> predicate)
        {
            KeyValuePair<int, T> retKvp = new KeyValuePair<int, T>(-1, default);

            for (int i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];

                if (predicate(item, i))
                {
                    retKvp = new KeyValuePair<int, T>(i, item);
                    break;
                }
            }

            return retKvp;
        }

        public static KeyValuePair<int, T> LastKvp<T>(
            this T[] arr,
            Func<T, int, bool> predicate)
        {
            KeyValuePair<int, T> retKvp = new KeyValuePair<int, T>(-1, default);

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                var item = arr[i];

                if (predicate(item, i))
                {
                    retKvp = new KeyValuePair<int, T>(i, item);
                    break;
                }
            }

            return retKvp;
        }

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

        public static T GetNthVal<T>(
            this IEnumerable<T> nmrbl,
            int idx,
            Func<int, T> defaultValueFactory = null)
        {
            T retVal = default;
            int i = 0;
            bool found = false;

            foreach (var val in nmrbl)
            {
                if (i == idx)
                {
                    retVal = val;
                    found = true;
                    break;
                }
                else
                {
                    i++;
                }
            }

            if (!found)
            {
                if (defaultValueFactory == null)
                {
                    throw new InvalidOperationException(
                        $"Sequence contains {i} elements while the required index is {idx}");
                }
                else
                {
                    retVal = defaultValueFactory(i);
                }
            }

            return retVal;
        }
    }
}
