using System.Collections.ObjectModel;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public static partial class SerializableLogEventCore
    {
        public interface IClnblCore<TLogLevel, TTimeStamp>
        {
            TTimeStamp TimeStamp { get; }
            TLogLevel Level { get; }
            string Message { get; }
            object Data { get; }

            SerializableExcp.IClnbl GetException();
            IEnumerable<object> GetProperties();
        }

        public class ImmtblCore<TLogLevel, TTimeStamp> : IClnblCore<TLogLevel, TTimeStamp>
        {
            public ImmtblCore(IClnblCore<TLogLevel, TTimeStamp> src)
            {
                TimeStamp = src.TimeStamp;
                Level = src.Level;
                Message = src.Message;
                Data = src.Data;

                Exception = src.GetException().AsImmtbl();
                Properties = src.GetProperties()?.RdnlC();
            }

            public TTimeStamp TimeStamp { get; }
            public TLogLevel Level { get; }
            public string Message { get; }
            public object Data { get; }

            public SerializableExcp.Immtbl Exception { get; }
            public ReadOnlyCollection<object> Properties { get; }

            public SerializableExcp.IClnbl GetException() => Exception;
            public IEnumerable<object> GetProperties() => Properties;
        }

        public class MtblCore<TLogLevel, TTimeStamp> : IClnblCore<TLogLevel, TTimeStamp>
        {
            public MtblCore()
            {
            }

            public MtblCore(IClnblCore<TLogLevel, TTimeStamp> src)
            {
                TimeStamp = src.TimeStamp;
                Level = src.Level;
                Message = src.Message;
                Data = src.Data;

                Exception = src.GetException().AsMtbl();
                Properties = src.GetProperties()?.ToList();
            }

            public TTimeStamp TimeStamp { get; set; }
            public TLogLevel Level { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }

            public SerializableExcp.Mtbl Exception { get; set; }
            public List<object> Properties { get; set; }

            public SerializableExcp.IClnbl GetException() => Exception;
            public IEnumerable<object> GetProperties() => Properties;
        }

        public static ImmtblCore<TLogLevel, TTimeStamp> ToImmtbl<TLogLevel, TTimeStamp>(
            this IClnblCore<TLogLevel, TTimeStamp> src) => new ImmtblCore<TLogLevel, TTimeStamp>(src);

        public static ImmtblCore<TLogLevel, TTimeStamp> AsImmtbl<TLogLevel, TTimeStamp>(
            this IClnblCore<TLogLevel, TTimeStamp> src) => src as ImmtblCore<TLogLevel, TTimeStamp> ?? src?.ToImmtbl();

        public static MtblCore<TLogLevel, TTimeStamp> ToMtbl<TLogLevel, TTimeStamp>(
            this IClnblCore<TLogLevel, TTimeStamp> src) => new MtblCore<TLogLevel, TTimeStamp>(src);

        public static MtblCore<TLogLevel, TTimeStamp> AsMtbl<TLogLevel, TTimeStamp>(
            this IClnblCore<TLogLevel, TTimeStamp> src) => src as MtblCore<TLogLevel, TTimeStamp> ?? src?.ToMtbl();

        public static ReadOnlyCollection<ImmtblCore<TLogLevel, TTimeStamp>> ToImmtblCllctn<TLogLevel, TTimeStamp>(
            this IEnumerable<IClnblCore<TLogLevel, TTimeStamp>> src) => src?.Select(
                item => item?.AsImmtbl()).RdnlC();

        public static ReadOnlyCollection<ImmtblCore<TLogLevel, TTimeStamp>> AsImmtblCllctn<TLogLevel, TTimeStamp>(
            this IEnumerable<IClnblCore<TLogLevel, TTimeStamp>> src) =>
            src as ReadOnlyCollection<ImmtblCore<TLogLevel, TTimeStamp>> ?? src?.ToImmtblCllctn();

        public static List<MtblCore<TLogLevel, TTimeStamp>> ToMtblList<TLogLevel, TTimeStamp>(
            this IEnumerable<IClnblCore<TLogLevel, TTimeStamp>> src) => src?.Select(
                item => item?.AsMtbl()).ToList();

        public static List<MtblCore<TLogLevel, TTimeStamp>> AsMtblList<TLogLevel, TTimeStamp>(
            this IEnumerable<IClnblCore<TLogLevel, TTimeStamp>> src) => src as List<MtblCore<TLogLevel, TTimeStamp>> ?? src?.ToMtblList();

        public static ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TTimeStamp>> AsImmtblDictnr<TKey, TLogLevel, TTimeStamp>(
            IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TTimeStamp>>> src) => src as ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TTimeStamp>> ?? (
            src as Dictionary<TKey, MtblCore<TLogLevel, TTimeStamp>>)?.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.AsImmtbl()).RdnlD();

        public static Dictionary<TKey, MtblCore<TLogLevel, TTimeStamp>> AsMtblDictnr<TKey, TLogLevel, TTimeStamp>(
            IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TTimeStamp>>> src) => src as Dictionary<TKey, MtblCore<TLogLevel, TTimeStamp>> ?? (
            src as ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TTimeStamp>>)?.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.AsMtbl());

        public static IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TTimeStamp>>> ToClnblDictnr<TKey, TLogLevel, TTimeStamp>(
            this Dictionary<TKey, MtblCore<TLogLevel, TTimeStamp>> src) => src.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.SafeCast<IClnblCore<TLogLevel, TTimeStamp>>());

        public static IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TTimeStamp>>> ToClnblDictnr<TKey, TLogLevel, TTimeStamp>(
            this ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TTimeStamp>> src) => src.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.SafeCast<IClnblCore<TLogLevel, TTimeStamp>>());
    }
}
