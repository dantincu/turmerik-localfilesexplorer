using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class TupleH
    {
        public static TResult WithTuple<TResult, T1, T2>(
            this Tuple<T1, T2> tuple,
            Func<T1, T2, TResult> factory)
        {
            (var t1, var t2) = tuple;
            var result = factory(t1, t2);

            return result;
        }

        public static TResult WithTuple<TResult, T1, T2, T3>(
            this Tuple<T1, T2, T3> tuple,
            Func<T1, T2, T3, TResult> factory)
        {
            (var t1, var t2, var t3) = tuple;
            var result = factory(t1, t2, t3);

            return result;
        }

        public static TResult WithTuple<TResult, T1, T2, T3, T4>(
            this Tuple<T1, T2, T3, T4> tuple,
            Func<T1, T2, T3, T4, TResult> factory)
        {
            (var t1, var t2, var t3, var t4) = tuple;
            var result = factory(t1, t2, t3, t4);

            return result;
        }

        public static TResult WithTuple<TResult, T1, T2, T3, T4, T5>(
            this Tuple<T1, T2, T3, T4, T5> tuple,
            Func<T1, T2, T3, T4, T5, TResult> factory)
        {
            (var t1, var t2, var t3, var t4, var t5) = tuple;
            var result = factory(t1, t2, t3, t4, t5);

            return result;
        }

        public static TResult WithTuple<TResult, T1, T2, T3, T4, T5, T6>(
            this Tuple<T1, T2, T3, T4, T5, T6> tuple,
            Func<T1, T2, T3, T4, T5, T6, TResult> factory)
        {
            (var t1, var t2, var t3, var t4, var t5, var t6) = tuple;
            var result = factory(t1, t2, t3, t4, t5, t6);

            return result;
        }

        public static TResult WithTuple<TResult, T1, T2, T3, T4, T5, T6, T7>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7> tuple,
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> factory)
        {
            (var t1, var t2, var t3, var t4, var t5, var t6, var t7) = tuple;
            var result = factory(t1, t2, t3, t4, t5, t6, t7);

            return result;
        }

        public static TResult WithTuple<TResult, T1, T2, T3, T4, T5, T6, T7, TRest>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple,
            Func<T1, T2, T3, T4, T5, T6, T7, TRest, TResult> factory)
        {
            T1 t1 = tuple.Item1;
            T2 t2 = tuple.Item2;
            T3 t3 = tuple.Item3;
            T4 t4 = tuple.Item4;
            T5 t5 = tuple.Item5;
            T6 t6 = tuple.Item6;
            T7 t7 = tuple.Item7;
            TRest rest = tuple.Rest;

            var result = factory(t1, t2, t3, t4, t5, t6, t7, rest);
            return result;
        }

        public static Tuple<T1, T2> ActWithTuple<T1, T2>(
            this Tuple<T1, T2> tuple,
            Action<T1, T2> action)
        {
            (var t1, var t2) = tuple;
            action(t1, t2);

            return tuple;
        }

        public static Tuple<T1, T2, T3> ActWithTuple<T1, T2, T3>(
            this Tuple<T1, T2, T3> tuple,
            Action<T1, T2, T3> action)
        {
            (var t1, var t2, var t3) = tuple;
            action(t1, t2, t3);

            return tuple;
        }

        public static Tuple<T1, T2, T3, T4> ActWithTuple<T1, T2, T3, T4>(
            this Tuple<T1, T2, T3, T4> tuple,
            Action<T1, T2, T3, T4> action)
        {
            (var t1, var t2, var t3, var t4) = tuple;
            action(t1, t2, t3, t4);

            return tuple;
        }

        public static Tuple<T1, T2, T3, T4, T5> ActWithTuple<T1, T2, T3, T4, T5>(
            this Tuple<T1, T2, T3, T4, T5> tuple,
            Action<T1, T2, T3, T4, T5> action)
        {
            (var t1, var t2, var t3, var t4, var t5) = tuple;
            action(t1, t2, t3, t4, t5);

            return tuple;
        }

        public static Tuple<T1, T2, T3, T4, T5, T6> ActWithTuple<T1, T2, T3, T4, T5, T6>(
            this Tuple<T1, T2, T3, T4, T5, T6> tuple,
            Action<T1, T2, T3, T4, T5, T6> action)
        {
            (var t1, var t2, var t3, var t4, var t5, var t6) = tuple;
            action(t1, t2, t3, t4, t5, t6);

            return tuple;
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7> ActWithTuple<T1, T2, T3, T4, T5, T6, T7>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7> tuple,
            Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            (var t1, var t2, var t3, var t4, var t5, var t6, var t7) = tuple;
            action(t1, t2, t3, t4, t5, t6, t7);

            return tuple;
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> ActWithTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple,
            Action<T1, T2, T3, T4, T5, T6, T7, TRest> action)
        {
            T1 t1 = tuple.Item1;
            T2 t2 = tuple.Item2;
            T3 t3 = tuple.Item3;
            T4 t4 = tuple.Item4;
            T5 t5 = tuple.Item5;
            T6 t6 = tuple.Item6;
            T7 t7 = tuple.Item7;
            TRest rest = tuple.Rest;

            action(t1, t2, t3, t4, t5, t6, t7, rest);
            return tuple;
        }
    }
}
