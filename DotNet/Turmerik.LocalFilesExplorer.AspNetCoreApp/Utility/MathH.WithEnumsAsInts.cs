using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Helpers
{
    public static partial class MathH
    {
        public static int ReduceEnumToInt<TEnum>(
            this TEnum value,
            Func<int, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            int intVal = converter(value);
            intVal = func(intVal);

            return intVal;
        }

        public static int ReduceEnumsToInt<TEnum>(
            this Tuple<TEnum, TEnum> enumTuple,
            Func<Tuple<int, int>, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            var intTuple = Tuple.Create(
                converter(enumTuple.Item1),
                converter(enumTuple.Item2));

            var retVal = func(intTuple);
            return retVal;
        }

        public static int ReduceEnumsToInt<TEnum>(
            this Tuple<TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int>, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            var intTuple = Tuple.Create(
                converter(enumTuple.Item1),
                converter(enumTuple.Item2),
                converter(enumTuple.Item3));

            var retVal = func(intTuple);
            return retVal;
        }

        public static int ReduceEnumsToInt<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int>, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            var intTuple = Tuple.Create(
                converter(enumTuple.Item1),
                converter(enumTuple.Item2),
                converter(enumTuple.Item3),
                converter(enumTuple.Item4));

            var retVal = func(intTuple);
            return retVal;
        }

        public static int ReduceEnumsToInt<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int, int>, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            var intTuple = Tuple.Create(
                converter(enumTuple.Item1),
                converter(enumTuple.Item2),
                converter(enumTuple.Item3),
                converter(enumTuple.Item4),
                converter(enumTuple.Item5));

            var retVal = func(intTuple);
            return retVal;
        }

        public static int ReduceEnumsToInt<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int, int, int>, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            var intTuple = Tuple.Create(
                converter(enumTuple.Item1),
                converter(enumTuple.Item2),
                converter(enumTuple.Item3),
                converter(enumTuple.Item4),
                converter(enumTuple.Item5),
                converter(enumTuple.Item6));

            var retVal = func(intTuple);
            return retVal;
        }

        public static int ReduceEnumsToInt<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int, int, int, int>, int> func,
            Func<TEnum, int> converter = null)
            where TEnum : struct
        {
            converter = converter.FirstNotNull(
                val => (int)(object)val);

            var intTuple = Tuple.Create(
                converter(enumTuple.Item1),
                converter(enumTuple.Item2),
                converter(enumTuple.Item3),
                converter(enumTuple.Item4),
                converter(enumTuple.Item5),
                converter(enumTuple.Item6),
                converter(enumTuple.Item7));

            var retVal = func(intTuple);
            return retVal;
        }

        public static TEnum ReduceEnum<TEnum>(
            this TEnum value,
            Func<int, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            int intVal = value.ReduceEnumToInt(
                func,
                converter);

            value = backConverter(intVal);
            return value;
        }

        public static TEnum ReduceEnums<TEnum>(
            this Tuple<TEnum, TEnum> enumTuple,
            Func<Tuple<int, int>, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            var intVal = enumTuple.ReduceEnumsToInt(
                func,
                converter);

            var retVal = backConverter(intVal);
            return retVal;
        }

        public static TEnum ReduceEnums<TEnum>(
            this Tuple<TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int>, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            var intVal = enumTuple.ReduceEnumsToInt(
                func,
                converter);

            var retVal = backConverter(intVal);
            return retVal;
        }

        public static TEnum ReduceEnums<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int>, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            var intVal = enumTuple.ReduceEnumsToInt(
                func,
                converter);

            var retVal = backConverter(intVal);
            return retVal;
        }

        public static TEnum ReduceEnums<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int, int>, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            var intVal = enumTuple.ReduceEnumsToInt(
                func,
                converter);

            var retVal = backConverter(intVal);
            return retVal;
        }

        public static TEnum ReduceEnums<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int, int, int>, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            var intVal = enumTuple.ReduceEnumsToInt(
                func,
                converter);

            var retVal = backConverter(intVal);
            return retVal;
        }

        public static TEnum ReduceEnums<TEnum>(
            this Tuple<TEnum, TEnum, TEnum, TEnum, TEnum, TEnum, TEnum> enumTuple,
            Func<Tuple<int, int, int, int, int, int, int>, int> func,
            Func<TEnum, int> converter = null,
            Func<int, TEnum> backConverter = null)
            where TEnum : struct
        {
            backConverter = backConverter.FirstNotNull(
                val => (TEnum)(object)val);

            var intVal = enumTuple.ReduceEnumsToInt(
                func,
                converter);

            var retVal = backConverter(intVal);
            return retVal;
        }
    }
}
