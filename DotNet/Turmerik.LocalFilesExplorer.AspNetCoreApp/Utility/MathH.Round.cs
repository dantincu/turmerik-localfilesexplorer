using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers
{
    public partial class MathH
    {
        public static int RoundToInt(
            this float value)
        {
            var dblVal = Math.Round(value);
            var retVal = (int)dblVal;
            return retVal;
        }

        public static int RoundToInt(
            this double value)
        {
            value = Math.Round(value);
            var retVal = (int)value;
            return retVal;
        }

        public static int RoundToInt(
            this decimal value)
        {
            value = Math.Round(value);
            var retVal = (int)value;
            return retVal;
        }

        public static long RoundToLong(
            this float value)
        {
            var dblVal = Math.Round(value);
            var retVal = (long)dblVal;
            return retVal;
        }

        public static long RoundToLong(
            this double value)
        {
            value = Math.Round(value);
            var retVal = (long)value;
            return retVal;
        }

        public static long RoundToLong(
            this decimal value)
        {
            value = Math.Round(value);
            var retVal = (long)value;
            return retVal;
        }
    }
}
