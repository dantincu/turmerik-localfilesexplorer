namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class LocalDeviceH
    {
        public static readonly bool IsWinOS = Environment.OSVersion.Platform <= PlatformID.WinCE;
    }
}
