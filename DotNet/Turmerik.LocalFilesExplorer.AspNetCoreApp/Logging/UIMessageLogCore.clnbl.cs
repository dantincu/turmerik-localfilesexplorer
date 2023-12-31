using System.Collections.ObjectModel;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging
{
    public static class UIMessageLogCore
    {
        public interface IClnblCore<TLogLevel, TDateTime> : SerializableLogEventCore.IClnblCore<TLogLevel, TDateTime>
        {
            string SerializedExcp { get; }
            string SerializedLevel { get; }
            string RenderedMsg { get; }
            string RenderedTimeStamp { get; }
            bool HasBeenRead { get; }
        }

        public class ImmtblCore<TLogLevel, TDateTime> : SerializableLogEventCore.ImmtblCore<TLogLevel, TDateTime>, IClnblCore<TLogLevel, TDateTime>
        {
            public ImmtblCore(IClnblCore<TLogLevel, TDateTime> src) : base(src)
            {
                SerializedExcp = src.SerializedExcp;
                SerializedLevel = src.SerializedLevel;
                RenderedMsg = src.RenderedMsg;
                RenderedTimeStamp = src.RenderedTimeStamp;
                HasBeenRead = src.HasBeenRead;
            }

            public string SerializedExcp { get; }
            public string SerializedLevel { get; }
            public string RenderedMsg { get; }
            public string RenderedTimeStamp { get; }
            public bool HasBeenRead { get; }
        }

        public class MtblCore<TLogLevel, TDateTime> : SerializableLogEventCore.MtblCore<TLogLevel, TDateTime>, IClnblCore<TLogLevel, TDateTime>
        {
            public MtblCore()
            {
            }

            public MtblCore(IClnblCore<TLogLevel, TDateTime> src) : base(src)
            {
                SerializedExcp = src.SerializedExcp;
                SerializedLevel = src.SerializedLevel;
                RenderedMsg = src.RenderedMsg;
                RenderedTimeStamp = src.RenderedTimeStamp;
                HasBeenRead = src.HasBeenRead;
            }

            public string SerializedExcp { get; set; }
            public string SerializedLevel { get; set; }
            public string RenderedMsg { get; set; }
            public string RenderedTimeStamp { get; set; }
            public bool HasBeenRead { get; set; }
        }

        public static ImmtblCore<TLogLevel, TDateTime> ToImmtbl<TLogLevel, TDateTime>(
            this IClnblCore<TLogLevel, TDateTime> src) => new ImmtblCore<TLogLevel, TDateTime>(src);

        public static ImmtblCore<TLogLevel, TDateTime> AsImmtbl<TLogLevel, TDateTime>(
            this IClnblCore<TLogLevel, TDateTime> src) => src as ImmtblCore<TLogLevel, TDateTime> ?? src?.ToImmtbl();

        public static MtblCore<TLogLevel, TDateTime> ToMtbl<TLogLevel, TDateTime>(
            this IClnblCore<TLogLevel, TDateTime> src) => new MtblCore<TLogLevel, TDateTime>(src);

        public static MtblCore<TLogLevel, TDateTime> AsMtbl<TLogLevel, TDateTime>(
            this IClnblCore<TLogLevel, TDateTime> src) => src as MtblCore<TLogLevel, TDateTime> ?? src?.ToMtbl();

        public static ReadOnlyCollection<ImmtblCore<TLogLevel, TDateTime>> ToImmtblCllctn<TLogLevel, TDateTime>(
            this IEnumerable<IClnblCore<TLogLevel, TDateTime>> src) => src?.Select(
                item => item?.AsImmtbl()).RdnlC();

        public static ReadOnlyCollection<ImmtblCore<TLogLevel, TDateTime>> AsImmtblCllctn<TLogLevel, TDateTime>(
            this IEnumerable<IClnblCore<TLogLevel, TDateTime>> src) =>
            src as ReadOnlyCollection<ImmtblCore<TLogLevel, TDateTime>> ?? src?.ToImmtblCllctn();

        public static List<MtblCore<TLogLevel, TDateTime>> ToMtblList<TLogLevel, TDateTime>(
            this IEnumerable<IClnblCore<TLogLevel, TDateTime>> src) => src?.Select(
                item => item?.AsMtbl()).ToList();

        public static List<MtblCore<TLogLevel, TDateTime>> AsMtblList<TLogLevel, TDateTime>(
            this IEnumerable<IClnblCore<TLogLevel, TDateTime>> src) => src as List<MtblCore<TLogLevel, TDateTime>> ?? src?.ToMtblList();

        public static ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TDateTime>> AsImmtblDictnr<TKey, TLogLevel, TDateTime>(
            IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TDateTime>>> src) => src as ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TDateTime>> ?? (
            src as Dictionary<TKey, MtblCore<TLogLevel, TDateTime>>)?.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.AsImmtbl()).RdnlD();

        public static Dictionary<TKey, MtblCore<TLogLevel, TDateTime>> AsMtblDictnr<TKey, TLogLevel, TDateTime>(
            IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TDateTime>>> src) => src as Dictionary<TKey, MtblCore<TLogLevel, TDateTime>> ?? (
            src as ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TDateTime>>)?.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.AsMtbl());

        public static IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TDateTime>>> ToClnblDictnr<TKey, TLogLevel, TDateTime>(
            this Dictionary<TKey, MtblCore<TLogLevel, TDateTime>> src) => src.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.SafeCast<IClnblCore<TLogLevel, TDateTime>>());

        public static IEnumerable<KeyValuePair<TKey, IClnblCore<TLogLevel, TDateTime>>> ToClnblDictnr<TKey, TLogLevel, TDateTime>(
            this ReadOnlyDictionary<TKey, ImmtblCore<TLogLevel, TDateTime>> src) => src.ToDictionary(
                kvp => kvp.Key, kvp => kvp.Value.SafeCast<IClnblCore<TLogLevel, TDateTime>>());
    }
}
