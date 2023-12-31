using System.Collections.ObjectModel;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public static class SerializableExcp
    {
        public interface IClnbl
        {
            string Message { get; }
            string StackTrace { get; }
            string TypeName { get; }

            IClnbl GetInnerExcp();

        }

        public class Immtbl : IClnbl
        {
            public Immtbl(IClnbl src)
            {
                Message = src.Message;
                TypeName = src.TypeName;
                StackTrace = src.StackTrace;
                InnerExcp = src.GetInnerExcp().AsImmtbl();
            }

            public string Message { get; }
            public string TypeName { get; }
            public string StackTrace { get; }

            public Immtbl InnerExcp { get; }

            public IClnbl GetInnerExcp() => InnerExcp;
        }

        public class Mtbl : IClnbl
        {
            public Mtbl()
            {
            }

            public Mtbl(IClnbl src)
            {
                Message = src.Message;
                TypeName = src.TypeName;
                StackTrace = src.StackTrace;
                InnerExcp = src.GetInnerExcp().AsMtbl();
            }

            public string Message { get; set; }
            public string TypeName { get; set; }
            public string StackTrace { get; set; }

            public Mtbl InnerExcp { get; set; }

            public IClnbl GetInnerExcp() => InnerExcp;
        }

        public static Mtbl FromExcp(
            Exception exc) =>
                exc is AggregateException aggExc ? SerializableAggExcp.FromAggExcp(
                    aggExc) : exc != null ? FromExcpCore(exc) : null;

        public static Mtbl FromExcpCore(
            Exception exc) => new Mtbl
            {
                Message = exc.Message,
                TypeName = exc.GetType().FullName,
                StackTrace = exc.StackTrace,
                InnerExcp = exc.InnerException?.With(
                    FromExcp)
            };

        public static Immtbl ToImmtbl(
            this IClnbl src) => new Immtbl(src);

        public static Immtbl AsImmtbl(
            this IClnbl src) => src as Immtbl ?? src?.ToImmtbl();

        public static Mtbl ToMtbl(
            this IClnbl src) => new Mtbl(src);

        public static Mtbl AsMtbl(
            this IClnbl src) => src as Mtbl ?? src?.ToMtbl();

        public static ReadOnlyCollection<Immtbl> ToImmtblCllctn(
            this IEnumerable<IClnbl> src) => src?.Select(
                item => item?.AsImmtbl()).RdnlC();

        public static ReadOnlyCollection<Immtbl> AsImmtblCllctn(
            this IEnumerable<IClnbl> src) =>
            src as ReadOnlyCollection<Immtbl> ?? src?.ToImmtblCllctn();

        public static List<Mtbl> ToMtblList(
            this IEnumerable<IClnbl> src) => src?.Select(
                item => item?.AsMtbl()).ToList();

        public static List<Mtbl> AsMtblList(
            this IEnumerable<IClnbl> src) => src as List<Mtbl> ?? src?.ToMtblList();

        public static ReadOnlyDictionary<TKey, Immtbl> AsImmtblDictnr<TKey>(
            IEnumerable<KeyValuePair<TKey, IClnbl>> src) => src as ReadOnlyDictionary<TKey, Immtbl> ?? (src as Dictionary<TKey, Mtbl>)?.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value?.AsImmtbl()).RdnlD();

        public static Dictionary<TKey, Mtbl> AsMtblDictnr<TKey>(
            IEnumerable<KeyValuePair<TKey, IClnbl>> src) => src as Dictionary<TKey, Mtbl> ?? (src as ReadOnlyDictionary<TKey, Immtbl>)?.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value?.AsMtbl());

        public static IEnumerable<KeyValuePair<TKey, IClnbl>> ToClnblDictnr<TKey>(
            this Dictionary<TKey, Mtbl> src) => src.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.SafeCast<IClnbl>());

        public static IEnumerable<KeyValuePair<TKey, IClnbl>> ToClnblDictnr<TKey>(
            this ReadOnlyDictionary<TKey, Immtbl> src) => src.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.SafeCast<IClnbl>());
    }
}
