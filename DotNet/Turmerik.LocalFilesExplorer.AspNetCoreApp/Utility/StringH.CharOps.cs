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

        public static char[] GetContained(this string str, params char[] chars)
        {
            char[] retChars = str.GetContained((IEnumerable<char>)chars);
            return retChars;
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

        public static bool ContainsOnly(this string input, bool allowWhitespace, char chr)
        {
            bool retVal = input.All(c => c == chr || allowWhitespace && char.IsWhiteSpace(c));
            return retVal;
        }

        public static bool ContainsOnly(this string input, bool allowWhitespace, params char[] charsArr)
        {
            bool retVal = input.All(c => allowWhitespace && char.IsWhiteSpace(c) || charsArr.Contains(c));
            return retVal;
        }

        public static bool AllWhiteSpacesAre(this string input, Func<char, bool> predicate)
        {
            bool retVal = input.All(c => !char.IsWhiteSpace(c) || predicate(c));
            return retVal;
        }

        public static bool AllWhiteSpacesAre(this string input, params char[] allowedWhiteSpaces)
        {
            bool retVal = input.All(c => !char.IsWhiteSpace(c) || allowedWhiteSpaces.Contains(c));
            return retVal;
        }

        public static bool AllWhiteSpacesAre(this string input, char allowedWhiteSpace = ' ')
        {
            bool retVal = input.All(c => !char.IsWhiteSpace(c) || allowedWhiteSpace == c);
            return retVal;
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

        public static Tuple<string, ReadOnlyCollection<char>> ToRdnlStr(this char[] charsArr, int startIndex = -1, int length = -1)
        {
            Tuple<string, ReadOnlyCollection<char>> retTuple;

            if (startIndex >= 0 && length >= 0)
            {
                retTuple = Tuple.Create(
                    new string(charsArr, startIndex, length),
                    charsArr.Skip(startIndex).Take(length).RdnlC());
            }
            else
            {
                retTuple = Tuple.Create(
                    new string(charsArr),
                    charsArr.RdnlC());
            }

            return retTuple;
        }
    }
}
