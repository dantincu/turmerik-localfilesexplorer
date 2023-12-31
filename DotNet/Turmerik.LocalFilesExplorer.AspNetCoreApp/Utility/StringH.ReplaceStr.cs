using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public partial class StringH
    {
        public static string ReplaceStr(
            this string inputStr,
            int startIdx,
            int length,
            string replacing)
        {
            string beforeStr = inputStr.Substring(0, startIdx);
            string afterStr = inputStr.Substring(startIdx + length);

            string retStr = string.Concat(
                beforeStr, replacing, afterStr);

            return retStr;
        }
    }
}
