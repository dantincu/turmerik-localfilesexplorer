using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static partial class StringH
    {
        public static bool ContainsAny(this string str, params char[] chars)
        {
            bool retVal = str.ContainsAny(chars);
            return retVal;
        }

        public static bool ContainsAny(this string str, IEnumerable<char> chars)
        {
            bool retVal = chars.Any(
                c => str.Contains(c));

            return retVal;
        }

        public static char[] GetContained(this string str, IEnumerable<char> chars)
        {
            char[] retChars = chars.Where(c => str.Contains(c)).ToArray();
            return retChars;
        }

        public static bool IsLatinLetter(this char chr)
        {
            bool isLtnTtr =
                chr >= 'a' && chr <= 'z' ||
                chr >= 'A' && chr <= 'Z';

            return isLtnTtr;
        }

        public static string ToStr(this char[] charsArr, int startIndex = -1, int length = -1)
        {
            string retStr;

            if (startIndex >= 0 && length >= 0)
            {
                retStr = new string(charsArr, startIndex, length);
            }
            else
            {
                retStr = new string(charsArr);
            }

            return retStr;
        }
    }
}
