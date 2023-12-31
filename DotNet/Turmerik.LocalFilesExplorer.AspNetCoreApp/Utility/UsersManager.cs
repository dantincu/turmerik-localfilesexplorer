namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface IUsersManager : IDisposable
    {
        Task<UserIdnf> GetUserIdnfAsync(
            string userIdnfHash);
    }

    public class UsersManager : IUsersManager
    {
        private readonly ISemaphoreSlimAdapter usersIdnfStorageSemaphore;
        private readonly IUsersIdnfStorage usersIdnfStorage;

        public UsersManager(
            IUsersIdnfStorage usersIdnfStorage,
            ISynchronizedAdapterFactory synchronizedAdapterFactory)
        {
            usersIdnfStorageSemaphore = synchronizedAdapterFactory.SempahoreSlim(
                new SemaphoreSlim(1, 1));

            this.usersIdnfStorage = usersIdnfStorage ?? throw new ArgumentNullException(
                nameof(usersIdnfStorage));
        }

        protected Dictionary<string, UserIdnf> SessionsMap { get; private set; }

        public void Dispose()
        {
            usersIdnfStorageSemaphore.Dispose();
            usersIdnfStorage.Dispose();
        }

        public async Task<UserIdnf> GetUserIdnfAsync(
            string userIdnfHash)
        {
            await AssureSessionsMapIsSet();

            UserIdnf userIdnf = await usersIdnfStorageSemaphore.GetAsync(async () =>
            {
                if (!SessionsMap.TryGetValue(userIdnfHash, out userIdnf))
                {
                    userIdnf = new UserIdnf
                    {
                        UserUuid = Guid.NewGuid(),
                        UserIdnfHash = userIdnfHash,
                    };

                    SessionsMap.Add(userIdnfHash, userIdnf);

                    await usersIdnfStorage.WriteAsync(
                        SessionsMap.Values, rethrowExc: false);
                }

                return userIdnf;
            });

            return userIdnf;
        }

        private async Task<Dictionary<string, UserIdnf>> AssureSessionsMapIsSet(
            ) => await usersIdnfStorageSemaphore.GetAsync(async () =>
            {
                SessionsMap ??= (await usersIdnfStorage.ReadAsync(
                    rethrowExc: false)).ToDictionary(
                    idnf => idnf.UserIdnfHash, idnf => idnf);

                return SessionsMap;
            });
    }
}
