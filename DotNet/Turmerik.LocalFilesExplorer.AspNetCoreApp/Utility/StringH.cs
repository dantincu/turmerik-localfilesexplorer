using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static partial class StringH
    {
        public const string UPPER_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LOWER_LETTERS = "abcdefghijklmnopqrstuvwxyz";
        public const string DIGITS = "0123456789";
        public const string PARENS = "{[()]}";
        public const string CODE_OPERATORS = "./%+-*|&<>=:?";
        public const string CODE_IDNF_ALLOWED_NON_ALPHANUMERICS = "@$_";
        public const string OTHER_PUNCTUATION_CHARS = "`,;'\"";

        public const string NL_CHAR = "\n\r";

        public static readonly string NL = Environment.NewLine;

        public static bool IsNewLineChar(char c)
        {
            bool retVal = NL_CHAR.Contains(c);
            return retVal;
        }

        public static StringComparison GetStringComparison(bool ignoreCase)
        {
            StringComparison stringComparison;

            if (ignoreCase)
            {
                stringComparison = StringComparison.InvariantCultureIgnoreCase;
            }
            else
            {
                stringComparison = StringComparison.InvariantCulture;
            }

            return stringComparison;
        }

        public static string NwLns(int count)
        {
            string retStr = string.Concat(
                Enumerable.Range(0, count).Select(
                    i => NL).ToArray());

            return retStr;
        }

        public static int IndexOfStr(
            this string input,
            string str,
            bool ignoreCase = false,
            int startIndex = 0)
        {
            var stringComparison = GetStringComparison(ignoreCase);
            int idx = input.IndexOf(str, startIndex, stringComparison);

            return idx;
        }

        public static string JoinStrRange(
            int rangeCount,
            Func<int, string> strFactory,
            string joinStr = null)
        {
            string retStr;

            if (rangeCount > 0)
            {
                string[] strArr = Enumerable.Range(0, rangeCount).Select(
                idx => strFactory(idx)).ToArray();

                if (joinStr != null)
                {
                    retStr = string.Join(joinStr, strArr);
                }
                else
                {
                    retStr = string.Concat(strArr);
                }
            }
            else
            {
                retStr = string.Empty;
            }

            return retStr;
        }

        public static string JoinStrRange(
            int rangeCount,
            string str,
            string joinStr = null)
        {
            string retStr = JoinStrRange(
                rangeCount,
                idx => str, joinStr);

            return retStr;
        }

        public static string Nullify(
            this string str,
            bool ignoreWhitespaces = false)
        {
            if (str != null)
            {
                string checkStr = str;

                if (ignoreWhitespaces)
                {
                    checkStr = checkStr.Trim();
                }

                if (string.IsNullOrEmpty(checkStr))
                {
                    str = null;
                }
            }

            return str;
        }

        public static string JoinNotNullStr(
            this string[] strArr,
            string joinStr,
            bool? excludeAllWhitespaces = true)
        {
            if (excludeAllWhitespaces != false)
            {
                strArr = strArr.Where(str => str.Nullify(
                    excludeAllWhitespaces.HasValue) != null).ToArray();
            }

            string retStr = string.Empty;

            if (strArr.Any())
            {
                retStr = string.Join(joinStr, strArr);
            }

            return retStr;
        }

        public static string JoinStr(
            this string[] strArr,
            string joinStr = null) => string.Join(
                joinStr ?? string.Empty,
                strArr);

        public static string[] GetTextLines(
            string text) => text.Split('\n').Select(
                line => line.TrimEnd('\r')).ToArray();
    }
}
