namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static partial class StringH
    {
        public static string ReplaceAllChars(
            this string input,
            Dictionary<char, string> replDictnr)
        {
            int inputLen = input.Length;

            string[] partsArr = input.Split(replDictnr.Keys.ToArray());
            int maxIdx = partsArr.Length - 2;

            List<string> partsList = new List<string>();
            int i = 0;

            for (int idx = 0; idx <= maxIdx; idx++)
            {
                string part = partsArr[idx];
                partsList.Add(part);

                i += part.Length;

                if (i < inputLen)
                {
                    char c = input[i];
                    string replPart = replDictnr[c];

                    partsList.Add(replPart);
                    i += replPart.Length;
                }
            }

            partsList.Add(partsArr.Last());
            string output = string.Concat(partsList);

            return output;
        }

        public static string ReplaceAllChars(
            this string input,
            Dictionary<char, char> replDictnr)
        {
            Dictionary<char, string> strReplDictnr = replDictnr.ToArray().ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.ToString());

            string output = input.ReplaceAllChars(strReplDictnr);
            return output;
        }

        public static string ReplaceChars(
            this string str,
            Func<char, char> replaceFactory,
            params char[] replacedChars)
        {
            string retStr = str.ReplaceChars(
                replaceFactory, replacedChars);

            return retStr;
        }

        public static string ReplaceChars(
            this string str,
            Func<char, char> replaceFactory,
            IEnumerable<char> replacedChars)
        {
            replaceFactory = replaceFactory.FirstNotNull(
                c => default);

            char[] retChars = str.Select(
                c => replacedChars.Contains(c) ? replaceFactory(c) : c).ToArray();

            string retStr = retChars.ToStr();
            return retStr;
        }
    }
}
