using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IAppInstanceStartInfoProvider
    {
        IAppInstanceStartInfo Data { get; }
        public string ProcessDirName { get; }
    }

    public interface IAppInstanceStartInfo
    {
        Guid InstanceGuid { get; }
        DateTime InstanceTimeStamp { get; }
        long InstanceTicks { get; }

        string GetInstanceGuidStr();
        string GetInstanceGuidStrNoDashes();
    }

    public static class AppInstanceStartInfo
    {
        public static AppInstanceStartInfoImmtbl ToImmtbl(
            this IAppInstanceStartInfo src) => new AppInstanceStartInfoImmtbl(src);

        public static AppInstanceStartInfoImmtbl ToImmtbl(
            this AppInstanceStartInfoMtbl src) => new AppInstanceStartInfoImmtbl(src);

        public static AppInstanceStartInfoMtbl ToMtbl(
            this IAppInstanceStartInfo src) => new AppInstanceStartInfoMtbl(src);
    }

    public class AppInstanceStartInfoImmtbl : IAppInstanceStartInfo
    {
        public AppInstanceStartInfoImmtbl(
            IAppInstanceStartInfo src)
        {
            InstanceGuid = src.InstanceGuid;
            InstanceTimeStamp = src.InstanceTimeStamp;
            InstanceTicks = src.InstanceTimeStamp.Ticks;
        }

        public AppInstanceStartInfoImmtbl(
            AppInstanceStartInfoMtbl src) : this(
                (IAppInstanceStartInfo)src)
        {
            InstanceGuidStr = src.InstanceGuidStr;
            InstanceGuidStrNoDashes = src.InstanceGuidStrNoDashes;
        }

        public Guid InstanceGuid { get; }
        public DateTime InstanceTimeStamp { get; }
        public long InstanceTicks { get; }

        private string InstanceGuidStr { get; }
        private string InstanceGuidStrNoDashes { get; }

        public string GetInstanceGuidStr() => InstanceGuidStr;
        public string GetInstanceGuidStrNoDashes() => InstanceGuidStrNoDashes;
    }

    public class AppInstanceStartInfoMtbl : IAppInstanceStartInfo
    {
        public AppInstanceStartInfoMtbl()
        {
        }

        public AppInstanceStartInfoMtbl(
            IAppInstanceStartInfo src)
        {
            InstanceGuid = src.InstanceGuid;
            InstanceTimeStamp = src.InstanceTimeStamp;
            InstanceTicks = src.InstanceTimeStamp.Ticks;
        }

        public AppInstanceStartInfoMtbl(
            AppInstanceStartInfoMtbl src) : this(
                (IAppInstanceStartInfo)src)
        {
            InstanceGuidStr = src.InstanceGuidStr;
            InstanceGuidStrNoDashes = src.InstanceGuidStrNoDashes;
        }

        public Guid InstanceGuid { get; set; }
        public string InstanceGuidStr { get; set; }
        public string InstanceGuidStrNoDashes { get; set; }
        public DateTime InstanceTimeStamp { get; set; }
        public long InstanceTicks { get; set; }

        public string GetInstanceGuidStr() => InstanceGuidStr;
        public string GetInstanceGuidStrNoDashes() => InstanceGuidStrNoDashes;
    }

    public class AppInstanceStartInfoProvider : IAppInstanceStartInfoProvider
    {
        public AppInstanceStartInfoProvider()
        {
            Guid guid = Guid.NewGuid();
            var timeStamp = DateTime.UtcNow;

            Data = new AppInstanceStartInfoImmtbl(
                new AppInstanceStartInfoMtbl
                {
                    InstanceGuid = guid,
                    InstanceTimeStamp = timeStamp,
                    InstanceTicks = timeStamp.Ticks,
                    InstanceGuidStr = guid.ToString("D"),
                    InstanceGuidStrNoDashes = guid.ToString("N")
                });

            ProcessDirName = string.Format("[{0}][{1}]",
                Data.InstanceTicks,
                Data.GetInstanceGuidStrNoDashes());
        }

        public IAppInstanceStartInfo Data { get; }
        public string ProcessDirName { get; }
    }
}
