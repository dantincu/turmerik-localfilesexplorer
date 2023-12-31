using System.Collections.ObjectModel;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Logging;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IUsersIdnfStorage : IDisposable
    {
        Task<ReadOnlyCollection<UserIdnf>> ReadAsync(
            Func<Exception, ReadOnlyCollection<UserIdnf>> excHandler = null,
            bool rethrowExc = true);

        Task WriteAsync(
            IEnumerable<UserIdnf> userIdnf,
            Action<Exception> excHandler = null,
            bool rethrowExc = true);

        Task RemoveAllAsync();
    }

    public class LocalJsonFileUsersIdnfStorage : IUsersIdnfStorage
    {
        private const string JSON_FILE_NAME = "data.json";

        private readonly IAppEnv appEnv;
        private readonly IJsonConversion jsonConversion;

        private readonly string jsonDirPath;
        private readonly string jsonFilePath;
        private readonly IMutexAdapter jsonFileMutex;

        private readonly IAppLogger logger;

        public LocalJsonFileUsersIdnfStorage(
            IAppEnv appEnv,
            IJsonConversion jsonConversion,
            ISynchronizedAdapterFactory synchronizedAdapterFactory,
            IAppLoggerCreator appLoggerCreator)
        {
            this.appEnv = appEnv ?? throw new ArgumentNullException(
                nameof(appEnv));

            this.jsonConversion = jsonConversion ?? throw new ArgumentNullException(
                nameof(jsonConversion));

            jsonDirPath = appEnv.GetTypePath(
                AppEnvDir.Data,
                GetType());

            jsonFilePath = Path.Combine(
                jsonDirPath, JSON_FILE_NAME);

            jsonFileMutex = synchronizedAdapterFactory.Mutex(
                MutexH.Create(jsonFilePath, true));

            logger = appLoggerCreator.GetAppLogger(GetType());
        }

        public void Dispose()
        {
            jsonFileMutex.Dispose();
        }

        public async Task<ReadOnlyCollection<UserIdnf>> ReadAsync(
            Func<Exception, ReadOnlyCollection<UserIdnf>> excHandler = null,
            bool rethrowExc = true)
        {
            ReadOnlyCollection<UserIdnf> usersCollctn;

            try
            {
                string json = ReadJsonFromFile();

                if (json != null)
                {
                    usersCollctn = jsonConversion.Adapter.Deserialize<UserIdnf[]>(json).RdnlC();
                }
                else
                {
                    usersCollctn = new UserIdnf[0].RdnlC();
                }
            }
            catch (Exception exc)
            {
                logger.Error(exc, "Could not read user identifiers from file path {0}", jsonFilePath);

                if (excHandler != null)
                {
                    usersCollctn = excHandler(exc);
                }
                else
                {
                    usersCollctn = new UserIdnf[0].RdnlC();
                }

                if (rethrowExc)
                {
                    throw;
                }
            }

            return usersCollctn;
        }

        public async Task WriteAsync(
            IEnumerable<UserIdnf> userIdnfsNmrbl,
            Action<Exception> excHandler = null,
            bool rethrowExc = true)
        {
            try
            {
                string json = jsonConversion.Adapter.Serialize(
                    userIdnfsNmrbl);

                Directory.CreateDirectory(jsonDirPath);
                WriteJsonToFile(json);
            }
            catch (Exception exc)
            {
                logger.Error(exc, "Could not write user identifiers to file path {0}", jsonFilePath);

                excHandler?.Invoke(exc);

                if (rethrowExc)
                {
                    throw;
                }
            }
        }

        public async Task RemoveAllAsync() => jsonFileMutex.Execute(() =>
        {
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }
        });

        private string? ReadJsonFromFile() => jsonFileMutex.Get(() =>
        {
            string json = null;

            if (File.Exists(jsonFilePath))
            {
                json = File.ReadAllText(jsonFilePath);
            }

            return json;
        });

        private void WriteJsonToFile(
            string json) => jsonFileMutex.Execute(
                () => File.WriteAllText(
                    jsonFilePath, json));
    }
}
