using static System.Environment;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IAppEnv
    {
        AppEnvLocator Locator { get; }
        string AppEnvDirBasePath { get; }
        string GetPath(AppEnvDir appEnvDir, params string[] pathPartsArr);
        string GetTypePath(AppEnvDir appEnvDir, Type dirNameType, params string[] pathPartsArr);
    }

    public class AppEnv : IAppEnv
    {
        public AppEnv(
            IJsonConversion jsonConversion,
            ITimeStampHelper timeStampHelper)
        {
            JsonConversion = jsonConversion ?? throw new ArgumentNullException(
                nameof(jsonConversion));

            TimeStampHelper = timeStampHelper ?? throw new ArgumentNullException(nameof(timeStampHelper));
            Locator = GetAppEnvLocatorImmtbl();
            AppEnvDirBasePath = GetAppEnvDirBasePath(Locator);
        }

        public AppEnvLocator Locator { get; }
        public string AppEnvDirBasePath { get; }

        protected IJsonConversion JsonConversion { get; }
        protected ITimeStampHelper TimeStampHelper { get; }

        protected virtual string AppEnvLocatorFilePath => "app-env-locator.json";

        public string GetPath(
            AppEnvDir appEnvDir,
            params string[] pathPartsArr)
        {
            pathPartsArr = new string[] {
                AppEnvDirBasePath,
                appEnvDir > 0 ? appEnvDir.ToString() : null,
            }.NotNull().Concat(pathPartsArr).ToArray();

            string retPath = Path.Combine(pathPartsArr);
            return retPath;
        }

        public string GetTypePath(
            AppEnvDir appEnvDir,
            Type dirNameType,
            params string[] pathPartsArr)
        {
            pathPartsArr = new string[] {
                AppEnvDirBasePath,
                appEnvDir > 0 ? appEnvDir.ToString() : null,
                dirNameType?.GetTypeFullDisplayName()
            }.NotNull().Concat(pathPartsArr).ToArray();

            string retPath = Path.Combine(pathPartsArr);
            return retPath;
        }

        protected virtual AppEnvLocator GetAppEnvLocatorMtbl()
        {
            AppEnvLocator appEnvLocator = null;
            string appEnvLocatorFilePath = AppEnvLocatorFilePath;

            if (!string.IsNullOrWhiteSpace(
                appEnvLocatorFilePath) && File.Exists(
                    appEnvLocatorFilePath))
            {
                var appEnvLocatorJson = File.ReadAllText(
                    appEnvLocatorFilePath);

                appEnvLocator = JsonConversion.Adapter.Deserialize<AppEnvLocator>(
                    appEnvLocatorJson);
            }

            return appEnvLocator;
        }

        protected virtual AppEnvLocator GetDefaultAppEnvLocatorMtbl()
        {
            /* var mtbl = new AppEnvLocator
            {
                AppEnvDirBasePath = DefaultDirNames.APP_ENV_DIR_BASE_REL_PATH,
            };

            return mtbl; */

            throw new NotSupportedException(
                "Default App Env Locator is not supported");
        }

        protected string GetStrValue(string value, string defaultValue)
        {
            string retValue = value;

            if (string.IsNullOrWhiteSpace(value))
            {
                retValue = defaultValue;
            }

            return retValue;
        }

        private string GetAppEnvDirBasePath(AppEnvLocator appEnvLocator)
        {
            string appEnvDirBasePath = appEnvLocator.AppEnvDirBasePath;

            if (!Path.IsPathRooted(appEnvDirBasePath))
            {
                appEnvDirBasePath = Path.Combine(
                    GetFolderPath(
                        SpecialFolder.ApplicationData),
                    appEnvDirBasePath);
            }

            return appEnvDirBasePath;
        }

        private AppEnvLocator GetAppEnvLocatorImmtbl() => GetAppEnvLocatorMtbl() ?? GetDefaultAppEnvLocatorMtbl();

        public static class DefaultDirNames
        {
            public const string APP_ENV_DIR_BASE_REL_PATH = "Turmerik/Apps";
        }
    }
}
