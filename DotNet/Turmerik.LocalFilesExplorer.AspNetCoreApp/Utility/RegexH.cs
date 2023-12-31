using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class RegexH
    {
        /// <summary>
        /// According to ChatGPT and https://stackoverflow.com/questions/14134558/list-of-all-special-characters-that-need-to-be-escaped-in-a-regex : <br /> <br />
        /// Here is a list of special characters that need to be escaped in a regex: <br />
        /// <b><c>.</c></b> <i>(dot)</i> <br />
        /// <b><c>*</c></b> <i>(asterisk)</i> <br />
        /// <b><c>+</c></b> <i>(plus)</i> <br />
        /// <b><c>?</c></b> <i>(question mark)</i> <br />
        /// <b><c>{</c></b> <i>and</i> <b><c>}</c></b> <i>(curly braces)</i> <br />
        /// <b><c>[</c></b> <i>and</i> <b><c>]</c></b> <i>(square brackets)</i> <br />
        /// <b><c>\</c></b> <i>(backslash)</i> <br />
        /// <b><c>^</c></b> <i>(caret)</i> <br />
        /// <b><c>$</c></b> <i>(dollar sign)</i> <br />
        /// <b><c>|</c></b> <i>(pipe)</i> <br />
        /// <b><c>(</c></b> <i>and</i> <b><c>)</c></b> <i>(parentheses)</i> <br />
        /// </summary>
        public const string REGEX_SPECIAL_CHARS = "\\.[]{}()*+-=!?^$|";

        public static RegexEncodedText EncodeForRegex(
            this string str,
            bool shouldMatchTheStartOfStr = false,
            bool shouldMatchTheEndOfStr = false)
        {
            var sb = new StringBuilder();

            if (shouldMatchTheStartOfStr)
            {
                sb.Append('^');
            }

            foreach (var c in str)
            {
                if (REGEX_SPECIAL_CHARS.Contains(c))
                {
                    sb.Append($"\\{c}");
                }
                else
                {
                    sb.Append(c);
                }
            }

            if (shouldMatchTheEndOfStr)
            {
                sb.Append('$');
            }

            return new RegexEncodedText(str, sb.ToString());
        }
    }
}
