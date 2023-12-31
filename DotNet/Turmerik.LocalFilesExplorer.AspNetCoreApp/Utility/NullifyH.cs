using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class NullifyH
    {
        public static bool? Nullify(this bool value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static char? Nullify(this char value) => (value == default(char)) switch
        {
            true => null,
            false => value
        };

        public static int? Nullify(this int value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static uint? Nullify(this uint value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static long? Nullify(this long value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static ulong? Nullify(this ulong value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static short? Nullify(this short value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static ushort? Nullify(this ushort value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static byte? Nullify(this byte value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static sbyte? Nullify(this sbyte value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static DateTime? Nullify(this DateTime value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static DateTimeOffset? Nullify(this DateTimeOffset value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static TimeSpan? Nullify(this TimeSpan value) => (value == default) switch
        {
            true => null,
            false => value
        };

        public static TNmrbl? NullifyN<TNmrbl>(this TNmrbl nmrbl) where TNmrbl : class, IEnumerable => nmrbl.GetEnumerator(
            ).MoveNext() switch
            {
                true => nmrbl,
                false => null
            };
    }
}
