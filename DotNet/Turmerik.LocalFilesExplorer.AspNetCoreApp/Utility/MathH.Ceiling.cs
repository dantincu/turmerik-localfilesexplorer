using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers
{
    public partial class MathH
    {
        public static int CeilingToInt(
            this float value)
        {
            var dblVal = Math.Ceiling(value);
            var retVal = (int)dblVal;
            return retVal;
        }

        public static int CeilingToInt(
            this double value)
        {
            value = Math.Ceiling(value);
            var retVal = (int)value;
            return retVal;
        }

        public static int CeilingToInt(
            this decimal value)
        {
            value = Math.Ceiling(value);
            var retVal = (int)value;
            return retVal;
        }

        public static long CeilingToLong(
            this float value)
        {
            var dblVal = Math.Ceiling(value);
            var retVal = (long)dblVal;
            return retVal;
        }

        public static long CeilingToLong(
            this double value)
        {
            value = Math.Ceiling(value);
            var retVal = (long)value;
            return retVal;
        }

        public static long CeilingToLong(
            this decimal value)
        {
            value = Math.Ceiling(value);
            var retVal = (long)value;
            return retVal;
        }
    }
}
