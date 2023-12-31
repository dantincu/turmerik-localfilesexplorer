using Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class UserIdnfH
    {
        public static string GetUserIdnfHash(
            this HttpContext httpContext,
            string userIdnfTpl)
        {
            string userName = httpContext.User.Identity?.Name ?? string.Empty;

            string userIdnfHash = EncodeH.GetUserIdnfHash(
                userIdnfTpl, userName);

            return userIdnfHash;
        }

        public static async Task<UserIdnf> GetUserIdnfAsync(
            this HttpContext httpContext,
            IUsersManager usersManager,
            string userIdnfTpl) => await usersManager.GetUserIdnfAsync(
                httpContext.GetUserIdnfHash(userIdnfTpl));

        public static async Task<UserIdnf> GetLocalFilesUserIdnfAsync(
            this HttpContext httpContext,
            IUsersManager usersManager) => await usersManager.GetUserIdnfAsync(
                httpContext.GetUserIdnfHash(
                    TrmrkUserIdnfH.Tpl.LOCAL_FILES));

        public static async Task<UserIdnf> GetOneDriveUserIdnfAsync(
            this HttpContext httpContext,
            IUsersManager usersManager) => await usersManager.GetUserIdnfAsync(
                httpContext.GetUserIdnfHash(
                    TrmrkUserIdnfH.Tpl.ONE_DRIVE));
    }
}
