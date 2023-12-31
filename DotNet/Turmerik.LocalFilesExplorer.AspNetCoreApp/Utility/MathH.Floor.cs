using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers
{
    public partial class MathH
    {
        public static int FloorToInt(
            this float value)
        {
            var dblVal = Math.Floor(value);
            var retVal = (int)dblVal;
            return retVal;
        }

        public static int FloorToInt(
            this double value)
        {
            value = Math.Floor(value);
            var retVal = (int)value;
            return retVal;
        }

        public static int FloorToInt(
            this decimal value)
        {
            value = Math.Floor(value);
            var retVal = (int)value;
            return retVal;
        }

        public static long FloorToLong(
            this float value)
        {
            var dblVal = Math.Floor(value);
            var retVal = (long)dblVal;
            return retVal;
        }

        public static long FloorToLong(
            this double value)
        {
            value = Math.Floor(value);
            var retVal = (long)value;
            return retVal;
        }

        public static long FloorToLong(
            this decimal value)
        {
            value = Math.Floor(value);
            var retVal = (long)value;
            return retVal;
        }
    }
}
