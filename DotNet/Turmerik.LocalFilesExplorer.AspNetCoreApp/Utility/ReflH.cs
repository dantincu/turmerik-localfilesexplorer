namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class ReflH
    {
        public static string GetTypeFullDisplayName(
            this Type type,
            char stopDelim = '[') => GetTypeFullDisplayName(
                type.FullName,
                stopDelim);

        public static string GetTypeFullDisplayName(
            string typeFullName,
            char stopDelim = '[') => typeFullName?.SplitStr(
                (str, len) => str.FirstKvp((c, i) => c == stopDelim).Key).Item1;
    }
}
