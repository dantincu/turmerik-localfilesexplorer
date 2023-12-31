using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static partial class MathH
    {
        public static long Magnitude(
            this long value)
        {
            long magnitude = 1;

            while (value >= 10)
            {
                magnitude *= 10;
                value /= 10;
            }

            return magnitude;
        }

        public static int Magnitude(
            this int value)
        {
            int magnitude = 1;

            while (value >= 10)
            {
                magnitude *= 10;
                value /= 10;
            }

            return magnitude;
        }

        public static long ReqIntv(
            this long value,
            long min = 0,
            long max = long.MaxValue)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        public static int ReqIntv(
            this int value,
            int min = 0,
            int max = int.MaxValue)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }
    }
}
