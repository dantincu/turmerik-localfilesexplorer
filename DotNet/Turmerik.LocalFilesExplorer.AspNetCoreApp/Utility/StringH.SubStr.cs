namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static partial class StringH
    {
        public static Tuple<string, string> SplitStr(
            this string inputStr,
            IdxRetriever<char, string> splitter)
        {
            Tuple<string, string> retTpl;
            int inputLen = inputStr.Length;

            int idx = splitter(inputStr, inputLen);

            if (idx >= 0 && idx < inputStr.Length)
            {
                string firstStr = inputStr.Substring(0, idx);
                string secondStr = inputStr.Substring(idx);

                retTpl = new Tuple<string, string>(
                    firstStr, secondStr);
            }
            else
            {
                retTpl = new Tuple<string, string>(
                    inputStr, null);
            }

            return retTpl;
        }

        public static string SliceStr(
            this string inputStr,
            int startIdx,
            int count,
            bool trimEntry = false)
        {
            int startIdxVal, length;

            if (startIdx >= 0)
            {
                startIdxVal = startIdx;

                if (count >= 0)
                {
                    length = count;
                }
                else
                {
                    length = inputStr.Length - startIdx + count + 1;
                }
            }
            else
            {
                if (count >= 0)
                {
                    length = count;
                    startIdxVal = inputStr.Length + startIdx;
                }
                else
                {
                    length = -1 * count;
                    startIdxVal = inputStr.Length + startIdx - length;
                }
            }

            string subStr = inputStr.Substring(startIdxVal, length);

            if (trimEntry)
            {
                subStr = subStr.Trim();
            }

            return subStr;
        }

        public static bool Matches(
            this string inputStr,
            int startIdx,
            out int relIdx,
            Func<char, int, bool> partialMatchPredicate,
            Func<char, int, bool> fullMatchPredicate = null)
        {
            fullMatchPredicate = fullMatchPredicate.FirstNotNull(
                (chr, idx) => true);

            int strLen = inputStr.Length;
            int idx = startIdx;
            relIdx = 0;

            bool matches = false;

            while (idx < strLen)
            {
                char chr = inputStr[idx];

                if (!(matches = partialMatchPredicate(
                    chr, relIdx)))
                {
                    break;
                }

                if (fullMatchPredicate(chr, relIdx))
                {
                    break;
                }

                matches = false;
                idx++;
                relIdx++;
            }

            return matches;
        }
    }
}
