using System.Numerics;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class BasicComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> comparerFunc;

        public BasicComparer(Func<T, T, int> comparerFunc)
        {
            this.comparerFunc = comparerFunc ?? throw new ArgumentNullException(nameof(comparerFunc));
        }

        public int Compare(T x, T y)
        {
            int result = comparerFunc(x, y);
            return result;
        }
    }

    public class StringComparer : BasicComparer<string>
    {
        public StringComparer(
            Func<string, string, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class DateTimeComparer : BasicComparer<DateTime>
    {
        public DateTimeComparer(
            Func<DateTime, DateTime, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class TimeSpanComparer : BasicComparer<TimeSpan>
    {
        public TimeSpanComparer(
            Func<TimeSpan, TimeSpan, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class IntComparer : BasicComparer<int>
    {
        public IntComparer(
            Func<int, int, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class UIntComparer : BasicComparer<uint>
    {
        public UIntComparer(
            Func<uint, uint, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class LongComparer : BasicComparer<long>
    {
        public LongComparer(
            Func<long, long, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class ULongComparer : BasicComparer<ulong>
    {
        public ULongComparer(
            Func<ulong, ulong, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class ShortComparer : BasicComparer<short>
    {
        public ShortComparer(
            Func<short, short, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class UShortComparer : BasicComparer<ushort>
    {
        public UShortComparer(
            Func<ushort, ushort, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class ByteComparer : BasicComparer<byte>
    {
        public ByteComparer(
            Func<byte, byte, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class SByteComparer : BasicComparer<sbyte>
    {
        public SByteComparer(
            Func<sbyte, sbyte, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class DecimalComparer : BasicComparer<decimal>
    {
        public DecimalComparer(
            Func<decimal, decimal, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class FloatComparer : BasicComparer<float>
    {
        public FloatComparer(
            Func<float, float, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class DoubleComparer : BasicComparer<double>
    {
        public DoubleComparer(
            Func<double, double, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }

    public class BigIntegerComparer : BasicComparer<BigInteger>
    {
        public BigIntegerComparer(
            Func<BigInteger, BigInteger, int> comparerFunc = null) : base(
                comparerFunc.FirstNotNull(
                    (a, b) => a.CompareTo(b)))
        {
        }
    }
}
