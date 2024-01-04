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
    }
}
