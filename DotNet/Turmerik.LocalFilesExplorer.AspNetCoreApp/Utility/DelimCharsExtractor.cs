namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IDelimCharsExtractor
    {
        string[] SplitStr(
            string inStr,
            char delim,
            char emptyChar);

        string[] SplitStr(
            string str,
            char delim,
            char startDelim,
            char emptyChar,
            out bool startsWithDelim,
            bool onlySplitIfStartsWithDelim = true);

        string TrimStr(
            string str,
            char startDelim,
            out bool startsWithDelim);
    }

    public class DelimCharsExtractor : IDelimCharsExtractor
    {
        public string[] SplitStr(
            string inStr,
            char delim,
            char emptyChar)
        {
            var strList = new List<string>();
            var chrList = new List<char>();
            char prevChar = default;

            for (int i = 0; i < inStr.Length; i++)
            {
                char chr = inStr[i];

                if (chr == delim || chr == emptyChar)
                {
                    if (chr == delim && prevChar != default && prevChar != chr)
                    {
                        strList.Add(
                            new string(
                                chrList.ToArray()));

                        chrList.Clear();
                    }
                    else if (prevChar == chr)
                    {
                        chrList.Add(chr);
                        prevChar = default;
                    }
                }
                else
                {
                    chrList.Add(chr);
                }

                prevChar = chr;
            }

            strList.Add(
                new string(
                    chrList.ToArray()));

            return strList.ToArray();
        }

        public string[] SplitStr(
            string str,
            char delim,
            char startDelim,
            char emptyChar,
            out bool startsWithDelim,
            bool onlySplitIfStartsWithDelim = true)
        {
            str = TrimStr(str,
                startDelim,
                out startsWithDelim);

            string[] strArr;

            if (startsWithDelim || !onlySplitIfStartsWithDelim)
            {
                strArr = SplitStr(
                    str, delim,
                    emptyChar);
            }
            else
            {
                strArr = [str];
            }

            return strArr;
        }

        public string TrimStr(
            string str,
            char startDelim,
            out bool startsWithDelim)
        {
            startsWithDelim = str.FirstOrDefault() == startDelim;
            bool startsWithDblDelim = startsWithDelim && str.Length > 1 && str[1] == startDelim;
            startsWithDelim = startsWithDelim && !startsWithDblDelim;

            if (startsWithDelim || startsWithDblDelim)
            {
                str = str.Substring(1);
            }

            return str;
        }
    }
}
