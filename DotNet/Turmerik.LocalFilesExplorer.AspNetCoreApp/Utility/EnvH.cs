using System.Reflection;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public class EnvH
    {
        public const string APP_SETTINGS_FILE_NAME = "appsettings.json";

        public static readonly string ExecutingAssemblyDirPath;

        static EnvH()
        {
            ExecutingAssemblyDirPath = GetExecutingAssemblyDirPath();
        }

        public static string GetExecutingAssemblyPath(
            ) => Assembly.GetExecutingAssembly().Location;

        public static string GetExecutingAssemblyDirPath()
        {
            string executingAssemblyPath = GetExecutingAssemblyPath();

            string executingAssemblyDirPath = Path.GetDirectoryName(
                executingAssemblyPath);

            return executingAssemblyDirPath;
        }
    }
}
